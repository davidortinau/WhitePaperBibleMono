using System;
using System.Collections.Generic;
using RestSharp;
using WhitePaperBibleCore.Models;

namespace WhitePaperBibleCore.Services
{
	public class PaperService
	{
		public PaperService()
        {
        }

        public void GetPapers(Action<List<PaperNode>> success, Action<string> failure)
        {
            var client = new RestClient(Constants.BASE_URI);
            var request = new RestRequest("papers.json?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };

			client.ExecuteAsync(request, (response, async) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    failure(response.ErrorMessage);
                }
                else
                {
                    success( SimpleJson.SimpleJson.DeserializeObject<List<PaperNode>>(response.Content) );
                }
            });
        }

		public void GetPaperReferences(int paperId, Action<List<ReferenceNode>> success, Action<string> failure)
        {
            var client = new RestClient(Constants.BASE_URI);

            var request = new RestRequest("papers/" + paperId.ToString() + "/references.json?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };

            client.ExecuteAsync(request, (response, async) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    failure(response.ErrorMessage);
                }
                else
                {
                    success( SimpleJson.SimpleJson.DeserializeObject<List<ReferenceNode>>(response.Content) );
                }
            });
        }
    }

	
}

