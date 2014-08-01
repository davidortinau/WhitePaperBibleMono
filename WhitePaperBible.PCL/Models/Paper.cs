using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using WhitePaperBible.Core.Helpers;

namespace WhitePaperBible.Core.Models
{
    [DataContract(Name = "paper")]
    public class Paper
    {
        [DataMember(Name = "permalink")] 
        public string permalink { get; set; }

        [DataMember(Name = "url_title")] 
        public string url_title { get; set; }

        [DataMember(Name = "updated_at")] 
        public DateTime updated_at { get; set; }

        [DataMember(Name = "title")] 
        public string title { get; set; }
        //public bool public { get; set; }

        [DataMember(Name = "featured")] 
        public bool? featured { get; set; }

        [DataMember(Name = "id")] 
        public int id { get; set; }

        [DataMember(Name = "description")] 
        public string description { get; set; }

        [DataMember(Name = "user_id")] 
        public int user_id { get; set; }

        [DataMember(Name = "view_count")] 
        public int view_count { get; set; }

        [DataMember(Name = "created_at")] 
        public DateTime created_at { get; set; }

		private List<Reference> _references;
		public List<Reference> references {
			get {
				return _references;
			}
			set {
				_references = value;
				HtmlContent = generateHtmlContent();
			}
		}

		private string _htmlContent;
		public string HtmlContent {
			get {
				return _htmlContent;
			}
			set {
				_htmlContent = value;
			}
		}

		public string ToPlainText()
		{
			var txt = "";
			txt +=	title + Environment.NewLine;
			txt += description + Environment.NewLine;

			if (references != null) {
				foreach (var reference in references) {
					txt += HtmlToText.ConvertHtml(reference.content) + Environment.NewLine;
				}
			}

			return txt;
		}

		private string generateHtmlContent()
		{
			var html = "<style type='text/css'>body { color: #000000; background-color: white; font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; padding-bottom: 50px; } h1, h2, h3, h4, h5, h6 { padding: 0px; margin: 0px; font-style: normal; font-weight: normal; } h2 { font-family: 'HelveticaNeue', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; margin-bottom: 20px; padding-bottom: 0px; } h4 { font-size: 16px; } p { font-family: Helvetica, Verdana, Arial, sans-serif; line-height:1.5; font-size: 16px; } .esv-text { padding: 0 0 10px 0; }</style>";
			html +=	"<h1>" + title + "</h1>";
			html += "<h2>" + description + "</h2>";
			if (references != null) {
				foreach (var reference in references) {
					html += reference.content;
				}
			}
			return html;
		}
    }
}
