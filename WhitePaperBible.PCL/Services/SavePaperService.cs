using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISavePaperService:IBaseService
	{
		void Execute (Paper paper);
	}

	public class SavePaperService:BaseService, ISavePaperService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (Paper paper)
		{
			var url = Constants.BASE_URI;

			if (paper.id <= 0) {
				// create
				url += String.Format ("papers?paper[title]={0}&paper[description]={1}&user_id={2}&paper[tag_list]={3}", paper.title, paper.description, AM.User.ID, paper.TagsString());
				url += "&caller=wpb-iPhone";
				Client.OpenURL (url, MethodEnum.POST, true);
			}else{
				// update

				url += String.Format ("papers/update/{0}?paper[title]={1}&paper[description]={2}&user_id={3}&paper[tag_list]={4}", paper.id, paper.title, paper.description, AM.User.ID, paper.TagsString());
				url += "&caller=wpb-iPhone";

				// now add references
				foreach(var r in paper.references){
					url += String.Format("&paper[references_attributes][{0}][id]={1}", r.id, r.id);
					url += String.Format("&paper[references_attributes][{0}][paper_id]={1}", r.id, paper.id);
					url += String.Format("&paper[references_attributes][{0}][reference]={1}", r.id, r.reference);
					url += String.Format("&paper[references_attributes][{0}][_delete]={1}", r.id, r.delete);// i guess set this if to be deleted?
				}

				Client.OpenURL (url, MethodEnum.PUT, true);
			}



		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
//			var user = ParseResponse<UserDTO> ();
//			DispatchSuccess (null);

			DispatchSuccess (new PaperSavedEventArgs (ParseResponse<PaperNode> ()));
		}

		#endregion

		public class PaperSavedEventArgs:EventArgs
		{
			public readonly Paper Paper;

			public PaperSavedEventArgs (PaperNode node)
			{
				Paper = node.paper;
			}
		}
	}
}

