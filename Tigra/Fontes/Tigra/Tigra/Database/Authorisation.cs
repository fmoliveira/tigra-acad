using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra.Database
{
    public class Authorisation
    {
        public enum Modules : byte
        {
            Admin = 1,
            Requirements = 2,
            UseCases = 3,
            TestCases = 4
        }

        public enum AdminSubModules : byte
        {
            Cells = 1,
            Teams = 2,
            Roles = 3,
            Templates = 4,
            Defaults = 5,
            Preferences = 6,
            Settings = 7
        }

        public enum AuthTypes : byte
        {
            Index = 1,
            Details = 2,
            Create = 3,
            Edit = 4,
            Delete = 5,
            SelfRate = 6
        }
    }
}