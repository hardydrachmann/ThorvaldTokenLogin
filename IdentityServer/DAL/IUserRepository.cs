using IdentityServer.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.DAL
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);

        User FindBySubjectId(string subjectId);

        User FindByUsername(string username);
    }
}
