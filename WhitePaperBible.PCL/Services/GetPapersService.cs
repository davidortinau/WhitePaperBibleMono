using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhitePaperBible.Core.Services
{
	public interface IGetPapersService:IBaseService
	{
		void Execute ();
	}

	public class GetPapersService:BaseService, IGetPapersService
	{
		public void Execute ()
		{
			Client.OpenURL (Constants.BASE_URI + "papers.json?caller=wpb-iPhone", false);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetPapersServiceEventArgs (ParseResponse<List<PaperNode>> ()));
		}

		#endregion
	}

	public class GetPapersServiceEventArgs:EventArgs
	{
		public readonly List<PaperNode> Papers;

		public GetPapersServiceEventArgs (List<PaperNode> papers)
		{
			Papers = papers;
		}
	}
}

