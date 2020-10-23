using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
//using System.Net.Http;
using RestSharp;
using RestSharp.Authenticators;

namespace Erizos_API
{
    public class RaceboardsPayload
    {
        #region Classes

        public class StateData
        {
            public readonly string id = "State";
            public string value = "";
        }

        public class ElectoralData
        {
            public readonly string id = "ElectoralVotes";
            public int value = 0;
        }

        public class InPercent
        {
            public readonly string id = "InPercent";
            //public double value = 0.0;
            public string value = "";
        }

        public class RaceStatus
        {
            public readonly string id = "RaceStatus";

            /*
             * 
             * 0 = None
             * 1 = Called
             * 2 = Just Called
             * 3 = Too Close
             * 
             */

            public int value = 0;
        }

        public class HasVotes
        {
            public readonly string id = "HasVotes";
            public bool value = false;
        }

        public class TotalVotes
        {
            public readonly string id = "TotalVotes";
            public int value = 0;
        }

        public class Difference
        {
            public readonly string id = "Difference";
            public int value = 0;
        }

        public class DemsWinner
        {
            public readonly string id = "DemsWinner";
            public bool value = false;
        }

        public class DemsVotes
        {
            public readonly string id = "DemsVotes";
            public int value = 0;
        }

        public class DemsPercent
        {
            public readonly string id = "DemsPercent";
            public double value = 0.0;
        }

        public class RepsWinner
        {
            public readonly string id = "RepsWinner";
            public bool value = false;
        }

        public class RepsVotes
        {
            public readonly string id = "RepsVotes";
            public int value = 0;
        }

        public class RepsPercent
        {
            public readonly string id = "RepsPercent";
            public double value = 0.0;
        }

        public class Candidate
        {
            public readonly string id = "Candidate";

            /*
             * 
             * 0 = Rep
             * 1 = Dem
             * 2 = UD
             * 
             */

            public int value = 0;
        }

        public class CheckMark
        {
            public readonly string id = "Checkmark";
            public bool value = false;
        }

        public class Logo
        {
            public readonly string id = "Logo";
            public bool value = false;
        }

        public class Raceboards1Way
        {
            public StateData StateData = new StateData();
            public Candidate Candidate = new Candidate();
            public CheckMark CheckMark = new CheckMark();
            public Logo Logo = new Logo();
        }

        public class Raceboards2Way
        {
            public StateData StateData = new StateData();
            public ElectoralData ElectoralData = new ElectoralData();
            public InPercent InPercent = new InPercent();
            public RaceStatus RaceStatus = new RaceStatus();
            public HasVotes HasVotes = new HasVotes();
            public TotalVotes TotalVotes = new TotalVotes();
            public Difference Difference = new Difference();
            public DemsWinner DemsWinner = new DemsWinner();
            public DemsVotes DemsVotes = new DemsVotes();
            public DemsPercent DemsPercent = new DemsPercent();
            public RepsWinner RepsWinner = new RepsWinner();
            public RepsVotes RepsVotes = new RepsVotes();
            public RepsPercent RepsPercent = new RepsPercent();
        }

        public static string unrealEngine;
        #endregion

