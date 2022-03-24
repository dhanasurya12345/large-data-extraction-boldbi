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
using Npgsql;
using Syncfusion.XlsIO;

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
        private string uploadFullPath = "";
        private string fileNameWithoutExtension = "";
        

       
        

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
                uploadFullPath = path;
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);



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
                    if (uploadFileName.EndsWith(".csv"))
                    {
                        CsvReader();

                        return Ok(resp);
                    }
                    else
                    {
                        ConsoleApplication();
                        return Ok(resp);
                    }

                   
                  //  CredentialConfiguration();
                  // DipWorkFlowProperties();
                    
                }
                return Ok(resp);

            }
            catch (Exception e)
            {
                var resp = "Upload failed!\n Error : " + e;
                return Ok(resp);
            }
        }


        private void ConsoleApplication()
        {

            var cs = "Host=localhost;Username=postgres;Password=Surya@12345;Database=postgres;CommandTimeout=600;";
            var con = new NpgsqlConnection(cs);
            con.Open();

            //Creates a new instance for ExcelEngine
            ExcelEngine excelEngine = new ExcelEngine();

            var startTime = DateTime.Now.Minute;
            FileStream stream = new FileStream(uploadFullPath, FileMode.Open);
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(stream);

            var row = workbook.Worksheets[0].Rows[0];
            var rowcount=row.Cells.Count();
            //IWorksheet worksheet = workbook.Worksheets[0];

            StringBuilder builder = new StringBuilder();
            StringBuilder columnName = new StringBuilder();
            

            int i = 0;
            foreach(var cell in row.Cells)
            {
                if (i > 0)
                {
                    builder.Append(",");
                    columnName.Append(",");
                }
                columnName.Append(cell.Value);
                builder.Append(cell.Value);
                builder.Append(" ");
                builder.Append("varchar(250)");
                i++;
            }
            var csvPath = Path.ChangeExtension(uploadFullPath, "csv");
            
           
           workbook.SaveAs(new FileStream(csvPath, FileMode.Create), ",");
            //  workbook.SaveAs(new FileStream(uploadFullPath, FileMode.Create), ",", Encoding.UTF8);
            // workbook.SaveAs(new FileStream(csvPath, FileMode.CreateNew,FileAccess.ReadWrite));
            //workbook = excelEngine.Excel.Workbooks.Open(new FileStream(csvPath, FileMode.Open));


            stream.Dispose();
            stream.Close();
            workbook.Close();
            excelEngine.Dispose();
            var droptable = $"DROP TABLE IF EXISTS " +fileNameWithoutExtension+ ";";
            var cmddrop = new NpgsqlCommand();
            cmddrop.Connection = con;
            cmddrop.CommandText = droptable;
            cmddrop.ExecuteNonQuery();

            var m_createtbl_cmd = new NpgsqlCommand($"CREATE TABLE "+fileNameWithoutExtension+"("+builder+")");
            m_createtbl_cmd.Connection = con;
            m_createtbl_cmd.ExecuteNonQuery();
            //var sql = $@"COPY xlstestthre("+columnName+") FROM '" + csvPath + "' DELIMITER ',' CSV HEADER";
            var sql= $@"COPY "+ fileNameWithoutExtension+ "(" + columnName + ") FROM '" + csvPath + "' DELIMITER ',' CSV HEADER";


            //var sql= @"COPY mydatacsv(format,age,ethnic,sex,area) FROM 'D:\csvFileForPostgres\csvFileToPostGress.csv' DELIMITER ',' CSV HEADER";
            // var sql = @"COPY largedatacsv(region,country,iteType,saleschannel,orderpriority,orderdate,orderid,shipdate,unitsold,unitprice,unitcost,totalrevenue,totalcost,totalprofit) FROM 'C:\Users\DhanasuryaAnandhan\Downloads\DipSampleFilesTesting\salesrecords.csv' DELIMITER ',' CSV HEADER";
            //var sql = @"COPY largedatacsv(region,country,iteType,saleschannel,orderpriority,orderdate,orderid,shipdate,unitsold,unitprice,unitcost,totalrevenue,totalcost,totalprofit) FROM '"+uploadFullPath + "' DELIMITER ',' CSV HEADER";

            var cmd = new NpgsqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            var endTime = DateTime.Now.Minute;
             var totaltime = startTime - endTime;

            con.Close();
            //Console.WriteLine("Product Import complete");
            //Console.ReadKey();

            //---------------------------------------------------------------------------------- using aspose library------------------------------------------------------

        }

        private void CsvReader()
        {
            //var cs = "Host=localhost;Username=postgres;Password=Surya@12345;Database=postgres";
            //var con = new NpgsqlConnection(cs);
            //con.Open();
            //var m_createtbl_cmd = new NpgsqlCommand($"CREATE TABLE largedatacsvse(region varchar(250),country varchar(250),iteType varchar(250),saleschannel varchar(250),orderpriority varchar(250),orderdate varchar(250),orderid varchar(250),shipdate varchar(250),unitsold varchar(250),unitprice varchar(250),unitcost varchar(250),totalrevenue varchar(250),totalcost varchar(250),totalprofit varchar(250))");
            //m_createtbl_cmd.Connection = con;
            //m_createtbl_cmd.ExecuteNonQuery();

            //var sql = @"COPY largedatacsvse(region,country,iteType,saleschannel,orderpriority,orderdate,orderid,shipdate,unitsold,unitprice,unitcost,totalrevenue,totalcost,totalprofit) FROM '" + uploadFullPath + "' DELIMITER ',' CSV HEADER";

            //var cmd = new NpgsqlCommand();
            //cmd.CommandText = sql;
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.Connection = con;
            //cmd.ExecuteNonQuery();

            //con.Close();
            var cs = "Host=localhost;Username=postgres;Password=Surya@12345;Database=postgres;CommandTimeout=600;";
            var con = new NpgsqlConnection(cs);
            con.Open();

            //Creates a new instance for ExcelEngine
            ExcelEngine excelEngine = new ExcelEngine();

            //Loads or open an existing workbook through Open method of IWorkbooks
            // IWorkbook workbook = excelEngine.Excel.Workbooks.Open(new FileStream(uploadFullPath, FileMode.Open));
            //FileStream stream = new FileStream(uploadFullPath, FileMode.Open);
            var streamreader = new StreamReader(uploadFullPath);
            

             
            //read header row
            var row = streamreader.ReadLine().Split(',');
          //  string stop="\\n";
          //  string line="";
            //while ((streamreader.ReadToEnd()) != stop)
            //{
            //    line = streamreader.ReadToEnd();
            //}
            //var columns = row.FirstOrDefault();

            //string[] allLines = System.IO.File.ReadAllLines(uploadFullPath);
            // line = allLines[0];
           

            //var row = workbook.Worksheets[0].Rows[0];
            // var rowcount = row.Cells.Count();
            //IWorksheet worksheet = workbook.Worksheets[0];

            StringBuilder builder = new StringBuilder();
            StringBuilder columnName = new StringBuilder();


            int i = 0;
            foreach (var cell in row)
            {
                if (i > 0)
                {
                    builder.Append(",");
                    columnName.Append(",");
                }
                columnName.Append(cell);
                builder.Append(cell);
                builder.Append(" ");
                builder.Append("varchar(250)");
                i++;
            }
           // workbook.Close();
            excelEngine.Dispose();
            //streamreader.Dispose();
            streamreader.Close();
            
            var droptable = $"DROP TABLE IF EXISTS " + fileNameWithoutExtension + ";";
            var cmddrop = new NpgsqlCommand();
            cmddrop.Connection = con;
            cmddrop.CommandText = droptable;
            cmddrop.ExecuteNonQuery();
            var m_createtbl_cmd = new NpgsqlCommand($"CREATE TABLE " + fileNameWithoutExtension + "(" + builder + ")");
            m_createtbl_cmd.Connection = con;
            m_createtbl_cmd.ExecuteNonQuery();
            var sql = $@"COPY " + fileNameWithoutExtension + "(" + columnName + ") FROM '" + uploadFullPath + "' DELIMITER ',' CSV HEADER";
            var cmd = new NpgsqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            con.Close();


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










