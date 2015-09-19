using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISaveReferenceService:IBaseService
	{
		void Execute (Paper paper, Reference reference);
	}

	public class SaveReferenceService:BaseService, ISaveReferenceService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (Paper paper, Reference reference)
		{
			var url = Constants.BASE_URI;
			url += String.Format("papers/{0}/references?reference[reference]={1}&reference[paper_id]={2}", paper.id, reference.reference, paper.id);
			url += "&caller=wpb-iPhone";
			Client.OpenURL (url, MethodEnum.POST, true);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
//			var user = ParseResponse<UserDTO> ();
			DispatchSuccess (new ReferenceSavedEventArgs (ParseResponse<ReferenceNode> ()));
		}

		#endregion

		public class ReferenceSavedEventArgs:EventArgs
		{
			public readonly Reference Reference;

			public ReferenceSavedEventArgs (ReferenceNode node)
			{
				Reference = node.reference;
			}
		}
	}
}

