using IdentityModel;
using IdentityServer.BE;
using IdentityServer.DAL;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                        context.IssuedClaims = GetUserClaims(user).ToList();
                    }
                }
            }
            catch (Exception e) { }
        }

        // Check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userRepository.FindByUsername(context.Subject.Identity.Name);
            if (user != null)
            {
                context.IsActive = !user.IsDeleted;
            }
        }

        //build claims array from user data
        public static Claim[] GetUserClaims(User user)
        {
            List<Claim> claimList = new List<Claim>();

            claimList.Add(new Claim("user_id", user.Id.ToString()));
            claimList.Add(new Claim(JwtClaimTypes.Name, user.Firstname + " " + user.Lastname));
            claimList.Add(new Claim(JwtClaimTypes.GivenName, user.Firstname));
            claimList.Add(new Claim(JwtClaimTypes.FamilyName, user.Lastname));
            claimList.Add(new Claim(JwtClaimTypes.Email, user.Email));
            claimList.Add(new Claim(JwtClaimTypes.PreferredUserName, user.Username));

            //roles       
            foreach (var role in user.Roles)
            {
                claimList.Add(new Claim(JwtClaimTypes.Role, role.Name));
            }
            return claimList.ToArray();
        }
    }
}
