using OnlineShop.Mappers;
using OnlineShop.Models.Db;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineShop
{
    // Note: For instructions on enabling IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=301868
    public class App : System.Web.HttpApplication
    {
        private static CommonMapper _mapper;
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<AppContext>(null);//new DbInitializer());     //do no use it now with "real" remote DB
            JobScheduler.Start();
        }

        public static CommonMapper Mapper
        {
            get
            {
                if (_mapper == null)
                    _mapper = new CommonMapper();
                return _mapper;
            }
        }

        public static ContextRepository Rep
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