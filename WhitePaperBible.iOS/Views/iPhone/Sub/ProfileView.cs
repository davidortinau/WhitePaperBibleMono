using System;
using System.Text;
using MonoTouch.UIKit;
using System.Drawing;
using WhitePaperBible.iOS.UI;
using WhitePaperBible.iOS.Managers;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using System.Threading.Tasks;
using System.Net.Http;
using MonoTouch.Foundation;
using WhitePaperBible.Core.Views;
using System.Web;
using System.Security.Cryptography;
using System.Linq;

namespace WhitePaperBible.iOS
{
	public class ProfileView:UIView, IMediatorTarget, IProfileView
	{
		UILabel NameTxt;
		UIImageView Avatar;
		UIButton EditBtn;

		public Invoker GoToEdit {
			get;
			private set;
		}

		public ProfileView () : base (new RectangleF (0, 0, 320, 100))
		{
			this.BackgroundColor = UIColor.Clear.FromHex(0xF8F8F8);

			GoToEdit = new Invoker ();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CreateView ();

//			DI.RequestMediator (this);
		}




		void CreateView ()
		{
			// avatar on left
			Avatar = new UIImageView (new RectangleF(15, 15, 60, 60));
			AddSubview (Avatar);

			// name label
			NameTxt = new UILabel (new RectangleF (15, 20, 300, 30));//90
			NameTxt.Text = "";
			AddSubview (NameTxt);

			// edit button
			EditBtn = new UIButton (UIButtonType.RoundedRect);
			EditBtn.SetTitle ("Edit Profile", UIControlState.Normal);
			EditBtn.Frame = new RectangleF (15, 50, 200, 30);//90
			EditBtn.BackgroundColor = UIColor.Clear;
			EditBtn.SetTitleColor (UIColor.DarkGray, UIControlState.Normal); 
			EditBtn.Font = UIFont.FromName ("Helvetica", 12);
			EditBtn.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
//			EditBtn.SetTitleColor (TNTStyles.DarkGray, UIControlState.Highlighted); 
			AddSubview (EditBtn);

			EditBtn.TouchUpInside += (object sender, EventArgs e) => {
				GoToEdit.Invoke();
			};

		}

		async public void SetUserProfile(AppUser user)
		{
			NameTxt.Text = user.Name;

			var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes (user.Email.Trim ()));
			var hashString = string.Join ("", hash.Select (x => x.ToString ("x2")));
			var imgUrl = String.Format ("http://gravatar.com/avatar/{0}.png?s=60&r=PG", hashString);
			Console.WriteLine ("Avatar URL: {0}", imgUrl);
//			Avatar.Image = await LoadImage (imgUrl);  TODO figure out why this isn't working

		}

		public async Task<UIImage> LoadImage (string imageUrl)
		{
			var httpClient = new HttpClient();

			Task<byte[]> contentsTask = httpClient.GetByteArrayAsync (imageUrl);

			// await! control returns to the caller and the task continues to run on another thread
			var contents = await contentsTask;

			// load from bytes
			return UIImage.LoadFromData (NSData.FromArray (contents));
		}
	}
}

