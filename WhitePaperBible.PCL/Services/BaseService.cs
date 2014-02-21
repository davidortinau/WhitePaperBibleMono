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

	public class BaseService:IInjectingTarget, IBaseService
	{
		[Inject]
		public IJSONWebClient Client;

		public event EventHandler Success = delegate{};
		public event EventHandler Fault = delegate{};

		public BaseService ()
		{
			DIUtil.InjectProps (this);
			AddEventListeners ();
		}

		public BaseService (IJSONWebClient webClient)
		{
			Client = webClient;
			AddEventListeners ();
		}

		protected virtual void AddEventListeners ()
		{
			Client.RequestComplete += HandleSuccess;
			Client.RequestError += (object sender, EventArgs e) => Fault (this, new ServiceFaultEventArgs (Client.ResponseText));
		}

		protected virtual TParseType ParseResponse<TParseType> () where TParseType:class
		{
			return JsonConvert.DeserializeObject<TParseType> (Client.ResponseText);
		}

		protected virtual void DispatchSuccess (EventArgs args)
		{
			Success (this, args);
		}

		protected virtual void HandleSuccess (object sender, EventArgs args)
		{
			throw(new NotImplementedException ());
		}
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

