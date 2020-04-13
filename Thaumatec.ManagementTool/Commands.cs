using Thaumatec.ManagementTool.WebClientGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Thaumatec.ManagementTool
{
    public class Commands
    {
        public bool IsValid { get; }

        private const string CLIENT_CMD = "client";
        private const string UPGRADE_CMD = "upgrade";
        private const string CREATE_CMD = "create";

        private readonly string _selectedCommand;
        private readonly string[] _parameters;

        private readonly List<string> _allCommands = new List<string>() { CLIENT_CMD, UPGRADE_CMD, CREATE_CMD };

        public Commands(string[] args)
        {
            if (args.Length != 0)
            {
                _parameters = args.Skip(1).ToArray();
            }
            _selectedCommand = args.FirstOrDefault();
        }

        public void Run()
        {
            switch (_selectedCommand)
            {
                case CLIENT_CMD:
                    WebClientGenerateSetup.GenerateWebClient();
                    break;
                default:
                    PrintHelp();
                    break;
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine($"Command not find: {_selectedCommand}");
            Console.WriteLine("Available commands:");
            foreach (var command in _allCommands)
            {
                Console.WriteLine(command);
            }
        }
    }
}
