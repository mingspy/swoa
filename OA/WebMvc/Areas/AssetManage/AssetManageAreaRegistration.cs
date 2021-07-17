using System.Web.Mvc;

namespace WebMvc.Areas.AssetManage
{
    public class AssetManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AssetManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AssetManage_default",
                "AssetManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
