using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhitePaperBible.Core.Services
{
	public interface IGetPapersByTagService:IBaseService
	{
		void Execute (string tagName);
	}

	public class GetPapersByTagService:BaseService, IGetPapersByTagService
	{
		public void Execute (string tagName)
		{
			Client.OpenURL (Constants.BASE_URI + "papers/tagged/" + tagName + ".json?caller=wpb-iPhone");
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetPapersByTagServiceEventArgs (ParseResponse<List<PaperNode>> ()));
		}

		#endregion
	}

	public class GetPapersByTagServiceEventArgs:EventArgs
	{
		public readonly List<PaperNode> Papers;

		public GetPapersByTagServiceEventArgs (List<PaperNode> papers)
		{
			Papers = papers;
		}
	}
}

