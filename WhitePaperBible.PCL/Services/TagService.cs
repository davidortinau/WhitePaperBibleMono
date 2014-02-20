using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using Newtonsoft.Json;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public class TagService
	{
		public TagService ()
		{
		}

		public void GetTags (Action<List<TagNode>> success, Action<string> failure)
		{
			IJSONWebClient client = DI.Get<IJSONWebClient> ();

			client.RequestComplete += (object sender, EventArgs e) => {
				success (JsonConvert.DeserializeObject<List<TagNode>> (client.ResponseText));
			};

			client.RequestError += (object sender, EventArgs e) => failure (client.ResponseText);

			client.OpenURL (Constants.BASE_URI + "tag.json?caller=wpb-iPhone");



		}

		public void GetPapersByTag (int tagId, Action<List<PaperNode>> success, Action<string> failure)
		{
			IJSONWebClient client = DI.Get<IJSONWebClient> ();

			client.RequestComplete += (object sender, EventArgs e) => {
				success (JsonConvert.DeserializeObject<List<PaperNode>> (client.ResponseText));
			};

			client.RequestError += (object sender, EventArgs e) => failure (client.ResponseText);

			client.OpenURL ("papers/tagged/" + tagId.ToString () + "?caller=wpb-iPhone");


		}
	}
}

