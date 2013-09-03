using System.Web.Mvc;
using BootstrapSupport;
using Tigra.Database;
using System.Data.Entity.Validation;
using System.Text;
using System.Data.Entity;
using System;

namespace Tigra.Controllers
{
    public class BootstrapBaseController: Controller
    {
        /// <summary>
        /// Sets a warning message.
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            TempData.Add(Alerts.WARNING, message);
        }

        /// <summary>
        /// Sets a success message.
        /// </summary>
        /// <param name="message"></param>
        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        /// <summary>
        /// Sets an information message.
        /// </summary>
        /// <param name="message"></param>
        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        /// <summary>
        /// Sets an error message.
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            TempData.Add(Alerts.DANGER, message);
        }

        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        public int SaveChanges(DbContext context)
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
