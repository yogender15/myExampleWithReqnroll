using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    public class ExcelUtility
    {
        public T GetTestDataAndMap<T>(string sheetName, string testCaseID)
        {
            var testData = new Dictionary<string, string>();

            using (var package = new ExcelPackage(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath))))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];
                var headers = GetHeaders(worksheet);
                var row = FindRowByColumnValue(worksheet, "TestDataID", testCaseID);

                foreach (var header in headers)
                {
                    var columnIndex = headers.IndexOf(header) + 1;
                    var cellValue = worksheet.Cells[row, columnIndex].Value?.ToString();
                    testData.Add(header, cellValue);
                }
            }
            var json = JsonConvert.SerializeObject(testData, Formatting.Indented);
            return JsonConvert.DeserializeObject<T>(json);
        }


        private List<string> GetHeaders(ExcelWorksheet worksheet)
        {
            var headers = new List<string>();
            var columnCount = worksheet.Dimension.End.Column;

            for (int i = 1; i <= columnCount; i++)
            {
                headers.Add(worksheet.Cells[1, i].Value?.ToString());

            }
            return headers;
        }

        private int FindRowByColumnValue(ExcelWorksheet worksheet, string columnName, string value)
        {
            var rowCount = worksheet.Dimension.End.Row;
            for (int i = 2; i <= rowCount; i++)
            {
                var cellValue = worksheet.Cells[i, GetColumnIndex(worksheet, columnName)].Value?.ToString();
                if (cellValue == value)
                {
                    return i;
                }

            }
            throw new InvalidOperationException($"No row found with {columnName} = {value}");
        }

        private int GetColumnIndex(ExcelWorksheet worksheet, string columnName)
        {
            var headers = GetHeaders(worksheet);
            return headers.IndexOf(columnName) + 1;
        }
    }
}
