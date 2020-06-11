using AisCodeChallenge.Web.MVC.Infrastructure;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AisCodeChallenge.Web.MVC
{
    public class MvcApplication : HttpApplication
    {

        private StartupLoadData startupLoadData = new StartupLoadData();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac
            DependencyRegister.Build();

            //AutoMapper
            AutoMapperConfig.RegisterConfiguration();

            //Clean directory on startup
            startupLoadData.CleanCurrentDirecoryData();

            //Call Method on AppStart to download last 10 files 
            var loadDataTask = startupLoadData.GetLatesActivity();
            loadDataTask.Wait();

        }
    }
}
