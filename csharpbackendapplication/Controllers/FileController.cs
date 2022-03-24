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
        //DIP variables
        private string uploadPath = "";
        private string uploadFileName = "";
        //Xlsio variables
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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {

                var result = await WriteFile(file);

                //if ("dip")
                //{
                var appConfiguration = ReadAppConfiguration();
                DipHelper helper = new DipHelper();
                helper.DipWorkFlow(appConfiguration.DIPConfiguration, uploadPath, uploadFileName);
                var resp = "Uploaded! DIP workflow started successfully";

                //}
                //else
                //{
                if (uploadFileName.EndsWith(".csv"))
                {
                    CsvReader();
                    return Ok(resp);
                }
                else
                {
                    ExcelReader();
                    return Ok(resp);
                }
                //}
                return Ok(resp);

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
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "upload");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                uploadFileName = file.FileName;
                uploadPath = pathBuilt;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "upload", file.FileName);
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
            }
            catch (Exception e)
            {
                return $"Error:{ e.Message}";
            }

            return file.FileName;
        }

        private static AppConfiguration ReadAppConfiguration()
        {
            using (StreamReader file = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), "DipConfig", "configuration.json")))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                AppConfiguration config = (AppConfiguration)serializer.Deserialize(file, typeof(AppConfiguration));
                return config;
            }
        }

        private void ExcelReader()
        {
            //Creates a new instance for ExcelEngine
            ExcelEngine excelEngine = new ExcelEngine();

            FileStream stream = new FileStream(uploadFullPath, FileMode.Open);
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(stream);

            var row = workbook.Worksheets[0].Rows[0];
            //var rowcount=row.Cells.Count();

            StringBuilder columnSchema = new StringBuilder();
            StringBuilder columnName = new StringBuilder();

            for (int i = 0; i < row.Cells.Length; i++)
            {
                if (i > 0)
                {
                    columnSchema.Append(",");
                    columnName.Append(",");
                }
                columnName.Append(row.Cells[i].Value);
                columnSchema.Append(row.Cells[i].Value);
                columnSchema.Append(" ");
                columnSchema.Append("varchar(250)");
            }

            var csvPath = Path.ChangeExtension(uploadFullPath, "csv");
            workbook.SaveAs(new FileStream(csvPath, FileMode.Create), ",");

            stream.Dispose();
            stream.Close();
            workbook.Close();
            excelEngine.Dispose();

            ExecuteNonQueries(columnName, columnSchema, csvPath);
        }

        private void CsvReader()
        {
            //Loads or open an existing workbook through Open method of IWorkbooks
            var streamreader = new StreamReader(uploadFullPath);
             
            //read header row
            var row = streamreader.ReadLine().Split(',');

            StringBuilder columnSchema = new StringBuilder();
            StringBuilder columnName = new StringBuilder();

            for (int i = 0; i < row.Length; i++)
            {
                if (i > 0)
                {
                    columnSchema.Append(",");
                    columnName.Append(",");
                }
                columnName.Append(row[i]);
                columnSchema.Append(row[i]);
                columnSchema.Append(" ");
                columnSchema.Append("varchar(250)");
            }

            streamreader.Close();

            ExecuteNonQueries(columnName, columnSchema, uploadFullPath);
        }

        private void ExecuteNonQueries(StringBuilder columnName, StringBuilder columnSchema, string csvPath)
        {

            var connectionString = ReadAppConfiguration().PostgreSQLConnectionString.ConnectionString;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var droptableQuery = $"DROP TABLE IF EXISTS " + fileNameWithoutExtension + ";";
            var dropTable = new NpgsqlCommand();
            dropTable.Connection = connection;
            dropTable.CommandTimeout = 600;
            dropTable.CommandText = droptableQuery;
            dropTable.ExecuteNonQuery();

            var createTableQuery = $"CREATE TABLE " + fileNameWithoutExtension + "(" + columnSchema + ")";
            var createTable = new NpgsqlCommand(createTableQuery);
            createTable.Connection = connection;
            createTable.CommandTimeout = 600;
            createTable.ExecuteNonQuery();

            var copyQuery = $@"COPY " + fileNameWithoutExtension + "(" + columnName + ") FROM '" + csvPath + "' DELIMITER ',' CSV HEADER";
            var copyTable = new NpgsqlCommand();
            copyTable.CommandText = copyQuery;
            copyTable.CommandTimeout = 600;
            copyTable.CommandType = System.Data.CommandType.Text;
            copyTable.Connection = connection;
            copyTable.ExecuteNonQuery();

            connection.Close();
        }

    }
}










