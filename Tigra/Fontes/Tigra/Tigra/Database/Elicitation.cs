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
    
    public partial class Elicitation
    {
        public int ElicitationID { get; set; }
        public int CellID { get; set; }
        public int UserID { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
    
        public virtual Cell Cell { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
