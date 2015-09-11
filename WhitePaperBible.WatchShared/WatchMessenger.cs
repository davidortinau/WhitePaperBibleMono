using System;
using System.Threading.Tasks;
using WatchKit;
using Foundation;

namespace WhitePaperBible.WatchShared
{
	public static class WatchMessenger
	{
		/// <summary>
		/// Requests the message.
		/// </summary>
		/// <returns>The message.</returns>
		/// <param name="type">Type.</param>
		/// <param name="requestMessage">Request message.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="U">The 2nd type parameter.</typeparam>
		static public Task<WatchMessage<T>> RequestMessage<T, U> (WatchAction type,WatchMessage<U> requestMessage) where U : WatchMessageParams, new() where T : WatchMessageParams, new()
		{
			return RequestMessage<T,U> (type, new WatchMessage<T>(), requestMessage);
		}

		/// <summary>
		/// Requests the message.
		/// </summary>
		/// <returns>The message.</returns>
		/// <param name="type">Type.</param>
		/// <param name="message">Message.</param>
		/// <param name="requestMessage">Request message.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="U">The 2nd type parameter.</typeparam>
		static public Task<WatchMessage<T>> RequestMessage<T, U> (WatchAction type, WatchMessage<T> responseMessage, WatchMessage<U> requestMessage) where U : WatchMessageParams, new() where T : WatchMessageParams, new()
		{
			var tcs = new TaskCompletionSource<WatchMessage<T>> ();

			WKInterfaceController.OpenParentApplication(PrepareMessage<U>(type, requestMessage),(NSDictionary userInfo,NSError error )=>{
				if(error != null)
				{
					tcs.TrySetException(new Exception(error.ToString()));
					return;
				}
				responseMessage.DecodeParams(userInfo);
				tcs.SetResult(responseMessage);
			});
			return tcs.Task;
		}	


		/// <summary>
		/// Prepares the message for sending over IPC from the watch extension.
		/// </summary>
		/// <returns>The message.</returns>
		/// <param name="messageType">Message type.</param>
		/// <param name="message">Message.</param>
		static public NSDictionary PrepareMessage<T> (WatchAction messageType, WatchMessage<T> requestMessage) where T : WatchMessageParams, new()
		{
			Console.WriteLine("PrepareMessage " + messageType);
			NSMutableDictionary dict = requestMessage.EncodeParams();

			dict.SetValueForKey(new NSNumber((int)messageType), new NSString(WatchConstants.KEY_ACTION));

			return dict;
		}
	}
}

