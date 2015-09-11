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
	public interface IFavoritesRepository
	{
		Task<List<Paper>> FetchAll ();

//		Task<Course> FetchOne (string id, bool forceRefresh = false);
	}

	public class FavoritesRepository : IFavoritesRepository
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IGetFavoritesService Service;

		public FavoritesRepository ()
		{
		}

		public async Task<List<Paper>> FetchAll()
		{
			AM = DI.Get<AppModel>();
			if(AM.Favorites == null){
				await LoadFromRemote();
			}

			return AM.Favorites;
		}

		async Task LoadFromRemote ()
		{
			var nodes = await Service.Execute ();
			AM.Favorites = new List<Paper> ();
			foreach (var node in nodes) {
				AM.Favorites.Add (node.paper);
			}
			
//			var url = string.Format("{0}favorite/index/?caller=wpb-iPhone", Constants.BASE_URI);
//			_cancelationToken = new CancellationTokenSource ();
//			var result = await _downloadService.Download(url, _cancelationToken, AM.UserSessionCookie);
//
//			List<PaperNode> payload;
//			try{
//				payload = JsonConvert.DeserializeObject<List<PaperNode>>(result);
//				AM.Favorites = new List<Paper> ();
//				foreach (var node in payload) {
//					AM.Favorites.Add (node.paper);
//				}
//			}catch(Exception ex){
//
//				Insights.Report(ex);
//
//			}


		}
	}
}

