using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using System.Threading.Tasks;
using PCLStorage;
using System.Xml.Serialization;
using System.IO;

namespace WhitePaperBible.Core.Commands
{
	public class SaveStorageCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public PapersReceivedInvoker PapersReceived;

		public override void Execute (InvokerArgs args)
		{
			var store = Store ();
		}

		public async Task Store()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync("WhitePaperBible", CreationCollisionOption.OpenIfExists);
			IFile file = await folder.CreateFileAsync("app_model.dat", CreationCollisionOption.ReplaceExisting);

			var serializer = new XmlSerializer (AM.GetType());
			var stringWriter = new StringWriter ();
			serializer.Serialize (stringWriter, AM);
			await file.WriteAllTextAsync (stringWriter.ToString ());
		}
	}
}
