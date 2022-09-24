using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeretanaWebApi.Models;

namespace TeretanaWebApi.Controllers
{
    public class UsersController : ApiController
    {

        public IHttpActionResult Get()
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");
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

        public IHttpActionResult Get(string username)
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");
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

                var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(fileText);

                List<NewUser> users = new List<NewUser>();

                for (int i = 0; i < deserializeUserList.Count(); i++)
                {
                    if (deserializeUserList[i].Username.Equals(username))
                    {
                        users.Add(deserializeUserList[i]);
                    }
                }

                return Content(HttpStatusCode.OK, users);
            }

            return Content(HttpStatusCode.OK, fileText);
        }

        [Route("api/Users/Login")]
        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");
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

                var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(fileText);

                sr.Close();

                for (int i = 0; i < deserializeUserList.Count(); i++)
                {
                    if (deserializeUserList[i].Password.Equals(user.Password) && deserializeUserList[i].Username.Equals(user.Username) && !deserializeUserList[i].Blocked)
                    {
                        if (deserializeUserList[i].RoleId == 1)
                        {

                            var finessCentersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");
                            string fitnessCentersFileLineReader;
                            if (File.Exists(finessCentersFilePath)  && File.ReadAllText(finessCentersFilePath).Length > 0)
                            {
                                StreamReader fitnesCentersSR = new StreamReader(finessCentersFilePath);
                                string finessCentersJsonContent = "";
                                fitnessCentersFileLineReader = fitnesCentersSR.ReadLine();
                                while (fitnessCentersFileLineReader != null)
                                {
                                    finessCentersJsonContent += fitnessCentersFileLineReader;
                                    fitnessCentersFileLineReader = fitnesCentersSR.ReadLine();
                                }
                                fitnesCentersSR.Close();
                                var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(finessCentersJsonContent);
                                for(int j = 0;j< deserializeFitnessCentersList.Count; j++)
                                {

                                    if(deserializeUserList[i].FitnessCenterTrainer== deserializeFitnessCentersList[j].Id && deserializeFitnessCentersList[j].Deleted)
                                    {
                                        return Content(HttpStatusCode.Forbidden, "Fitness center where trainer works is deleted");
                                    }

                                }
                            }
                            else
                            {
                                return Content(HttpStatusCode.NoContent, "Fitness center where trainer works is not found");
                            }
                        }
                        return Content(HttpStatusCode.OK, deserializeUserList[i].ToJson());
                    }else if (deserializeUserList[i].Blocked)
                    {
                        return Content(HttpStatusCode.Forbidden, "user blocked");
                    }
                }

            }
            return Content(HttpStatusCode.NotFound, "Not found");
        }

        [Route("api/Users/ChangeData")]
        [HttpPost]
        public IHttpActionResult ChangeData(EditUser user)
        {
            var usersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");
            

            string userFileLineReader;
            
            
            if (File.Exists(usersFilePath) && File.ReadAllText(usersFilePath).Length > 0 )
            {

                StreamReader usersSR = new StreamReader(usersFilePath);


                string usersJsonContent = "";
                

                userFileLineReader = usersSR.ReadLine();
                

                while (userFileLineReader != null)
                {

                    usersJsonContent += userFileLineReader;

                    userFileLineReader = usersSR.ReadLine();
                }


                var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(usersJsonContent);

                usersSR.Close();

                NewUser tempUser;

                for (int i = 0; i < deserializeUserList.Count(); i++)
                {
                    if (deserializeUserList[i].Username.Equals(user.Username))
                    {
                        tempUser = deserializeUserList[i];
                        deserializeUserList.RemoveAt(i);

                        if (user.Blocked)
                        {
                            tempUser.Blocked = true;
                        }
                        else
                        {

                            if (user.Name.Length > 0)
                                tempUser.Name = user.Name;

                            if (user.Surname.Length > 0)
                                tempUser.Surname = user.Surname;

                            if (user.Password.Length > 0)
                                tempUser.Password = user.Password;

                            if (user.Gender.Length > 0)
                                tempUser.Gender = user.Gender;

                            if (user.Email.Length > 0)
                                tempUser.Email = user.Email;

                            if (user.BirthDate.Length > 0)
                                tempUser.BirthDate = user.BirthDate;
                        }

                        deserializeUserList.Add(tempUser);
                        break;
                    }
                }

                        StreamWriter sw = new StreamWriter(usersFilePath);
                        sw.WriteLine(JsonConvert.SerializeObject(deserializeUserList));
                        sw.Close();
                        return Content(HttpStatusCode.OK, "A new user successfully added");

               
            }
            return Content(HttpStatusCode.Conflict, "An error occured");
        }

        public IHttpActionResult Post(NewUser newUser)
        {
            var usersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");
            var rolesFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/roles.txt");

            string userFileLineReader;
            string roleFileLineReader;

            try
            {

                if (File.Exists(usersFilePath) && File.ReadAllText(usersFilePath).Length > 0 && File.Exists(rolesFilePath) && File.ReadAllText(rolesFilePath).Length > 0)
                {

                    StreamReader usersSR = new StreamReader(usersFilePath);
                    StreamReader rolesSR = new StreamReader(rolesFilePath);


                    string usersJsonContent = "";
                    string rolesJsonContent = "";

                    userFileLineReader = usersSR.ReadLine();
                    roleFileLineReader = rolesSR.ReadLine();

                    while (userFileLineReader != null)
                    {

                        usersJsonContent += userFileLineReader;

                        userFileLineReader = usersSR.ReadLine();
                    }

                    while (roleFileLineReader != null)
                    {

                        rolesJsonContent += roleFileLineReader;

                        roleFileLineReader = rolesSR.ReadLine();
                    }

                    var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(usersJsonContent);
                    var deserializeRoleList = JsonConvert.DeserializeObject<List<Role>>(rolesJsonContent);

                    rolesSR.Close();
                    usersSR.Close();

                    for (int i = 0; i < deserializeUserList.Count(); i++)
                    {
                        if (deserializeUserList[i].Username.Equals(newUser.Username))
                        {
                            rolesSR.Close();
                            usersSR.Close();
                            return Content(HttpStatusCode.Conflict, $"User {newUser.Username} already exist!");
                        }


                    }

                    for (int i = 0; i < deserializeRoleList.Count(); i++)
                    {
                        if (deserializeRoleList[i].RoleId.Equals(newUser.RoleId))
                        {

                            StreamWriter sw = new StreamWriter(usersFilePath);
                            deserializeUserList.Add(newUser);
                            sw.WriteLine(JsonConvert.SerializeObject(deserializeUserList));
                            sw.Close();
                            return Content(HttpStatusCode.OK, "A new user successfully added");

                        }
                    }

                    return Content(HttpStatusCode.Conflict, $"User {newUser.Username} with role number {newUser.RoleId} does not exist!");
                }

                else
                {
                    if (File.Exists(rolesFilePath) && File.ReadAllText(rolesFilePath).Length > 0)
                    {

                        StreamReader rolesSR = new StreamReader(rolesFilePath);
                        string roleJsonContent = "";

                        roleFileLineReader = rolesSR.ReadLine();

                        while (roleFileLineReader != null)
                        {

                            roleJsonContent += roleFileLineReader;

                            roleFileLineReader = rolesSR.ReadLine();
                        }

                        rolesSR.Close();


                        var deserializeRoleList = JsonConvert.DeserializeObject<List<Role>>(roleJsonContent);

                        for (int i = 0; i < deserializeRoleList.Count(); i++)
                        {
                            if (deserializeRoleList[i].RoleId.Equals(newUser.RoleId))
                            {
                                List<NewUser> users = new List<NewUser>();
                                StreamWriter sw = new StreamWriter(usersFilePath);
                                users.Add(newUser);
                                sw.WriteLine(JsonConvert.SerializeObject(users));
                                sw.Close();
                                return Content(HttpStatusCode.OK, "A new user successfully added");

                            }
                        }


                        return Content(HttpStatusCode.Conflict, $"User {newUser.Username} with role number {newUser.RoleId} does not exist!");
                    }

                }
                return Content(HttpStatusCode.Conflict, "An error occured");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"An error occured {e.Message}");
            }
        }
    }
}
