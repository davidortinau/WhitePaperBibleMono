using System;
using Foundation;
using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Repositories;
using WhitePaperBible.Core.Services;

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
					Console.WriteLine("Favorites");
					WatchMessage<PapersResponseParams> responseMessage = new WatchMessage<PapersResponseParams>();

					var repo = DI.Get<IFavoritesRepository>();
					Console.WriteLine("Favorites - Got Repo");

					try{
						responseMessage.Params.Papers = await repo.FetchAll();
					}catch(Exception ex){
						reply (new NSDictionary (
							"payload", "error " + ex.Message
						)
						);
						return;	
					}


					Console.WriteLine("Favorites - Count {0}", responseMessage.Params.Papers.Count);;
					responseMessage.ErrorCode = MessageResponseStatus.Success;
					reply(responseMessage.EncodeParams());
					break;
				}
			case WatchAction.Paper:
				{
					Console.WriteLine("Paper");

//					// decode message params
					WatchMessage<PaperRequestParams> message = new WatchMessage<PaperRequestParams>();
					message.DecodeParams(userInfo);
//
//					// create response message
					WatchMessage<PaperResponseParams> responseMessage = new WatchMessage<PaperResponseParams>();

					var repo = DI.Get<IPaperRepository>();
					if(repo != null){
						responseMessage.Params.Paper = await repo.FetchOne(message.Params.Paper);
						Console.WriteLine("WatchText IS? {0}", responseMessage.Params.Paper.WatchText);
						Console.WriteLine("Author: {0}, View Count: {1}", responseMessage.Params.Paper.Author.Name, responseMessage.Params.Paper.view_count);
						responseMessage.ErrorCode = MessageResponseStatus.Success;
					}else{
						responseMessage.Params.Paper = new Paper(){WatchText = "NO PAPER FOUND"};
						responseMessage.ErrorCode = MessageResponseStatus.Fail;
					}
					reply(responseMessage.EncodeParams());
					break;
				}
			}
		}
	}
}

