using MonkeyArms;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Commands
{
	public class ConfigureServicesCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			DI.MapClassToInterface<GetPapersService, IGetPapersService> ();
			DI.MapClassToInterface<GetTagsService, IGetTagsService> ();
			DI.MapClassToInterface<GetPapersByTagService, IGetPapersByTagService> ();
		}
	}
}

