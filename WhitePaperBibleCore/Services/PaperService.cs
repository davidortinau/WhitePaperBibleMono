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
			
//			var response = client.Execute(request);
//			if(string.IsNullOrWhiteSpace(response.Content) || response.StatusCode != System.Net.HttpStatusCode.OK) {
//				failure(response.ErrorMessage );
//			}else{
//				success( SimpleJson.SimpleJson.DeserializeObject<List<PaperNode>>(response.Content) );
//			}

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
		
//		public void GetPapers()
//		{
//			var request = new RestRequest("papers.json?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };
//			var response = Client.Execute(request);
//			if(string.IsNullOrWhiteSpace(response.Content) || response.StatusCode != System.Net.HttpStatusCode.OK) {
//			    return null;
//			}
//			rxTerm = DeserializeRxTerm(response.Content);
//		}
		
//		public void GetPapers(Action<List<PaperNode>> success, Action<string> failure)
//		{
//			var request = (HttpWebRequest)WebRequest.Create (baseuri + "papers.json?caller=wpb-iPhone");
//     		request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
//     		using(var response = (HttpWebResponse)request.GetResponse()) {
//          		using(var streamReader = new StreamReader(response.GetResponseStream())) {
//                	success( JsonValue.Load(streamReader) );
//		          }
//		     }	
//			
//			failure( ResponseStatus.Error.ToString() );
//		}

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

