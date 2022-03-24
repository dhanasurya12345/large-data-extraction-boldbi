using LargeFileExtraction.Controllers;
using Npgsql;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeFileExtraction
{
    public class XlsioHelper
    {
        private string filePath = "";
        private string fileNameWithoutExtension = "";

        internal XlsioHelper(string filePath, string fileNameWithoutExtension)
        {
            this.filePath = filePath;
            this.fileNameWithoutExtension = fileNameWithoutExtension;
        }
        internal void ExcelReader()
        {
            //Creates a new instance for ExcelEngine
            ExcelEngine excelEngine = new ExcelEngine();

            FileStream stream = new FileStream(filePath, FileMode.Open);
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

            var csvPath = Path.ChangeExtension(filePath, "csv");
            workbook.SaveAs(new FileStream(csvPath, FileMode.Create), ",");

            stream.Dispose();
            stream.Close();
            workbook.Close();
            excelEngine.Dispose();

            ExecuteNonQueries(columnName, columnSchema, csvPath);
        }

        internal void CsvReader()
        {
            //Loads or open an existing workbook through Open method of IWorkbooks
            var streamreader = new StreamReader(filePath);

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

            ExecuteNonQueries(columnName, columnSchema, filePath);
        }

        private void ExecuteNonQueries(StringBuilder columnName, StringBuilder columnSchema, string csvPath)
        {

            var connectionString = FileController.ReadAppConfiguration().PostgreSQL.ConnectionString;
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
