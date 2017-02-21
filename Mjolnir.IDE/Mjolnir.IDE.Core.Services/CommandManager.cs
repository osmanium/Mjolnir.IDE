using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mjolnir.IDE.Core.Services
{
    /// <summary>
    /// The command manager class where you can register and query for a command
    /// </summary>
    public sealed class CommandManager : ICommandManager
    {
        /// <summary>
        /// The dictionary which holds all the commands
        /// </summary>
        private readonly Dictionary<string, ICommand> _commands;

        /// <summary>
        /// Command manager constructor
        /// </summary>
        public CommandManager()
        {
            _commands = new Dictionary<string, ICommand>();
        }

        #region ICommandManager Members

        /// <summary>
        /// Register a command with the command manager
        /// </summary>
        /// <param name="name">The name of the command</param>
        /// <param name="command">The command to register</param>
        /// <returns>true if a command is registered, false otherwise</returns>
        public bool RegisterCommand(string name, ICommand command)
        {
            if (_commands.ContainsKey(name))
                throw new Exception("Command " + name + " already exists !");

            _commands.Add(name, command);
            return true;
        }

        /// <summary>
        /// Gets the command if registered with the command manager
        /// </summary>
        /// <param name="name">The name of the command</param>
        /// <returns>The command if available, null otherwise</returns>
        public ICommand GetCommand(string name)
        {
            if (_commands.ContainsKey(name))
                return _commands[name];
            return null;
        }

        /// <summary>
        /// Calls RaiseCanExecuteChanged on all Delegate commands available in the command manager
        /// </summary>
        public void Refresh()
        {
            foreach (var keyValuePair in _commands)
            {
                if (keyValuePair.Value is DelegateCommand)
                {
                    var c = keyValuePair.Value as DelegateCommand;
                    c.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion
    }
}