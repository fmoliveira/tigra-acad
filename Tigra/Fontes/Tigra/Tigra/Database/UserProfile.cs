//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tigra.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public byte[] Picture { get; set; }
        public string Location { get; set; }
        public string Biography { get; set; }
        public string UserTheme { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
    }
}
