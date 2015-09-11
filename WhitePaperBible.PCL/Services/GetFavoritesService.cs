using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;
using System.Threading.Tasks;

namespace WhitePaperBible.Core.Services
{
	public interface IGetFavoritesService:IBaseService
	{
		Task<List<PaperNode>> Execute ();
	}

	public class GetFavoritesService:BaseService, IGetFavoritesService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public async Task<List<PaperNode>> Execute ()
		{
			await Client.OpenURL (Constants.BASE_URI + "favorite/index/?caller=wpb-iPhone", MethodEnum.GET, true);
			return JsonConvert.DeserializeObject<List<PaperNode>> (Client.ResponseText);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetFavoritesServiceEventArgs (ParseResponse<List<PaperNode>> ()));
		}

		#endregion
	}

	public class GetFavoritesServiceEventArgs:EventArgs
	{
		public readonly List<PaperNode> Papers;

		public GetFavoritesServiceEventArgs (List<PaperNode> papers)
		{
			Papers = papers;
		}
	}
}

