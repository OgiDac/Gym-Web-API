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
    public class GroupTrainingController : ApiController
    {
        /*  public IHttpActionResult Get()
          {
              var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");
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
          */
        public IHttpActionResult Get(string id)
        {
            if (id == null) id = "";

            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");
            var fitnessCentersfullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");

            string line;

            string fileText = "";

            string fitnessCenterLine;
            string fitnessCenterFileText = "";

            if (File.Exists(fitnessCentersfullPath) && File.ReadAllText(fitnessCentersfullPath).Length > 0) { 

            StreamReader fcsr = new StreamReader(fitnessCentersfullPath);

            fitnessCenterLine = fcsr.ReadLine();


            while (fitnessCenterLine != null)
            {

                fitnessCenterFileText += fitnessCenterLine;

                fitnessCenterLine = fcsr.ReadLine();
            }

            fcsr.Close();
        }

                var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(fitnessCenterFileText);



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

                var deserializeGroupTrainingsList = JsonConvert.DeserializeObject<List<GroupTraining>>(fileText);

                List<GroupTraining> trainings = new List<GroupTraining>();

                for(int i = 0; i < deserializeGroupTrainingsList.Count(); i++)
                {
                    if (deserializeGroupTrainingsList[i].FitnessCenter.Equals(id) || id.Length==0)
                    {
                        try
                        {
                            string fitnessCenterName = deserializeFitnessCentersList.First((center) => center.Id.Equals(deserializeGroupTrainingsList[i].FitnessCenter)).CenterName;
                            deserializeGroupTrainingsList[i].FitnessCenter = fitnessCenterName;
                        } catch (Exception e) { }
                        trainings.Add(deserializeGroupTrainingsList[i]);
                    }
                }

                return Content(HttpStatusCode.OK, trainings);
            }

            return Content(HttpStatusCode.OK, fileText);
        }

        public IHttpActionResult Put(VisitorGroupTraining visitor)
        {
            var groupTrainingsFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");

            string groupTrainingsFileLineReader;

            if (File.Exists(groupTrainingsFilePath) && File.ReadAllText(groupTrainingsFilePath).Length > 0)
            {
                StreamReader groupTrainingsSR = new StreamReader(groupTrainingsFilePath);

                string groupTrainingJsonContent = "";

                groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();

                while (groupTrainingsFileLineReader != null)
                {
                    groupTrainingJsonContent += groupTrainingsFileLineReader;
                    groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();
                }

                groupTrainingsSR.Close();


                var deserializeGroupTrainingsList = JsonConvert.DeserializeObject<List<GroupTraining>>(groupTrainingJsonContent);

                for(int i = 0; i < deserializeGroupTrainingsList.Count(); i++)
                {
                    if (deserializeGroupTrainingsList[i].Id.Equals(visitor.TariningId))
                    {
                        if(visitor.Username.Length>0)
                        {
                            if (deserializeGroupTrainingsList[i].VisitorList == null)
                                deserializeGroupTrainingsList[i].VisitorList = new List<string>();
                            deserializeGroupTrainingsList[i].VisitorList.Add(visitor.Username);
                        }
                        else if(deserializeGroupTrainingsList[i].VisitorList == null || deserializeGroupTrainingsList[i].VisitorList.Count() ==0)
                        {
                            deserializeGroupTrainingsList[i].Deleted = true;
                        }
                        else
                        {
                            return Content(HttpStatusCode.Conflict, "It's not possible to delete a training with participants");
                        }
                        StreamWriter sw = new StreamWriter(groupTrainingsFilePath);
                        sw.WriteLine(JsonConvert.SerializeObject(deserializeGroupTrainingsList));
                        sw.Close();
                        return Content(HttpStatusCode.OK, "A new group training successfully aded");
                    }

                }

                return Content(HttpStatusCode.Conflict, "Error!");

            }

            return Content(HttpStatusCode.Conflict, "Error!");
        }

        public IHttpActionResult Post(GroupTraining groupTraining)
        {
            var groupTrainingsFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");
            var trainingTypesFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/training_type.txt");
            var fitnessCentersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");

            string groupTrainingsFileLineReader;
            string tariningTypesFileLineReader;
            string fitnessCentersFileLineReader;


            try
            {
                if (File.Exists(groupTrainingsFilePath) && File.ReadAllText(groupTrainingsFilePath).Length > 0 && File.Exists(trainingTypesFilePath) && File.ReadAllText(trainingTypesFilePath).Length > 0 && File.Exists(fitnessCentersFilePath) && File.ReadAllText(fitnessCentersFilePath).Length > 0)
                {
                    StreamReader groupTrainingsSR = new StreamReader(groupTrainingsFilePath);
                    StreamReader trainingTypesSR = new StreamReader(trainingTypesFilePath);
                    StreamReader fitnessCentersSR = new StreamReader(fitnessCentersFilePath);

                    string groupTrainingJsonContent = "";
                    string tariningTypeJsonContent = "";
                    string fitnessCentersJsonContent = "";

                    groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();
                    tariningTypesFileLineReader = trainingTypesSR.ReadLine();
                    fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();

                    while (groupTrainingsFileLineReader != null)
                    {
                        groupTrainingJsonContent += groupTrainingsFileLineReader;
                        groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();
                    }

                    while (tariningTypesFileLineReader != null)
                    {
                        tariningTypeJsonContent += tariningTypesFileLineReader;
                        tariningTypesFileLineReader = trainingTypesSR.ReadLine();
                    }

                    while (fitnessCentersFileLineReader != null)
                    {
                        fitnessCentersJsonContent += fitnessCentersFileLineReader;
                        fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();
                    }


                    groupTrainingsSR.Close();
                    trainingTypesSR.Close();
                    fitnessCentersSR.Close();

                    var deserializeGroupTrainingsList = JsonConvert.DeserializeObject<List<GroupTraining>>(groupTrainingJsonContent);
                    var deserializeTrainingTypesList = JsonConvert.DeserializeObject<List<TrainingType>>(tariningTypeJsonContent);
                    var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(fitnessCentersJsonContent);

                    
                   

                    for (int i = 0; i < deserializeFitnessCentersList.Count(); i++)
                    {


                        if (deserializeFitnessCentersList[i].Id.Equals(groupTraining.FitnessCenter))
                        {


                            StreamWriter sw = new StreamWriter(groupTrainingsFilePath);
                            deserializeGroupTrainingsList.Add(groupTraining);
                            sw.WriteLine(JsonConvert.SerializeObject(deserializeGroupTrainingsList));
                            sw.Close();
                            return Content(HttpStatusCode.OK, "A new group training successfully aded");
                        }
                    }


                    return Content(HttpStatusCode.Conflict, $"Group training does not contain {groupTraining.FitnessCenter}");

                }
                else
                {
                    if (File.Exists(trainingTypesFilePath) && File.ReadAllText(trainingTypesFilePath).Length > 0 && File.Exists(fitnessCentersFilePath) && File.ReadAllText(fitnessCentersFilePath).Length > 0)
                    {
                        StreamReader trainingTypesSR = new StreamReader(trainingTypesFilePath);
                        StreamReader fitnessCentersSR = new StreamReader(fitnessCentersFilePath);

                        string tariningTypeJsonContent = "";
                        string fitnessCentersJsonContent = "";

                        tariningTypesFileLineReader = trainingTypesSR.ReadLine();
                        fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();

                        while (tariningTypesFileLineReader != null)
                        {
                            tariningTypeJsonContent += tariningTypesFileLineReader;
                            tariningTypesFileLineReader = trainingTypesSR.ReadLine();
                        }

                        while (fitnessCentersFileLineReader != null)
                        {
                            fitnessCentersJsonContent += fitnessCentersFileLineReader;
                            fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();
                        }



                        var deserializeTrainingTypesList = JsonConvert.DeserializeObject<List<TrainingType>>(tariningTypeJsonContent);
                        var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(fitnessCentersJsonContent);

                        trainingTypesSR.Close();
                        fitnessCentersSR.Close();



                        

                        for (int i = 0; i < deserializeFitnessCentersList.Count(); i++)
                        {


                            if (deserializeFitnessCentersList[i].Id.Equals(groupTraining.FitnessCenter))
                            {


                                List<GroupTraining> groupTrainings = new List<GroupTraining>();
                                StreamWriter sw = new StreamWriter(groupTrainingsFilePath);
                                groupTrainings.Add(groupTraining);
                                sw.WriteLine(JsonConvert.SerializeObject(groupTrainings));
                                sw.Close();
                                return Content(HttpStatusCode.OK, "A new group training successfully aded");
                            }
                        }


                        return Content(HttpStatusCode.Conflict, $"Group training does not contain {groupTraining.FitnessCenter}");
                    }


                }

                return Content(HttpStatusCode.Conflict, "An error occured");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"An error occured {e.Message}");
            }

        }

        [Route("api/GroupTraining/Edit")]
        [HttpPut]
        public IHttpActionResult Put(GroupTraining groupTraining)
        {
       var test =     groupTraining;
            var groupTrainingsFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");

            string groupTrainingsFileLineReader;

            if (File.Exists(groupTrainingsFilePath) && File.ReadAllText(groupTrainingsFilePath).Length > 0)
            {
                StreamReader groupTrainingsSR = new StreamReader(groupTrainingsFilePath);

                string groupTrainingJsonContent = "";

                groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();

                while (groupTrainingsFileLineReader != null)
                {
                    groupTrainingJsonContent += groupTrainingsFileLineReader;
                    groupTrainingsFileLineReader = groupTrainingsSR.ReadLine();
                }

                groupTrainingsSR.Close();


                var deserializeGroupTrainingsList = JsonConvert.DeserializeObject<List<GroupTraining>>(groupTrainingJsonContent);

                for (int i = 0; i < deserializeGroupTrainingsList.Count(); i++)
                {
                    if (deserializeGroupTrainingsList[i].Id.Equals(groupTraining.Id))
                    {
                        GroupTraining temptraining = deserializeGroupTrainingsList[i];
                        deserializeGroupTrainingsList.RemoveAt(i);

                        if (groupTraining.MaxNumberOfPeople > 0)
                        {
                            temptraining.MaxNumberOfPeople = groupTraining.MaxNumberOfPeople;
                        }  
                        if (groupTraining.TrainingDuration > 0)
                        {
                            temptraining.TrainingDuration = groupTraining.TrainingDuration;
                        }
                        if (groupTraining.TrainingName.Length>0)
                        {
                            temptraining.TrainingName = groupTraining.TrainingName;
                        }
                        if (groupTraining.TrainingType.Length>0)
                        {
                            temptraining.TrainingType = groupTraining.TrainingType;
                        }
                        if (groupTraining.DateAndTime.Length>0)
                        {
                            temptraining.DateAndTime = groupTraining.DateAndTime;
                        }

                        deserializeGroupTrainingsList.Add(temptraining);
                      
                        StreamWriter sw = new StreamWriter(groupTrainingsFilePath);
                        sw.WriteLine(JsonConvert.SerializeObject(deserializeGroupTrainingsList));
                        sw.Close();
                        return Content(HttpStatusCode.OK, "A new group training successfully changed");
                    }

                }

                return Content(HttpStatusCode.Conflict, "Error!");

            }
            return Content(HttpStatusCode.NotFound,"Training not found");
        }
    }
}
