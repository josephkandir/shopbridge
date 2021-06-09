using Microsoft.EntityFrameworkCore;
using shopbridge.api.Context;
using shopbridge.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace shopbridge.api.Repositories.User
{
    public class UserRepo : IUser
    {
        private List<Models.User> _users = new List<Models.User>
        {
            new Models.User {
                Id = 1,
                FirstName = "Abc",
                LastName = "Xyz",
                Username = "admin",
                Password = "admin@123",
                Role = "admin",
            },
            new Models.User {
                Id = 1,
                FirstName = "Def",
                LastName = "Xyz",
                Username = "user",
                Password = "user@123",
                Role = "user",
            }
        };

        public async Task<IEnumerable<Models.User>> GetAsync()
        {
            if (_users == null)
                return null;

            var users = new List<Models.User>();

            await Task.Run(() => {
                for (var i = 0; i < _users.Count; i++)
                {
                    _users[i].Password = null;
                    users.Add(_users[i]);
                }
            });

            return users;
        }

        public Models.User GetAsync(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            if (user != null)
                user.Password = null;

            return user;
        }

        public Models.User GetByUsernamePasswordAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if(user != null)
                user.Password = null;

            return user;
        }
    }
}
