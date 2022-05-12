using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.Models;

namespace Highway_to_heaven.Services
{
    public class UserService
    {
        private readonly HIGHWAY_TO_HEAVENContext context;

        public UserService(HIGHWAY_TO_HEAVENContext context)
        {
            this.context = context;
        }

        public User GetUserByName(string name)
        {
            return this.context.Users.FirstOrDefault(t => t.Login.Equals(name));
        }
    }
}
