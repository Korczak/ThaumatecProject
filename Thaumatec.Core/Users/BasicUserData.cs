using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Users
{
    public class BasicUserData
    {
        public int Id { get; }
        public string Username { get; }

        public BasicUserData(int id, string username)
        {
            Id = id;
            Username = username;
        }

        public override string ToString()
        {
            return $"{Username}:{Id}";
        }

    }
}
