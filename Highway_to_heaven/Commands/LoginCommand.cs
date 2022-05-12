using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway_to_heaven.Services;
using Highway_to_heaven.Models;

namespace Highway_to_heaven.Commands
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
