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
        private IEnumerable<User> _users;

        //Instantiates our singleton
        public UserRepository()
        {
            _users = JsonConvert.DeserializeObject<List<User>>(getAllUsers().Result);
        }

        //Validates username and password using BCrypt.
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

        //Searches our list and returns subject with correct ID
        public User FindBySubjectId(string subjectId)
        {
            try
            {
                return _users.FirstOrDefault(x => x.Id.ToString() == subjectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Searches our list and returns subject with correct UserName
        public User FindByUsername(string username)
        {
            try
            {
                _users = JsonConvert.DeserializeObject<List<User>>(getAllUsers().Result);
                return _users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get all users via API, using a HttpClient
        private async Task<string> getAllUsers()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://localhost:5001/api/login/");
                    return response;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task getUsers()
        {
            _users = JsonConvert.DeserializeObject<List<User>>(getAllUsers().Result);
        }
    }
}
