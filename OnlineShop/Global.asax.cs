using OnlineShop.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineShop.Models.Db;
using System.Web;

namespace OnlineShop
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			Database.SetInitializer(new DbInitializer());
        }

        public static ContextRepository ContextRepository
        {
            get
            {
                if (!HttpContext.Current.Items.Contains("_EntityContext"))
                {
                    HttpContext.Current.Items.Add("_EntityContext", new ContextRepository());
                }
                return HttpContext.Current.Items["_EntityContext"] as ContextRepository;
            }
        }

        protected virtual void Application_BeginRequest()
        {
            HttpContext.Current.Items["_EntityContext"] = new ContextRepository();
        }

        protected virtual void Application_EndRequest()
        {
            var entityContext = HttpContext.Current.Items["_EntityContext"] as ContextRepository;

            if (entityContext != null)
                entityContext.Dispose();
        }

    }
}
