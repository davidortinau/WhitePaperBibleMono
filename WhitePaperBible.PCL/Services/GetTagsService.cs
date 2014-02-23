using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhitePaperBible.Core.Services
{
	public interface IGetTagsService:IBaseService
	{
		void Execute ();
	}

	public class GetTagsService:BaseService, IGetTagsService
	{
		public void Execute ()
		{
			Client.OpenURL (Constants.BASE_URI + "tag.json?caller=wpb-iPhone");
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetTagsServiceEventArgs (ParseResponse<List<TagNode>> ()));
		}

		#endregion
	}

	public class GetTagsServiceEventArgs:EventArgs
	{
		public readonly List<TagNode> Tags;

		public GetTagsServiceEventArgs (List<TagNode> tags)
		{
			Tags = tags;
		}
	}
}

