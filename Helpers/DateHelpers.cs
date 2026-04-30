using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    static class DateHelpers
    {
        public static string getFutureDate(int days, String format)
        {
            DateTime date = DateTime.Now.AddDays(days);
            //format- "dd/MM/yyyy"
            string output = DateTime.Parse(date.ToString()).ToString(format);
            return output;
        }




        public static String ToDateTime(string input, string format)
        {
            try
            {
                string output = DateTime.Parse(input).ToString("dd/MM/yyyy");
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
