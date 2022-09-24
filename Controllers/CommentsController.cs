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
    public class CommentsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/comments.txt");
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

        public IHttpActionResult Post(Comment comment)
        {
            var commentsFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/comments.txt");
            var usersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/users.txt");
            var fitnessCentersFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/fitness_centers.txt");
            var groupTrainingFfullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/group_trainings.txt");

            string groupTrainingline;

            string groupTrainingFileText = "";

            string commentsFileLineReader;
            string usersFileLineReader;
            string fitnessCentersFileLineReader;

            try
            {
                if (File.Exists(commentsFilePath) && File.ReadAllText(commentsFilePath).Length > 0 && File.Exists(usersFilePath) && File.ReadAllText(usersFilePath).Length > 0 && File.Exists(fitnessCentersFilePath) && File.ReadAllText(fitnessCentersFilePath).Length > 0)
                {
                    StreamReader commentsSR = new StreamReader(commentsFilePath);
                    StreamReader usersSR = new StreamReader(usersFilePath);
                    StreamReader fitnessCentersSR = new StreamReader(fitnessCentersFilePath);

                    string commentsJsonContent = "";
                    string usersJsonContent = "";
                    string fitnessCentersJsonContent = "";

                    commentsFileLineReader = commentsSR.ReadLine();
                    usersFileLineReader = usersSR.ReadLine();
                    fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();

                    while (commentsFileLineReader != null)
                    {
                        commentsJsonContent += commentsFileLineReader;
                        commentsFileLineReader = commentsSR.ReadLine();
                    }

                    while (usersFileLineReader != null)
                    {
                        usersJsonContent += usersFileLineReader;
                        usersFileLineReader = usersSR.ReadLine();
                    }

                    while (fitnessCentersFileLineReader != null)
                    {
                        fitnessCentersJsonContent += fitnessCentersFileLineReader;
                        fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();
                    }

                    var deserializeCommentList = JsonConvert.DeserializeObject<List<Comment>>(commentsJsonContent);
                    var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(usersJsonContent);
                    var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(fitnessCentersJsonContent);

                    commentsSR.Close();
                    usersSR.Close();
                    fitnessCentersSR.Close();

                    FitnessCenter temp = null;

                    for (int i = 0; i < deserializeFitnessCentersList.Count(); i++)
                    {
                        if (deserializeFitnessCentersList[i].Id == comment.FitnessCenter)
                        {
                            temp = deserializeFitnessCentersList[i];
                        }
                    }

                    if (temp == null)
                    {
                        commentsSR.Close();
                        usersSR.Close();
                        fitnessCentersSR.Close();
                        return Content(HttpStatusCode.Conflict, $"Fitness center {comment.FitnessCenter} does not exist!");
                    }

                    if (File.Exists(groupTrainingFfullPath) && File.ReadAllText(groupTrainingFfullPath).Length > 0)
                    {
                        StreamReader gtsr = new StreamReader(groupTrainingFfullPath);


                        groupTrainingline = gtsr.ReadLine();


                        while (groupTrainingline != null)
                        {

                            groupTrainingFileText += groupTrainingline;

                            groupTrainingline = gtsr.ReadLine();
                        }

                        gtsr.Close();

                        var deserializeGroupTrainingsList = JsonConvert.DeserializeObject<List<GroupTraining>>(groupTrainingFileText);

                        List<GroupTraining> trainings = new List<GroupTraining>();

                        bool userParticipated = false;

                        for(int i = 0; i < deserializeGroupTrainingsList.Count; i++)
                        {
                            if(deserializeGroupTrainingsList[i].FitnessCenter.Equals(comment.FitnessCenter) && deserializeGroupTrainingsList[i].VisitorList!=null && deserializeGroupTrainingsList[i].VisitorList.Contains(comment.Visitor))
                            {
                                userParticipated = true;
                            }
                        }

                        if (!userParticipated)
                        {
                            return Content(HttpStatusCode.NotAcceptable, "User did't participate in any group trainings");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotAcceptable, "User did't participate in any group trainings");
                    }

                        for (int i = 0; i < deserializeUserList.Count(); i++)
                    {
                        if (deserializeUserList[i].Username.Equals(comment.Visitor) && deserializeUserList[i].RoleId == 2 && comment.Raiting > 0 && comment.Raiting < 6)
                        {
                            StreamWriter sw = new StreamWriter(commentsFilePath);
                            deserializeCommentList.Add(comment);
                            sw.WriteLine(JsonConvert.SerializeObject(deserializeCommentList));
                            sw.Close();
                            return Content(HttpStatusCode.OK, "A new comment successfully aded");
                        }
                    }

                    return Content(HttpStatusCode.Conflict, $"User {comment.Visitor} does not exist or raiting {comment.Raiting} is not valid!!");

                }
                else
                {
                    if (File.Exists(usersFilePath) && File.ReadAllText(usersFilePath).Length > 0 && File.Exists(fitnessCentersFilePath) && File.ReadAllText(fitnessCentersFilePath).Length > 0)
                    {
                        StreamReader usersSR = new StreamReader(usersFilePath);
                        StreamReader fitnessCentersSR = new StreamReader(fitnessCentersFilePath);

                        string usersJsonContent = "";
                        string fitnessCentersJsonContent = "";

                        usersFileLineReader = usersSR.ReadLine();
                        fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();

                        while (usersFileLineReader != null)
                        {
                            usersJsonContent += usersFileLineReader;
                            usersFileLineReader = usersSR.ReadLine();
                        }

                        while (fitnessCentersFileLineReader != null)
                        {
                            fitnessCentersJsonContent += fitnessCentersFileLineReader;
                            fitnessCentersFileLineReader = fitnessCentersSR.ReadLine();
                        }

                        var deserializeUserList = JsonConvert.DeserializeObject<List<NewUser>>(usersJsonContent);
                        var deserializeFitnessCentersList = JsonConvert.DeserializeObject<List<FitnessCenter>>(fitnessCentersJsonContent);

                        usersSR.Close();
                        fitnessCentersSR.Close();

                        FitnessCenter temp = null;

                        for (int i = 0; i < deserializeFitnessCentersList.Count(); i++)
                        {
                            if (deserializeFitnessCentersList[i].Id == comment.FitnessCenter)
                            {
                                temp = deserializeFitnessCentersList[i];
                               
                            }
                        }

                        if (temp == null)
                        {
                            usersSR.Close();
                            fitnessCentersSR.Close();
                            return Content(HttpStatusCode.Conflict, $"Fitness center {comment.FitnessCenter} does not exist!");
                        }

                        for (int i = 0; i < deserializeUserList.Count(); i++)
                        {
                            if (deserializeUserList[i].Username.Equals(comment.Visitor) && deserializeUserList[i].RoleId == 2 && comment.Raiting > 0 && comment.Raiting < 6)
                            {
                                List<Comment> comments = new List<Comment>();
                                StreamWriter sw = new StreamWriter(commentsFilePath);
                                comments.Add(comment);
                                sw.WriteLine(JsonConvert.SerializeObject(comments));
                                sw.Close();
                                return Content(HttpStatusCode.OK, "A new comment successfully aded");
                            }
                        }
                        return Content(HttpStatusCode.Conflict, $"User {comment.Visitor} does not exist or raiting {comment.Raiting} is not valid!");
                    }

                }
                return Content(HttpStatusCode.Conflict, "An error occured");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"An error occured {e.Message}");
            }

         }
    
        public IHttpActionResult Put(UpdateComment comment)
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/baza/comments.txt"); 
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


                var deserializeCommentList = JsonConvert.DeserializeObject<List<Comment>>(fileText);

                for (int i = 0; i < deserializeCommentList.Count; i++)
                { 
                    if(deserializeCommentList[i].FitnessCenter==comment.FitnessCenter && deserializeCommentList[i].Visitor == comment.Visitor)
                    {
                        Comment temp = deserializeCommentList[i];
                        deserializeCommentList.RemoveAt(i);
                        temp.Status = comment.Status;
                        deserializeCommentList.Add(temp);
                        StreamWriter sw = new StreamWriter(fullPath);
                        sw.WriteLine(JsonConvert.SerializeObject(deserializeCommentList));
                        sw.Close();
                        return Content(HttpStatusCode.OK, "Status changed");
                    }
                }

                return Content(HttpStatusCode.OK, fileText);
            }
            return Content(HttpStatusCode.NotFound, "Comment not found");
        }
    }
}
