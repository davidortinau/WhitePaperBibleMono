using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;
using System.Threading.Tasks;

namespace WhitePaperBible.Core.Repositories
{
	public class FavoritesRepository
	{
		[Inject]
		public AppModel AM;

		public FavoritesRepository ()
		{
		}

		public async Task<List<Paper>> GetFavorites()
		{
			if(AM.Favorites == null){
				await LoadFavoritesFromRemote();
			}

			return AM.Favorites;
		}

		async void LoadFavoritesFromRemote ()
		{
			
			var url = string.Format("{0}favorite/index/?caller=wpb-iPhone", Constants.BASE_URI);
			var result = await _downloadService.Download(url, _cancelationToken);

			SyncFilePayload payload;
			try{
				payload = JsonConvert.DeserializeObject<SyncFilePayload>(result);
				_pendingQueue = payload.Files;
				_queue = new List<string>(_pendingQueue);
			}catch(Exception ex){

				Insights.Report(ex);
				Insights.Track(string.Format("Json Parse Error: {0}", ex.Message));

				Mvx.Trace("What happened?! {0}", ex.Message);
			}
		}
	}
}

