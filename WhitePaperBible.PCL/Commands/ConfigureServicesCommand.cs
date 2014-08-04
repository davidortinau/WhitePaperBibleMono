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
			DI.MapClassToInterface<AuthenticateUserService, IAuthenticateUserService> ();
			DI.MapClassToInterface<GetFavoritesService, IGetFavoritesService> ();
			DI.MapClassToInterface<GetMyPapersService, IGetMyPapersService> ();
			DI.MapClassToInterface<GetUserProfileService, IGetUserProfileService> ();
			DI.MapClassToInterface<SaveUserProfileService, ISaveUserProfileService> ();
			DI.MapClassToInterface<BibleSearchService, IBibleSearchService> ();
			DI.MapClassToInterface<SaveFavoriteService, ISaveFavoriteService> ();
			DI.MapClassToInterface<SavePaperService, ISavePaperService> ();
			DI.MapClassToInterface<SaveReferenceService, ISaveRefereceService> ();
		}
	}
}