using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                var row = FindRowByColumnValue(worksheet, headers, "TestDataID", testCaseID);

                // Bug fix: use loop index directly instead of IndexOf
                // IndexOf always returns the first occurrence — wrong column if duplicates exist
                for (int colIndex = 0; colIndex < headers.Count; colIndex++)
                {
                    var header = headers[colIndex];
                    var columnIndex = colIndex + 1;
                    var cellValue = worksheet.Cells[row, columnIndex].Value?.ToString();
                    try
                    {
                        testData.Add(header, cellValue);
                    }
                    catch (Exception)
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

        // Bug fix: accept pre-read headers so GetHeaders is not called on every row
        // Bug fix: Trim() both sides so trailing/leading spaces in Excel cells do not break the match
        private int FindRowByColumnValue(ExcelWorksheet worksheet, List<string> headers, string columnName, string value)
        {
            var columnIndex = GetColumnIndex(headers, columnName);
            var rowCount = worksheet.Dimension.End.Row;

            for (int i = 2; i <= rowCount; i++)
            {
                var cellValue = worksheet.Cells[i, columnIndex].Value?.ToString()?.Trim();
                if (cellValue == value?.Trim())
                {
                    return i;
                }
            }
            throw new InvalidOperationException($"No row found with {columnName} = {value}");
        }

        // Bug fix: accepts headers list instead of re-reading from Excel on every call
        private int GetColumnIndex(List<string> headers, string columnName)
        {
            return headers.IndexOf(columnName) + 1;
        }

        public string getFieldTestData(string fieldName, string sheetName, string testCaseID)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];
                var headers = GetHeaders(worksheet);
                var row = FindRowByColumnValue(worksheet, headers, "TestDataID", testCaseID);
                var testData = new Dictionary<string, string>();

                // Bug fix: use loop index directly instead of IndexOf
                for (int colIndex = 0; colIndex < headers.Count; colIndex++)
                {
                    var header = headers[colIndex];
                    var columnIndex = colIndex + 1;
                    var cellValue = worksheet.Cells[row, columnIndex].Value?.ToString();
                    testData.Add(header, cellValue);
                }

                return testData[fieldName];
            }
        }
    }
}
