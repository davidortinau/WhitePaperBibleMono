using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WhitePaperBible.Core.Services
{
	public interface IGetPaperReferencesService:IBaseService
	{
		Task<List<ReferenceNode>> Execute (int paperID);
	}

	public class GetPaperReferencesService:BaseService, IGetPaperReferencesService
	{
		public GetPaperReferencesService ()
		{
		}

		public async Task<List<ReferenceNode>> Execute (int paperID)
		{
			await Client.OpenURL (Constants.BASE_URI + "papers/" + paperID.ToString () + "/references.json?caller=wpb-iPhone");
			return JsonConvert.DeserializeObject<List<ReferenceNode>> (Client.ResponseText);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetPaperReferencesServiceEventArgs (JsonConvert.DeserializeObject<List<ReferenceNode>> (Client.ResponseText)));

		}

		#endregion
	}

	public class GetPaperReferencesServiceEventArgs:EventArgs
	{
		public readonly List<ReferenceNode> References;

		public GetPaperReferencesServiceEventArgs (List<ReferenceNode> refs)
		{
			References = refs;
		}
	}
}

