using System;
using System.Runtime.Serialization;

namespace WhitePaperBible.Core.Models
{
	[DataContract(Name = "tag")]
    public class Tag
    {
        [DataMember(Name = "permalink")] 
        public string permalink { get; set; }
	}
}

