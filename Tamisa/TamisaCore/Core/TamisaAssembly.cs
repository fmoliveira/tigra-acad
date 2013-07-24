using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tamisa.Core
{
    /// <summary>
    /// Defines an assembly as compatible with Tamisa.
    /// </summary>
    public abstract class TamisaAssembly
    {
        /// <summary>
        /// References assembly info.
        /// </summary>
        private AssemblyName CurrentAssembly
        {
            get { return Assembly.GetExecutingAssembly().GetName(); }
        }

        /// <summary>
        /// Name of the module.
        /// </summary>
        public String Name
        {
            get { return this.CurrentAssembly.Name; }
        }

        /// <summary>
        /// Version of the module.
        /// </summary>
        public Version Version
        {
            get { return this.CurrentAssembly.Version; }
        }

        /// <summary>
        /// Name of the developer.
        /// </summary>
        public String DeveloperName
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// E-mail address of the developer.
        /// </summary>
        public abstract String DeveloperEmail { get; set; }

        /// <summary>
        /// Website of the developer.
        /// </summary>
        public abstract String DeveloperWebsite { get; set; }
    }
}
