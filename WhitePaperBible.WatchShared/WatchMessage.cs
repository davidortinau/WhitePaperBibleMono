using System;
using Foundation;
using Newtonsoft.Json;

namespace WhitePaperBible.WatchShared
{
	/// <summary>
	/// IPC message.
	/// </summary>
	public class WatchMessage<T> where T : WatchMessageParams, new()
	{
		/// <summary>
		/// Gets or sets the error code.
		/// </summary>
		/// <value>The error code.</value>
		public MessageResponseStatus ErrorCode { get; set; }

		/// <summary>
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>The parameters.</value>
		public T Params { get; set; }

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public WatchMessage()
		{
			Params = new T();
			ErrorCode = MessageResponseStatus.Success; // default to success
		}

		/// <summary>
		/// Encodes the parameters.
		/// </summary>
		/// <returns>The parameters.</returns>
		public NSMutableDictionary EncodeParams()
		{
			NSMutableDictionary param = new NSMutableDictionary();
			string json = Serialise();
			Console.WriteLine("EncodeParams " + json);
			param.SetValueForKey(new NSString(json), new NSString(WatchConstants.KEY_PAYLOAD));
			param.SetValueForKey(new NSNumber((int)ErrorCode), new NSString(WatchConstants.KEY_ERROR));

			return param;
		}

		/// <summary>
		/// Decodes the parameters.
		/// </summary>
		/// <param name="userInfo">User info.</param>
		public void DecodeParams(NSDictionary userInfo)
		{
			try
			{
				NSString json = userInfo.ValueForKey(new NSString(WatchConstants.KEY_PAYLOAD)) as NSString;
				Console.WriteLine ("DecodeParams " + json);
				NSNumber errorCode = userInfo.ValueForKey(new NSString(WatchConstants.KEY_ERROR)) as NSNumber;
				ErrorCode = (MessageResponseStatus)errorCode.Int32Value;
				Deserialise(json);
			}
			catch(Exception exception)
			{
				Console.WriteLine("Failed to decode " + exception.Message);
			}
		}

		/// <summary>
		/// Serialise this instance.
		/// </summary>
		public string Serialise()
		{
			return JsonConvert.SerializeObject(Params);
		}

		/// <summary>
		/// Deserialise the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public void Deserialise(string json)
		{
			Params = JsonConvert.DeserializeObject<T>(json);
		}
	}
}

