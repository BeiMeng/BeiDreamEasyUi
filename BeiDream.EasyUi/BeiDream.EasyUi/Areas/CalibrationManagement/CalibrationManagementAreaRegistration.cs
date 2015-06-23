using System.Web.Mvc;

namespace BeiDream.EasyUi.Areas.CalibrationManagement
{
    public class CalibrationManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CalibrationManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CalibrationManagement_default",
                "CalibrationManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
