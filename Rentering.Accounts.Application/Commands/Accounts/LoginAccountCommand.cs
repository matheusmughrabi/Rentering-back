﻿using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands.Accounts
{
    public class LoginAccountCommand : ICommand
    {
        public LoginAccountCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}