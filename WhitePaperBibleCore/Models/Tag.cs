using System;
using System.Runtime.Serialization;

namespace WhitePaperBibleCore.Models
{
	[DataContract(Name = "tag")]
    public class Tag
    {
        [DataMember(Name = "permalink")] 
        public string permalink { get; set; }
	}
}

