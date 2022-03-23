using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DipSampleUploadButton.Repository;


using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DipSampleUploadButton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class DipSampleController : ControllerBase

    {
        ProgramController p = new ProgramController();

        public static string pathbuilt = null;      
        public static string url = "";
        public static string processGroupId = "";
        public static string userName = "";
        public static string password = "";
        private string uploadPath = "";
        private string uploadFileName = "";
        

       
        

        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            return Ok("file upload api running");
        }

      
        public static void CredentialConfiguration()
        {


           
            using (StreamReader file = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), "DipCredentials", "CredentialDetails.json")))
            {
                
                    JsonSerializer serializer = new JsonSerializer();
                    Root fileObject = (Root)serializer.Deserialize(file, typeof(Root));
                
               
                url = string.Format("https://{0}:{1}/dataintegration-api/", fileObject.UserUtility.HostName, fileObject.UserUtility.PortNumber);
                processGroupId = fileObject.UserUtility.ProcessGroupId;
                userName = fileObject.UserUtility.Username;
                password = fileObject.UserUtility.Password;
              
                
            }


           
        }


        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                
                fileName = file.FileName;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "upload");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                uploadFileName = fileName;
                uploadPath = pathBuilt;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "upload", fileName);
                if (System.IO.File.Exists(path))
                {
                    
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                        
                    }
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                            


            }
            catch (Exception e)
            {
                return $"Error:{ e.Message}";
            }
            
            return fileName;
        }
        [HttpPost]
      
        [Route("saveFiles")]
        [RequestFormLimits(MultipartBodyLengthLimit = 5097152000)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {

                var result = await WriteFile(file);
                var resp = "Uploaded! DIP workflow started successfully";
                var statusCode = Ok(resp).StatusCode;
                if (statusCode == 200)
                {

                    CredentialConfiguration();
                    DipWorkFlowProperties();
                    return Ok(resp);
                }
                return Ok(resp);

            }
            catch (Exception e)
            {
                var resp = "Upload failed!\n Error : " + e;
                return Ok(resp);
            }
        }
       

        private void DipWorkFlowProperties()
        {
            
            try
            {
                
               
              string accessToken=  p.UmsDetails(url,userName, password);
                p.ClientDetails(url,accessToken);
                p.StartProcessGroup(url, "STOPPED", processGroupId, accessToken);
                p.Details(url, processGroupId,accessToken);
                p.GetFile(url, ProgramController.getFileDict, ProgramController.getfileVersion, accessToken, uploadPath, uploadFileName);               
                p.StartProcessGroup(url, "RUNNING", processGroupId, accessToken);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        



    }


}










