using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class ExcelTestDataUtility
    {
        private readonly string filePath;

        public ExcelTestDataUtility(string filePath)
        {
            this.filePath = filePath;
        }

        public Dictionary<string, string> GetTestDataByID(string sheetName, string testCaseID)
        {

            var testData = new Dictionary<string, string>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];
                var headers = GetHeaders(worksheet);
                var row = FindRowByColumnValue(worksheet, "TestDataID", testCaseID);

                foreach (var header in headers)
                {
                    var columnIndex = headers.IndexOf(header) + 1;
                    var cellValue = worksheet.Cells[row, columnIndex].Value?.ToString();
                    try
                    {
                        testData.Add(header, cellValue);
                        //Console.WriteLine(header + " : "+ cellValue);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Exception while adding {header}");
                    }
                }
            }
            return testData;
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


        public String getFieldTestData(String fieldName, string sheetName, string testCaseID)
        {
            var testData = new Dictionary<string, string>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
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
            String fieldValue = testData[fieldName];
            return testData[fieldName];
        }
    }
}
