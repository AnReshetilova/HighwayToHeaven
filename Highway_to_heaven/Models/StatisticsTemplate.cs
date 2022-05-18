using Highway_to_heaven.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highway_to_heaven.Models
{
    class StatisticsTemplate
    {
        public string Username { get; set; }
        public string Score { get; set; }

        public StatisticsTemplate(User user, UserService userService)
        {
            Username = user.Name;
            Score = userService.GetUserScore(user.Login).ToString();
        }
    }
}
