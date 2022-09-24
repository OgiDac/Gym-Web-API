using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeretanaWebApi.Models;
using System.Globalization;

namespace TeretanaWebApi.Controllers
{
    public class FitnessCentersController : ApiController
    {
        public IHttpActionResult Get()
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");
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

        public IHttpActionResult Get(string id, string centerName, string address, int? openingYear, int? sort)
        {
            if (id is null) id = "";
            if (centerName is null) centerName = "";
            if (address is null) address = "";

            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");
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

                var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(fileText);

                List<FitnessCenter> fitnesscenters = new List<FitnessCenter>();

                for (int i = 0; i < deserializeFitnessCentersList.Count(); i++)
                {
                    if (deserializeFitnessCentersList[i].Id.Equals(id) || (id.Length==0 && ((deserializeFitnessCentersList[i].CenterName.Contains(centerName) || centerName.Length == 0) && (deserializeFitnessCentersList[i].Address.Equals(address) || address.Length == 0) && (deserializeFitnessCentersList[i].OpeningYear == openingYear || openingYear == null))))
                    {
                        fitnesscenters.Add(deserializeFitnessCentersList[i]);

                        
                    }
                }
           
                    if (sort != null)
                    {
                        switch(sort){
                            case 1:
                            fitnesscenters = fitnesscenters.OrderByDescending(element => element.CenterName).ToList();
                                break;
                            case 2:
                            fitnesscenters = fitnesscenters.OrderBy(element => element.CenterName).ToList();
                                break;
                            case 3:
                            fitnesscenters = fitnesscenters.OrderByDescending(element => element.Address).ToList();
                                break;
                            case 4:
                            fitnesscenters = fitnesscenters.OrderBy(element => element.Address).ToList();
                                break;
                            case 5:
                            fitnesscenters = fitnesscenters.OrderByDescending(element => element.OpeningYear).ToList();
                                break;                     
                            case 6:
                            fitnesscenters = fitnesscenters.OrderBy(element => element.OpeningYear).ToList();
                                break;
                        }
                    
                    return Content(HttpStatusCode.OK, fitnesscenters);
                }

                return Content(HttpStatusCode.OK, fitnesscenters);
            }

            return Content(HttpStatusCode.NotFound, "File does not exist!");
        }

        public IHttpActionResult Post(FitnessCenter fitnesCenter)
        {
            var finessCentersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");
            var usersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");

            string fitnessCentersFileLineReader;
            string usersFileLineReader;
            try
            {
                if (File.Exists(finessCentersFilePath) && File.Exists(usersFilePath) && File.ReadAllText(finessCentersFilePath).Length > 0 && File.ReadAllText(usersFilePath).Length > 0)
                {
                    StreamReader fitnesCentersSR = new StreamReader(finessCentersFilePath);
                    StreamReader usersSR = new StreamReader(usersFilePath);

                    string finessCentersJsonContent = "";
                    string usersJsonContent = "";

                    fitnessCentersFileLineReader = fitnesCentersSR.ReadLine();
                    usersFileLineReader = usersSR.ReadLine();

                    while (fitnessCentersFileLineReader != null)
                    {
                        finessCentersJsonContent += fitnessCentersFileLineReader;
                        fitnessCentersFileLineReader = fitnesCentersSR.ReadLine();
                    }

                    while (usersFileLineReader != null)
                    {
                        usersJsonContent += usersFileLineReader;
                        usersFileLineReader = usersSR.ReadLine();
                    }

                    var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(finessCentersJsonContent);
                    var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(usersJsonContent);

                    fitnesCentersSR.Close();
                    usersSR.Close();

                    for (int i = 0; i < deserializeUserList.Count(); i++)
                    {
                        if (deserializeUserList[i].Username.Equals(fitnesCenter.Owner) && deserializeUserList[i].RoleId == 0)
                        {
                            StreamWriter sw = new StreamWriter(finessCentersFilePath);
                            deserializeFitnessCentersList.Add(fitnesCenter);
                            sw.WriteLine(JsonConvert.SerializeObject(deserializeFitnessCentersList));
                            sw.Close();
                            return Content(HttpStatusCode.OK, "A new fitness center successfully aded");
                        }
                    }
                    return Content(HttpStatusCode.Conflict, $"User {fitnesCenter.Owner} does not exist or does not have owner role!");

                }
                else
                {

                    if (File.Exists(usersFilePath) && File.ReadAllText(usersFilePath).Length > 0)
                    {

                        StreamReader sr = new StreamReader(usersFilePath);
                        string usersJsonContent = "";

                        usersFileLineReader = sr.ReadLine();

                        while (usersFileLineReader != null)
                        {

                            usersJsonContent += usersFileLineReader;

                            usersFileLineReader = sr.ReadLine();
                        }

                        sr.Close();


                        var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(usersJsonContent);


                        for (int i = 0; i < deserializeUserList.Count(); i++)
                        {
                            if (deserializeUserList[i].Username.Equals(fitnesCenter.Owner) && deserializeUserList[i].RoleId == 0)
                            {
                                List<FitnessCenter> fitnessCenters = new List<FitnessCenter>();
                                StreamWriter sw = new StreamWriter(finessCentersFilePath);
                                fitnessCenters.Add(fitnesCenter);
                                sw.WriteLine(JsonConvert.SerializeObject(fitnessCenters));
                                sw.Close();
                                return Content(HttpStatusCode.OK, "A new fitness center successfully aded");
                            }
                        }

                        return Content(HttpStatusCode.Conflict, $"User {fitnesCenter.Owner} does not exist or does not have owner role!!");
                    }



                }

                return Content(HttpStatusCode.Conflict, "An error occured");

            }
            catch (Exception e)
            {

                return Content(HttpStatusCode.NotFound, $"An error occured {e.Message}");
            }
        }
    
