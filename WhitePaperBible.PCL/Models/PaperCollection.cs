using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace WhitePaperBible.Core.Models
{
	[DataContract]
    public class PaperCollection
	{
		[DataMember(Name = "title")]
		public String Title { get; set; }
		
		[DataMember(Name = "paper")] 
		public List<Paper> Papers { get; set; }   
	}
}
