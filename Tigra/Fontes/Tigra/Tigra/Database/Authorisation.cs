using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra.Database
{
    public class Authorisation
    {
        public enum Modules : short
        {
            Cells,
            Teams,
            Roles,
            Templates,
            Defaults,
            Preferences,
            Settings,
            Requirements,
            UseCases,
            TestCases
        }

        private const byte AUTH_INDEX = 1;
        private const byte AUTH_DETAILS = 2;
        private const byte AUTH_CREATE = 3;
        private const byte AUTH_EDIT = 4;
        private const byte AUTH_DELETE = 5;
        private const byte AUTH_SELF_RATE = 6;

        public enum Authorisations : byte
        {
            Index = AUTH_INDEX,
            Details = AUTH_DETAILS,
            Create = AUTH_CREATE,
            Edit = AUTH_EDIT,
            Delete = AUTH_DELETE,
            SelfRate = AUTH_SELF_RATE
        }
    }
}