using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace LargeFileExtraction
{
    class DipHelper
    {
        public static int getfileVersion;
        public static int putDatabaseRecordVersion;
        public static string getFileDict = null;
        public static string putDatabaseRecordDict = null;


        internal void DipWorkFlow(DipConfiguration dipConfig, string uploadPath, string uploadFileName)
        {
            try
            {
                string url = string.Format("https://{0}:{1}/dataintegration-api/", dipConfig.HostName, dipConfig.PortNumber);
                string accessToken = UmsDetails(url, dipConfig.Username, dipConfig.Password);
                string clientid = GetClientId(url, accessToken);
                StartProcessGroup(url, "STOPPED", dipConfig.ProcessGroupId, accessToken);
                DIPProcessDetails(url, dipConfig.ProcessGroupId, accessToken);
                StartGetFileProcessor(url, clientid, accessToken, uploadPath, uploadFileName);
                StartProcessGroup(url, "RUNNING", dipConfig.ProcessGroupId, accessToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
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
        public string GetClientId(string url, string accessToken)
        {
            try
            {
                // GET request to get client id
                string clientId = null;
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

        public void DIPProcessDetails(string url, string processGroupId, string accessToken)
        {
            try
            {
                Dictionary<string, string> Processors = new Dictionary<string, string>();
                Dictionary<string, int> ProcessorVersions = new Dictionary<string, int>();
                var httpWebRequestId = (HttpWebRequest)WebRequest.Create(url + "flow/process-groups/" + processGroupId);
                httpWebRequestId.Method = "GET";
                httpWebRequestId.Headers.Add("Authorization", "Bearer " + accessToken);
                var httpResponseId = (HttpWebResponse)httpWebRequestId.GetResponse();
                using (var streamReaderId = new StreamReader(httpResponseId.GetResponseStream()))
                {
                    var processGroupResult = streamReaderId.ReadToEnd();
                    var processorsData = JsonConvert.DeserializeObject<DIPProperties>(processGroupResult).processGroupFlow.flow.processors;

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
                throw ex;
            }
        }
        
        public void StartGetFileProcessor(string url, string clientId, string accessToken,string uploadPath,string uploadFileName)
        {
            try
            {
                string filepath = uploadPath.Replace(@"\", @"\\");               
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "processors/" + getFileDict);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                httpWebRequest.Method = "PUT";
                using (var streamWriterQuery = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsondata = "{\"revision\":{\"clientId\":\"" + clientId + "\",\"version\":\"" + getfileVersion + "\"},\"component\":{\"id\":\"" + getFileDict + "\",\"config\": {\"properties\": {\"Input Directory\":\"" + filepath + "\",\"File Filter\":\"" + uploadFileName + "\"}}}}";
                    streamWriterQuery.Write(jsondata);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void StartProcessGroup(string url, string state, string processGroupId, string accessToken)
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}