        public static string SendPayload(Raceboards2Way a)
        {
            string ReturnValue = "";

            string TemplateID = "";

            string ProfileID = "5f45411daf53a00fb08a5372";


            try
            {
                //Gets the template ID value

                var client = new RestClient("http://10.230.36.36:5550/api/templates");
                client.Timeout = -1;
                client.Authenticator = new HttpBasicAuthenticator("admin", "admin");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK && response.Content != "")
                {
                    JArray templates = JArray.Parse(response.Content);

                    foreach (var result in templates)
                    {
                        if (result["name"].ToString() == "Raceboards")
                        {
                            TemplateID = result["_id"].ToString();
                        }
                    }

                    //Parses object to create final payload

                    JObject x = JObject.Parse(JsonConvert.SerializeObject(a));

                    var ids = x.SelectTokens("..id").ToList();
                    var values = x.SelectTokens("..value").ToList();


                    string dataString = "";

                    for (int i = 0; i <= ids.Count() - 1; i++)
                    {

                        //Checks the value type for final payload

                        if (values[i].Type == JTokenType.Boolean)
                        {
                            dataString += "{\"id\":\"" + ids[i].ToString() + "\",\"value\":" + Convert.ToBoolean(values[i]).ToString().ToLower() + "},";
                        }
                        else if (values[i].Type == JTokenType.String)
                        {
                            dataString += "{\"id\":\"" + ids[i].ToString() + "\",\"value\":\"" + values[i] + "\"},";
                        }
                        else if (values[i].Type == JTokenType.Integer || values[i].Type == JTokenType.Float)
                        {
                            dataString += "{\"id\":\"" + ids[i].ToString() + "\",\"value\":" + values[i].ToString() + "},";
                        }
                    }

                    //Formats final payload

                    string FinalPayload = "{\"name\":\"Raceboards " + values[0].ToString() + "\",\"template\":\"" + TemplateID + "\",\"fields\":[{\"id\":\"Raceboards\",\"fields\":[{\"id\":\"Data\",\"fields\":[" + dataString.TrimEnd(',') + "]}]}]}";

                    try
                    {

                        //Sends payload to engine

                        //client = new RestClient($"http://10.230.36.36:5550/api/profiles/{ProfileID}/Mike/take/in");
                        //client = new RestClient($"http://10.230.36.36:5550/api/profiles/{ProfileID}/UNREAL4/take/in");
                        client = new RestClient($"http://10.230.36.36:5550/api/profiles/{ProfileID}/{unrealEngine}/take/in");
                        client.Timeout = -1;
                        client.Authenticator = new HttpBasicAuthenticator("admin", "admin");
                        request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", JsonConvert.DeserializeObject(FinalPayload), ParameterType.RequestBody);
                        response = client.Execute(request);
                        //Console.WriteLine(JsonConvert.DeserializeObject(FinalPayload));
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ReturnValue = response.StatusCode.ToString();
                        }
                        else
                        {
                            ReturnValue = "Error with api\n\n" + response.StatusCode.ToString();
                        }

                        return ReturnValue;
                    }
                    catch(Exception ex)
                    {
                        ReturnValue = "Error send payload\n\n" + ex.ToString();

                        return ReturnValue;
                    }

                }
                else
                {
                    ReturnValue = "Error getting template id\n\n" + response.StatusCode;

                    return ReturnValue;
                }
            }
            catch(Exception ex)
            {
                ReturnValue = "Error creating raceboards playload\n\n" + ex.ToString();

                return ReturnValue;
            }
            
        }

        public static string SendPayload(Raceboards1Way a)
        {
            string ReturnValue = "";
            string TemplateID = "";
            string ProfileID = "5f45411daf53a00fb08a5372";

            try
            {
                //Gets the template ID value

                var client = new RestClient("http://10.230.36.36:5550/api/templates");
                client.Timeout = -1;
                client.Authenticator = new HttpBasicAuthenticator("admin", "admin");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK && response.Content != "")
                {
                    JArray templates = JArray.Parse(response.Content);

                    foreach (var result in templates)
                    {
                        if (result["name"].ToString() == "Raceboards")
                        {
                            TemplateID = result["_id"].ToString();
                        }
                    }

                    //Parses object to create final payload

                    JObject x = JObject.Parse(JsonConvert.SerializeObject(a));
                    var ids = x.SelectTokens("..id").ToList();
                    var values = x.SelectTokens("..value").ToList();

                    string dataString = "";

                    for (int i = 0; i <= ids.Count() - 1; i++)
                    {
                        //Checks the value type for final payload

                        if (values[i].Type == JTokenType.Boolean)
                        {
                            dataString += "{\"id\":\"" + ids[i].ToString() + "\",\"value\":" + Convert.ToBoolean(values[i]).ToString().ToLower() + "},";
                        }
                        else if (values[i].Type == JTokenType.String)
                        {
                            dataString += "{\"id\":\"" + ids[i].ToString() + "\",\"value\":\"" + values[i] + "\"},";
                        }
                        else if (values[i].Type == JTokenType.Integer || values[i].Type == JTokenType.Float)
                        {
                            dataString += "{\"id\":\"" + ids[i].ToString() + "\",\"value\":" + values[i].ToString() + "},";
                        }
                    }

                    //Formats final payload

                    string FinalPayload = "{\"name\":\"Raceboards Oneway " + values[0].ToString() + "\",\"template\":\"" + TemplateID + "\",\"fields\":[{\"id\":\"OneWay\",\"fields\":[{\"id\":\"Data\",\"fields\":[" + dataString.TrimEnd(',') + "]}]}]}";

                    try
                    {
                        //Sends payload to engine

                        //client = new RestClient($"http://10.230.36.36:5550/api/profiles/{ProfileID}/Mike/take/in");
                        client = new RestClient($"http://10.230.36.36:5550/api/profiles/{ProfileID}/{unrealEngine}/take/in");
                        client.Timeout = -1;
                        client.Authenticator = new HttpBasicAuthenticator("admin", "admin");
                        request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", JsonConvert.DeserializeObject(FinalPayload), ParameterType.RequestBody);
                        response = client.Execute(request);
                        //Console.WriteLine(JsonConvert.DeserializeObject(FinalPayload));
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ReturnValue = response.StatusCode.ToString();
                        }
                        else
                        {
                            ReturnValue = "Error with api\n\n" + response.StatusCode.ToString();
                        }

                        return ReturnValue;
                    }
                    catch (Exception ex)
                    {
                        ReturnValue = "Error send payload\n\n" + ex.ToString();

                        return ReturnValue;
                    }

                }
                else
                {
                    ReturnValue = "Error getting template id\n\n" + response.StatusCode;

                    return ReturnValue;
                }
            }
            catch (Exception ex)
            {
                ReturnValue = "Error creating raceboards playload\n\n" + ex.ToString();

                return ReturnValue;
            }

        }
    }
}