        public IHttpActionResult Put(EditFitnessCenter center)
        {

            var finessCentersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");
            var groupTrainingsFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");
            string fitnessCentersFileLineReader;

            if (File.Exists(finessCentersFilePath) && File.ReadAllText(finessCentersFilePath).Length > 0)
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

                FitnessCenter selectedCenter = null;

                for (int i=0; i < deserializeFitnessCentersList.Count; i++)
                {
                    if (deserializeFitnessCentersList[i].Id == center.Id)
                    {
                        selectedCenter = deserializeFitnessCentersList[i];
                        deserializeFitnessCentersList.RemoveAt(i);
                        break;
                    }
                }

                if (selectedCenter == null)
                {
                    return NotFound();
                }

                if (center.Deleted)
                {

                    if (File.Exists(groupTrainingsFilePath) && File.ReadAllText(groupTrainingsFilePath).Length > 0)
                    {

                        StreamReader groupTrainingsSR = new StreamReader(groupTrainingsFilePath);

                        string groupTrainingsFileLineReader;
                        string groupTrainingJsonContent = "";

                        groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();

                        while (groupTrainingsFileLineReader != null)
                        {
                            groupTrainingJsonContent += groupTrainingsFileLineReader;
                            groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();
                        }

                        groupTrainingsSR.Close();


                        var deserializeGroupTrainingsList = JsonConvert.DeserializeObject<List<GroupTraining>>(groupTrainingJsonContent);

                        for(int i=0;i< deserializeGroupTrainingsList.Count; i++)
                        {
                            if (deserializeGroupTrainingsList[i].FitnessCenter == center.Id)
                            {
                                string dateTime = deserializeGroupTrainingsList[i].DateAndTime.Replace("/","");
                                string changed = dateTime[2].ToString() + dateTime[3].ToString() + "/"+ dateTime[0] + dateTime[1] + "/"+ dateTime[4] + dateTime[5]+ dateTime[6] + dateTime[7];

                                DateTime trainingDateTime = DateTime.Parse(changed);
                                if (trainingDateTime > DateTime.Now) {
                                
                                return Conflict();
                                }
                            }
                        }
                        selectedCenter.Deleted = true;
                    }
                    else
                    {
                        selectedCenter.Deleted = true;
                    }
                }         
                else
                {
                    if (center.CenterName.Length > 0)
                    {
                        selectedCenter.CenterName = center.CenterName;
                    }
                    if (center.Address.Length > 0)
                    {
                        selectedCenter.Address = center.Address;
                    }
                    if (center.MonthlyMembership!=null && center.MonthlyMembership != 0)
                    {
                        selectedCenter.MonthlyMembership = (int)center.MonthlyMembership;
                    }
                    if (center.OpeningYear != null && center.OpeningYear != 0)
                    {
                        selectedCenter.OpeningYear = (int)center.OpeningYear;
                    }
                    if (center.PriceOfOneGroupTraining != null && center.PriceOfOneGroupTraining != 0)
                    {
                        selectedCenter.PriceOfOneGroupTraining = (int)center.PriceOfOneGroupTraining;
                    }
                    if (center.PriceOfOneTraining != null && center.PriceOfOneTraining != 0)
                    {
                        selectedCenter.PriceOfOneTraining = (int)center.PriceOfOneTraining;
                    }
                }


                deserializeFitnessCentersList.Add(selectedCenter);
                StreamWriter sw = new StreamWriter(finessCentersFilePath);
                sw.WriteLine(JsonConvert.SerializeObject(deserializeFitnessCentersList));
                sw.Close();
                fitnesCentersSR.Close();
                return Ok();
            }

                return NotFound();
        }
    }
}
