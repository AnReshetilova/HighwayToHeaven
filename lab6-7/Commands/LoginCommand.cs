using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab6_7.Services;
using lab6_7.Models;

namespace lab6_7.Commands
{
    class LoginCommand : CommandBase
    {
        private readonly Action<object> execute; // тк с readonly быстрее работает
        private readonly Func<object, bool> canExecute;

        public override void Execute(object parameter) => execute(parameter);

        public LoginCommand(Action<object> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(Execute));
        }
    }
}
