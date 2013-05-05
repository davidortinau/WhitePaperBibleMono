using System;
using System.Runtime.Serialization;

namespace WhitePaperBibleCore.Models
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
    }
}
