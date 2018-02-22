using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.BE;
using System.Net.Http;
using Newtonsoft.Json;

namespace IdentityServer.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _clientIdentityAPI = new HttpClient();

        private readonly IEnumerable<User> _users;

        public UserRepository()
        {
            _users = JsonConvert.DeserializeObject<List<User>>(getAllUsers().Result);
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                string userCredentials = username + password;
                return BCrypt.Net.BCrypt.Verify(userCredentials, user.Password);
            }
            return false;
        }

        public User FindBySubjectId(string subjectId)
        {
            return _users.FirstOrDefault(x => x.Id.ToString() == subjectId);
        }

        public User FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<string> getAllUsers()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://localhost:5001/api/login/");
                return response;
            }
        }
    }
}
