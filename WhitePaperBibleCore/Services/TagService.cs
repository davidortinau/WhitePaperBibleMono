using System;
using System.Collections.Generic;
using RestSharp;
using WhitePaperBibleCore.Models;
using Newtonsoft.Json;


namespace WhitePaperBibleCore.Services
{
	public class TagService
	{
		public TagService ()
		{
		}
		
		public void GetTags(Action<List<TagNode>> success, Action<string> failure)
        {
            var client = new RestClient(Constants.BASE_URI);

            var request = new RestRequest("tag.json?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };
			
            client.ExecuteAsync(request, (response, async) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    failure(response.ErrorMessage);
                }
                else
                {
                    success( JsonConvert.DeserializeObject<List<TagNode>>(response.Content) );
                }
            });
        }
		
		public void GetPapersByTag(int tagId, Action<List<PaperNode>> success, Action<string> failure)
        {
            var client = new RestClient(Constants.BASE_URI);

            var request = new RestRequest("papers/tagged/" + tagId.ToString() + "?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };

            client.ExecuteAsync(request, (response, async) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    failure(response.ErrorMessage);
                }
                else
                {
                    success( JsonConvert.DeserializeObject<List<PaperNode>>(response.Content) );
                }
            });
        }
	}
}

