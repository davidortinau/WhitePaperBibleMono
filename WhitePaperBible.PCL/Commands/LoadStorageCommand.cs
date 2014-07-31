using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using System.Threading.Tasks;
using PCLStorage;
using System.Xml.Serialization;
using System.IO;

namespace WhitePaperBible.Core.Commands
{
	public class LoadStorageCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public StorageLoadedInvoker StorageLoaded;

		public override void Execute (InvokerArgs args)
		{
			var loadStore = LoadStore ();
		}

		public async Task LoadStore()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync("WhitePaperBible", CreationCollisionOption.OpenIfExists);
			IFile file = await folder.GetFileAsync("app_model.dat");

			var serializer = new XmlSerializer (AM.GetType());
			var fileText = await file.ReadAllTextAsync ();
			var stringReader = new StringReader (fileText);
			AppModel m = (AppModel)serializer.Deserialize (stringReader);
			AM.User = m.User;
			AM.Papers = m.Papers;
			AM.Favorites = m.Favorites;
			AM.Tags = m.Tags;
			AM.IsLoggedIn = (AM.User != null && AM.User.ID > 0);
			AM.UserSessionCookie = m.UserSessionCookie;
			StorageLoaded.Invoke ();
		}
	}
}

