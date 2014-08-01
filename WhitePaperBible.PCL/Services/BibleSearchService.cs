﻿using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using WhitePaperBible.Core.Enums;

namespace WhitePaperBible.Core.Services
{
	public interface IBibleSearchService:IBaseService
	{
		void Execute (string keywords, SearchScopeEnum scope);
	}

	public class BibleSearchService:BaseService, IBibleSearchService
	{
		public void Execute (string keywords, SearchScopeEnum scope)
		{
			if (scope == SearchScopeEnum.Reference) {
				Client.OpenURL (Constants.BASE_URI + String.Format ("search/by_reference.json?keywords={0}&caller=wpb-iPhone", keywords));
			}else if (scope == SearchScopeEnum.Keyword){
				Client.OpenURL (Constants.BASE_URI + String.Format ("search/by_keyword.json?keywords={0}&caller=wpb-iPhone", keywords));
			}else{
				Client.OpenURL (Constants.BASE_URI + String.Format ("search/by_phrase.json?keywords={0}&caller=wpb-iPhone", keywords));
			}
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new BibleSearchServiceEventArgs (ParseResponse<List<Reference>> ()));
		}

		#endregion
	}

	public class BibleSearchServiceEventArgs:EventArgs
	{
		public readonly List<Reference> Results;

		public BibleSearchServiceEventArgs (List<Reference> results)
		{
			Results = results;
		}
	}
}

