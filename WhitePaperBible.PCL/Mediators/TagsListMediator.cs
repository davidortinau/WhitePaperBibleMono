using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class TagsListMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;
		[Inject]
		public GetTagsInvoker GetTags;
		[Inject]
		public TagsReceivedInvoker TagsReceived;
		ITagsListView Target;

		public TagsListMediator (ITagsListView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.Filter, HandleFilter);
			InvokerMap.Add (Target.OnTagSelected, HandlerPaperSelected);
			InvokerMap.Add (TagsReceived, (object sender, EventArgs e) => SetTags ());

			Target.SearchPlaceHolderText = "Search Tags";

			SetTags ();

		}

		void HandleFilter (object sender, EventArgs e)
		{
			if (AppModel.Tags != null) {
				Target.SetTags (AppModel.FilterTags (Target.SearchQuery));
			}
		}

		void HandlerPaperSelected (object sender, EventArgs e)
		{
			AppModel.CurrentTag = Target.SelectedTag;
		}

		public void SetTags ()
		{
			if (AppModel.Tags != null && AppModel.Tags.Count > 0) {
				Target.SetTags (AppModel.Tags);
			} else {
				GetTags.Invoke ();
			}
		}
	}
}