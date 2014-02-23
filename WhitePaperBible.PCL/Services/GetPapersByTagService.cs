using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhitePaperBible.Core.Services
{
	public interface IGetPapersByTagService
	{
		void Execute (int tagId);
	}

	public class GetPapersByTagService:BaseService, IGetPapersByTagService
	{
		#region IGetPapersByTagService implementation

		public void Execute (int tagId)
		{
			Client.OpenURL ("papers/tagged/" + tagId.ToString () + "?caller=wpb-iPhone");
		}

		#endregion

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

