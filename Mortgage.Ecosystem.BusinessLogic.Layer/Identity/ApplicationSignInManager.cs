using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Identity
{
    public class ApplicationSignInManager : SignInManager<UserEntity>
    {
        public ApplicationSignInManager(UserManager<UserEntity> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<UserEntity> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<UserEntity>> logger, IAuthenticationSchemeProvider schemes,
            IUserConfirmation<UserEntity> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
            logger, schemes, confirmation)
        {
        }

        public override async Task<bool> CanSignInAsync(UserEntity user)
        {
            if (user.UserStatus == 0)
            {
                Logger.LogWarning(4, "User {userId} cannot sign in as the account is currently disabled.",
                    await UserManager.GetUserIdAsync(user));
                return false;
            }

            return await base.CanSignInAsync(user);
        }
    }
}