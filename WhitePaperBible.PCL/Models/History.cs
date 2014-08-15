using System;
using System.Runtime.Serialization;

namespace WhitePaperBible.Core.Models
{
	[DataContract(Name = "paper_history")]
    public class History
    {
		[DataMember(Name = "view_count")] 
		public Int32 view_count { get; set; }

		[DataMember(Name = "paper_id")] 
		public Int32 paper_id { get; set; }
	}
}

