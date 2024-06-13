using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public Task<string> Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
