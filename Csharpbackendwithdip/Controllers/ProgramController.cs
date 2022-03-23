using DipSampleUploadButton.Repository;


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DipSampleUploadButton.Controllers
{
    class ProgramController
    {
        
        public static string clientId = null;
        public string inputDirectory = null;
        public string tableName = null;
        public string filepath = null;
        public string filename = null;
        public  Dictionary<string, string> valuePairs = new Dictionary<string, string>();
        public  Dictionary<string, string> Processors = new Dictionary<string, string>();
        public  Dictionary<string, int> ProcessorVersions = new Dictionary<string, int>();
        public static int getfileVersion;
        public static int putDatabaseRecordVersion;

       

        public static string getFileDict = null;
        public static string putDatabaseRecordDict = null;

        public object HttpFileCoCollection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string UmsDetails(string url,string username,string password)
        {
            string accessToken = "";
            ////POST request to generate access token
            
            try
            {
                WebRequest request = WebRequest.Create(url + "access/token");
                request.Method = "POST";
                string userData = "username=" + username + "&password=" + password + "";
                byte[] byteArray = Encoding.UTF8.GetBytes(userData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                accessToken = reader.ReadToEnd();
                
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return accessToken;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string ClientDetails(string url, string accessToken)
        {
            try
            {
                // GET request to get client id
                var httpWebRequestClientId = (HttpWebRequest)WebRequest.Create(url + "flow/client-id");
                httpWebRequestClientId.Method = "GET";
                httpWebRequestClientId.Headers.Add("Authorization", "Bearer " + accessToken);
                var httpResponseClientId = (HttpWebResponse)httpWebRequestClientId.GetResponse();
                using (var streamReaderClientId = new StreamReader(httpResponseClientId.GetResponseStream()))
                {
                    clientId = streamReaderClientId.ReadToEnd();
                   
                }
                return clientId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public  void Details(string url, string processGroupId, string accessToken)
        {
            try
            {
                var httpWebRequestId = (HttpWebRequest)WebRequest.Create(url + "flow/process-groups/" + processGroupId);
                httpWebRequestId.Method = "GET";
                httpWebRequestId.Headers.Add("Authorization", "Bearer " + accessToken);
                var httpResponseId = (HttpWebResponse)httpWebRequestId.GetResponse();
                using (var streamReaderId = new StreamReader(httpResponseId.GetResponseStream()))
                {
                    var processGroupResult = streamReaderId.ReadToEnd();
                    var processorsData = JsonConvert.DeserializeObject<JsonProperty>(processGroupResult).processGroupFlow.flow.processors;

                    foreach (var items in processorsData)
                    {
                        Processors.Add(items.component.name, items.component.id);
                        ProcessorVersions.Add(items.component.name, items.revision.version);
                    }
                }
                getFileDict = Processors["GetFile"];
                putDatabaseRecordDict = Processors["PutDatabaseRecord"];
                getfileVersion = ProcessorVersions["GetFile"];
                putDatabaseRecordVersion = ProcessorVersions["PutDatabaseRecord"];
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public  void GetFile(string url, string getFileDict, int getfileVersion,string accessToken,string uploadPath,string uploadFileName)
        {
            try
            {
                inputDirectory = string.IsNullOrEmpty(uploadPath) ? @"C:\Users\DhanasuryaAnandhan\Downloads\large (1)" : uploadPath;
                filename = string.IsNullOrEmpty(uploadFileName) ? "large.xlsx" : uploadFileName;               
                filepath = inputDirectory.Replace(@"\", @"\\");               
                var httpWebRequestQd = (HttpWebRequest)WebRequest.Create(url + "processors/" + getFileDict);
                httpWebRequestQd.ContentType = "application/json";
                httpWebRequestQd.Headers.Add("Authorization", "Bearer " + accessToken);
                httpWebRequestQd.Method = "PUT";
                using (var streamWriterQuery = new StreamWriter(httpWebRequestQd.GetRequestStream()))
                {
                    string jsondata = "{\"revision\":{\"clientId\":\"" + clientId + "\",\"version\":\"" + getfileVersion + "\"},\"component\":{\"id\":\"" + getFileDict + "\",\"config\": {\"properties\": {\"Input Directory\":\"" + filepath + "\",\"File Filter\":\"" + filename + "\"}}}}";
                    streamWriterQuery.Write(jsondata);
                }
                HttpWebResponse httpResponseQd = null;
                try
                {
                    httpResponseQd = (HttpWebResponse)httpWebRequestQd.GetResponse();
                }
                catch (Exception ex){ 
                }
                using (var streamReaderQuery = new StreamReader(httpResponseQd.GetResponseStream()))
                {
                    var putQuery = streamReaderQuery.ReadToEnd();
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public  void StartProcessGroup(string url, string state, string processGroupId, string accessToken)
        {
            try
            {
                var httpWebRequestStart = (HttpWebRequest)WebRequest.Create(url + "flow/process-groups/" + processGroupId);
                httpWebRequestStart.ContentType = "application/json";
                httpWebRequestStart.Headers.Add("Authorization", "Bearer " + accessToken);
                httpWebRequestStart.Method = "PUT";
                using (var streamWriterStart = new StreamWriter(httpWebRequestStart.GetRequestStream()))
                {
                    string jsondata = "{\"id\":\"" + processGroupId + "\",\"state\":\"" + state + "\"}";
                    streamWriterStart.Write(jsondata);
                }
                var httpResponseStart = (HttpWebResponse)httpWebRequestStart.GetResponse();
                using (var streamReaderStart = new StreamReader(httpResponseStart.GetResponseStream()))
                {
                    var putStart = streamReaderStart.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }


}










