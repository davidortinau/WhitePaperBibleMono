using System;
using Foundation;
using WhitePaperBible.WatchShared;
using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using WhitePaperBible.WatchShared.MessageParams;

namespace WhitePaperBible.iOS
{
	static public class WatchAppManager
	{
		static public void ProcessMessage(NSDictionary userInfo, Action<NSDictionary> reply)
		{
			Console.WriteLine("ProcessMessage");
			NSNumber messageId = userInfo.ObjectForKey(new NSString(WatchConstants.KEY_ACTION)) as NSNumber;
			WatchAction action = (WatchAction)messageId.Int32Value;
			Console.WriteLine("IPC Message " + action);

			switch (action)
			{
			case WatchAction.Favorites:
				{
					// create response message
					WatchMessage<PapersResponseParams> responseMessage = new WatchMessage<PapersResponseParams>();

//					var mediator = DI.Get<FavoritesListMediator>();
//					mediator.GetFavorites
//
//					if (AM.Favorites != null && AM.Favorites != null) {
//						Target.SetPapers (AM.Favorites);
//					}else{
//						if (AM.IsLoggedIn) {
//							GetFavorites.Invoke ();
//						}
//					}

					responseMessage.ErrorCode = MessageResponseStatus.Success;

					List<Paper> papers = new List<Paper>();
					papers.Add(new Paper(){ title = "Abiding in Christ"});
					papers.Add(new Paper(){ title = "Believer's Authority"});
					papers.Add(new Paper(){ title = "Bible Scriptures About Work"});
					papers.Add(new Paper(){ title = "Bible Verses about Prospering"});
					papers.Add(new Paper(){ title = "Encouragement to do God's work"});
					papers.Add(new Paper(){ title = "Encouragement to Receive by Faith"});

					responseMessage.Params.Papers = papers;

					// reply to message
					reply(responseMessage.EncodeParams());
					break;
				}
			case WatchAction.Paper:
				{
//					// decode message params
//					IPCMessage<IPCParams> message = new IPCMessage<IPCParams>();
//					message.DecodeParams(userInfo);
//
//					// create response message
//					IPCMessage<IPCParams> responseMessage = new IPCMessage<IPCParams>();
//
//					// populate repsonse message
//					responseMessage.ErrorCode = ErrorCode.SomeError;
//
//					// reply to message
//					reply(responseMessage.EncodeParams());
					break;
				}
			}
		}
	}
}

