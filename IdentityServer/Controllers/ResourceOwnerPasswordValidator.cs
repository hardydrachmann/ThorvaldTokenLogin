using IdentityModel;
using IdentityServer.BE;
using IdentityServer.DAL;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly HttpClient _clientIdentityAPI = new HttpClient();
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //this is used to validate your user account with provided grant at /connect/token
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.UserName != null)
            {
                if (_userRepository.ValidateCredentials(context.UserName, context.Password))
                {
                    var user = _userRepository.FindByUsername(context.UserName);
                    context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: "custom",
                        claims: GetUserClaims(user));
                }
            }
            return Task.FromResult(0);
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