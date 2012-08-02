using System;
using System.Data.Entity;
using System.Web;
using RIAServices.M2M.Demo.Web.Model;

namespace RIAServices.M2M.Demo.Web
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DogTrainerModel>());

        }
    }
}