using IdentityServer.DAL;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class ProfileService : IProfileService
    {
        //services
        readonly ILogger Logger;
        readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository, ILogger<ProfileService> logger)
        {
            _userRepository = userRepository;
            Logger = logger;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    var user = _userRepository.FindByUsername(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        //set issued claims to return
                        context.IssuedClaims = ResourceOwnerPasswordValidator
                            .GetUserClaims(user).ToList();
                    }
                }
            }
            catch (Exception e) { }
        }

        // Check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userRepository.FindByUsername(context.Subject.Identity.Name);
            context.IsActive = user != null;
        }
    }
}
