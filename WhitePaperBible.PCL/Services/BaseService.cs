using System;
using MonkeyArms;
using Newtonsoft.Json;

namespace WhitePaperBible.Core.Services
{
	public interface IBaseService
	{
		event EventHandler Success;
		event EventHandler Fault;
	}

	public abstract class BaseService:IInjectingTarget, IBaseService
	{
		private IJSONWebClient _client;

		[Inject]
		public IJSONWebClient Client {
			get {
				return _client;
			}
			set {
				_client = value;
				AddEventListeners ();
			}
		}

		public event EventHandler Success = delegate{};
		public event EventHandler Fault = delegate{};

		protected BaseService ()
		{
			if (DI.CanResolve<IJSONWebClient> ()) {
				DIUtil.InjectProps (this);
			}
		}

		protected virtual void AddEventListeners ()
		{
			_client.RequestComplete += HandleSuccess;
			_client.RequestError += (object sender, EventArgs e) => Fault (this, new ServiceFaultEventArgs (_client.ResponseText));
		}

		protected virtual TParseType ParseResponse<TParseType> () where TParseType:class
		{
			return JsonConvert.DeserializeObject<TParseType> (_client.ResponseText);
		}

		protected virtual void DispatchSuccess (EventArgs args)
		{
			Success (this, args);
		}

		protected abstract void HandleSuccess (object sender, EventArgs args);
	}

	public class ServiceFaultEventArgs:EventArgs
	{
		public readonly string ErrorResponse;

		public ServiceFaultEventArgs (string errorMessage)
		{
			ErrorResponse = errorMessage;
		}
	}
}

