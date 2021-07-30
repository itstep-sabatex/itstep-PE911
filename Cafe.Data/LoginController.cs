using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data
{
    public class LoginController<Context> : IUserLoginController
                    where Context : BaseDbContext ,new() 
    {
        AccessLevel _userAccesLevel;
        const int MaxLoginCounted = 4; 
        int _loginCounter = MaxLoginCounted;
        public LoginController(AccessLevel userAccesLevel)
        {
            _userAccesLevel = userAccesLevel;
        }

        public IUser[] Users
        {
            get
            {
                using (var context = new Context())
                {
                    var result = context.UserAccesLevels.Where(s => s.AccessLevel == _userAccesLevel)
                                                  .Include(i => i.User)
                                                  .Select(sel => sel.User)
                                                  .ToArray();
                    return result;
                }

            } 
        }

        public int LoginCounter => _loginCounter;

        public bool TryLogin(int userId, string password)
        {
            if (_loginCounter <=0) return false;
            using (var context = new Context())
            {
                if (context.Users.SingleOrDefault(s => s.Id == userId && s.Password == password) == null)
                {
                    _loginCounter--;
                    return false;
                }
                else
                {
                    _loginCounter = MaxLoginCounted;
                    return true;
                }

            }
        }
    }
}
