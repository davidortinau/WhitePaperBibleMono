using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using Newtonsoft.Json;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public class PaperService
	{
		public PaperService ()
		{
		}

		public void GetPapers (Action<List<PaperNode>> success, Action<string> failure)
		{
			IJSONWebClient webClient = DI.Get<IJSONWebClient> ();

			webClient.RequestComplete += (object sender, EventArgs e) => {

				List<PaperNode> nodes = JsonConvert.DeserializeObject<List<PaperNode>> (webClient.ResponseText);
				success (nodes);
			};

			webClient.RequestError += (object sender, EventArgs e) => failure (webClient.ResponseText);

			webClient.OpenURL (Constants.BASE_URI + "papers.json?caller=wpb-iPhone");

           
		}

		public void GetPaperReferences (int paperId, Action<List<ReferenceNode>> success, Action<string> failure)
		{
			IJSONWebClient client = DI.Get<IJSONWebClient> ();

			client.RequestComplete += (object sender, EventArgs e) => {
				success (JsonConvert.DeserializeObject<List<ReferenceNode>> (client.ResponseText));
			};

			client.RequestError += (object sender, EventArgs e) => failure (client.ResponseText);

			client.OpenURL (Constants.BASE_URI + "papers/" + paperId.ToString () + "/references.json?caller=wpb-iPhone");

		
		}
	}
}

