using System;
using System.Runtime.Serialization;

namespace WhitePaperBible.Core.Models
{
	[DataContract(Name = "tag")]
    public class Tag
    {
		[DataMember(Name = "name")] 
		public string name { get; set; }

		[DataMember(Name = "count")] 
		public Int32 count { get; set; }

		[DataMember(Name = "id")] 
		public Int32 id { get; set; }
	}
}

