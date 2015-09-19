using System.Runtime.Serialization;
using System;

namespace WhitePaperBible.Core.Models
{
	public class UserDTO
	{
		[DataMember(Name = "user")] 
		public AppUser User { get; set; }
	}

	[DataContract(Name = "user")]
	public class AppUser
	{
		[DataMember(Name = "id")] 
		public int ID { get; set; }

		[DataMember(Name = "name")] 
		public string Name { get; set; }

		[DataMember(Name = "email")] 
		public string Email { get; set; }

		[DataMember(Name = "bio")] 
		public string Bio { get; set; }

		[DataMember(Name = "website")] 
		public string Website { get; set; }

		[DataMember(Name = "role")] 
		public string Role { get; set; }

//		[DataMember(Name = "updated_at")] 
//		public DateTime Updated { get; set; }
//
//		[DataMember(Name = "created_at")] 
//		public DateTime Created { get; set; }
//
//		[DataMember(Name = "crypted_password")] 
//		public string crypted_password { get; set; }
//
//		[DataMember(Name = "identity_url")] 
//		public string identity_url { get; set; }
//
//		[DataMember(Name = "openid_identifier")] 
//		public string openid_identifier { get; set; }
//
//		[DataMember(Name = "password_salt")] 
//		public string password_salt { get; set; }
//
//		[DataMember(Name = "perishable_token")] 
//		public string perishable_token { get; set; }
//
//		[DataMember(Name = "persistence_token")] 
//		public string persistence_token { get; set; }
//
//		[DataMember(Name = "remember_token")] 
//		public string remember_token { get; set; }
//
//		[DataMember(Name = "remember_token_expires_at")] 
//		public string remember_token_expires_at { get; set; }
//
//		[DataMember(Name = "twitter_id")] 
//		public string twitter_id { get; set; }
//
//		[DataMember(Name = "twitter_profile_image_url")] 
//		public string twitter_profile_image_url { get; set; }
//
//		[DataMember(Name = "twitter_screen_name")] 
//		public string twitter_screen_name { get; set; }
//
//		[DataMember(Name = "twitter_secret")] 
//		public string twitter_secret { get; set; }
//
//		[DataMember(Name = "twitter_token")] 
//		public string twitter_token { get; set; }

		[DataMember(Name = "username")] 
		public string username { get; set; }

		public string password {
			get;
			set;
		}

		public string passwordConfirmation {
			get;
			set;
		}

		public void Update (AppUser user)
		{
			this.Name = user.Name;
			this.Bio = user.Bio;
			this.Website = user.Website;
			this.Email = user.Email;
			this.username = user.username;

		}
	}
}

