using System;
using Foundation;
using WhitePaperBible.WatchShared;
using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using WhitePaperBible.WatchShared.MessageParams;
using WhitePaperBible.Core.Repositories;

namespace WhitePaperBible.iOS
{
	static public class WatchAppManager
	{
		static async public void ProcessMessage(NSDictionary userInfo, Action<NSDictionary> reply)
		{
			Console.WriteLine("ProcessMessage");
			NSNumber messageId = userInfo.ObjectForKey(new NSString(WatchConstants.KEY_ACTION)) as NSNumber;
			WatchAction action = (WatchAction)messageId.Int32Value;
			Console.WriteLine("IPC Message " + action);

			switch (action)
			{
			case WatchAction.Favorites:
				{
					WatchMessage<PapersResponseParams> responseMessage = new WatchMessage<PapersResponseParams>();

					var repo = DI.Get<IFavoritesRepository>();
					responseMessage.Params.Papers = await repo.FetchAll();
					responseMessage.ErrorCode = MessageResponseStatus.Success;
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

