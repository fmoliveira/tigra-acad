using System.Web.Mvc;
using BootstrapSupport;

namespace BootstrapMvcSample.Controllers
{
    public class BootstrapBaseController: Controller
    {
        public void Warning(string message)
        {
            TempData.Add(Alerts.WARNING, message);
        }

        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Add(Alerts.DANGER, message);
        }
    }
}
