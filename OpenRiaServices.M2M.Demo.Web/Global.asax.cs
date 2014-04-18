using System;
using System.Data.Entity;
using System.Web;
using OpenRiaServices.M2M.Demo.Web.Model;

namespace OpenRiaServices.M2M.Demo.Web
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DogTrainerModel>());

        }
    }
}