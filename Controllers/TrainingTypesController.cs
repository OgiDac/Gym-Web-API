using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeretanaWebApi.Controllers
{
    public class TrainingTypesController : ApiController
    {
        public IHttpActionResult Get()
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/training_type.txt");
            string line;
            string fileText = "";

            if (File.Exists(fullPath) && File.ReadAllText(fullPath).Length > 0)
            {
                StreamReader sr = new StreamReader(fullPath);

                line = sr.ReadLine();

                while (line != null)
                {

                    fileText += line;

                    line = sr.ReadLine();
                }

                sr.Close();

                return Content(HttpStatusCode.OK, fileText);
            }

            return Content(HttpStatusCode.OK, fileText);
        }
    }
}
