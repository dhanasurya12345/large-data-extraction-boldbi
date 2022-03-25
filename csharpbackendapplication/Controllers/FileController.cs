using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using Newtonsoft.Json;
using Npgsql;
using Syncfusion.XlsIO;
using System.Text.Json;
using System.Text;

namespace LargeFileExtraction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase

    {
        private string uploadPath = "";
        private string uploadFileName = "";
        private string uploadFullPath = "";
        private string fileNameWithoutExtension = "";
       


        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            return Ok("Application is running");
        }

        [HttpPost]
        [Route("savefile")]
        [RequestFormLimits(MultipartBodyLengthLimit = 5097152000)]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
               
                string chosenApproach = "";
                chosenApproach = HttpContext.Request.Form["options"];
                var file = Request.Form.Files[0];
                var result = await WriteFile(file);

                if (chosenApproach == "DIP")
                {
                    var appConfiguration = ReadAppConfiguration();
                    DipHelper dipHelper = new DipHelper();
                    dipHelper.DipWorkFlow(appConfiguration.DIPConfiguration, uploadPath, uploadFileName);
                    var resp = "Uploaded! DIP workflow started successfully";
                    return Ok(resp);
                }
                else
                {

                    XlsioHelper xlsioHelper = new XlsioHelper(uploadFullPath, fileNameWithoutExtension);
                    if (uploadFileName.EndsWith(".csv"))
                    {

                        xlsioHelper.CsvReader();
                        var resp = "Uploaded! Csv File moved to  Postgress Database Successfully.";

                        return Ok(resp);
                    }
                    else
                    {
                        xlsioHelper.ExcelReader();
                        var resp = "Uploaded! Excel File moved to Postgress Database Successfully.";

                        return Ok(resp);
                    }
                }
                
               
                
              //  return Ok(resp);

            }
            catch (Exception e)
            {
                var resp = "Upload failed!\n Error : " + e;
                return Ok(resp);
            }
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            try
            {
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                uploadFileName = file.FileName;
                uploadPath = pathBuilt;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);
                uploadFullPath = path;
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);

                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return $"Error:{ e.Message}";
            }

            return file.FileName;
        }

        public static AppConfiguration ReadAppConfiguration()
        {
            using (StreamReader file = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(),"configuration.json")))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                AppConfiguration config = (AppConfiguration)serializer.Deserialize(file, typeof(AppConfiguration));
                return config;
            }
        }

       

    }
}










