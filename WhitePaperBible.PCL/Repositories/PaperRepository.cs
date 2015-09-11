using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;
using System.Threading.Tasks;
using Xamarin;
using Newtonsoft.Json;
using System.Threading;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Repositories
{
	public interface IPaperRepository
	{
		Task<List<Paper>> FetchAll ();

		Task<Paper> FetchOne (Paper paper, bool forceRefresh = false);
	}

	public class PaperRepository : IPaperRepository
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IGetPapersService Service;

		[Inject]
		public IGetPaperReferencesService ReferencesService;

		public PaperRepository ()
		{
		}

		public async Task<List<Paper>> FetchAll()
		{
			AM = DI.Get<AppModel>();
			if(AM.Papers == null){
				await LoadFromRemote();
			}

			return AM.Papers;
		}

		async Task LoadFromRemote ()
		{
			var nodes = await Service.Execute ();
			AM.Papers = new List<Paper> ();
			foreach (var node in nodes) {
				AM.Papers.Add (node.paper);
			}

		}

		public async Task<Paper> FetchOne (Paper paper, bool forceRefresh = false)
		{
			AM = DI.Get<AppModel>();
			paper = AM.GetPaperById(paper.id);

			if(ReferencesService == null){
				ReferencesService = DI.Get<IGetPaperReferencesService>();
			}
			List<ReferenceNode> nodes = await ReferencesService.Execute(paper.id);

			var references = new List<Reference> ();
			foreach (var node in nodes) {
				references.Add (node.reference);
			}

			paper.references = references;
			paper.WatchText = paper.HtmlContent;//paper.ToPlainText();//make it so the watch extension doesn't need this conversion code
			return paper;
		}
	}
}

