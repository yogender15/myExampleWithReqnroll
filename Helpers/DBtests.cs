using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class DBtests
    {
        public static List<string> GetDBdataList(string query)
        {
            NpgsqlDataReader dbData = null;
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.SQLQueryPropertiesFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);
            Console.WriteLine(query);

            var dbDataValue = new List<string>();
            using (NpgsqlConnection con = DBConnectionUtility.GetConnection())
            {
                try
                {

                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connected");
                        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                        //NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.CommandTimeout = 300;
                        //cmd.CommandText = query;
                        //cmd.Connection = con;
                        try
                        {
                            dbData = cmd.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Npgsql Exception:" + ex.Message);
                        }
                        //  NpgsqlDataReader dbData = cmd.ExecuteReader();


                        //int i = 0;
                        while (dbData.Read())
                        {
                            object columnValueObj = dbData.GetValue(0);
                            string columnValue = (columnValueObj != DBNull.Value) ? columnValueObj.ToString() : null;
                            dbDataValue.Add(columnValue);
                            //                           i = i + 1;
                            //Console.WriteLine(i);
                            //string columnValue = dbData.GetString(dbData.GetOrdinal("uprn"));
                            //dbDataValue.Add(columnValue);


                            //for (int i = 0; i < dbData.FieldCount; i++)
                            //{
                            //    string columnName = dbData.GetName(i);
                            //    object columnValueObj = dbData.GetValue(i);
                            //    string columnValue = (columnValueObj != DBNull.Value) ? columnValueObj.ToString() : null;
                            //    //   Console.WriteLine($"Column Name:{columnName}, value: {columnValue}");
                            //    dbDataValue.Add(columnValue);
                            //}
                            //Log.Information("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));
                            //Console.WriteLine("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));

                        }

                    }

                }
                catch (NpgsqlException ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                finally
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            return dbDataValue;

        }
    }
}
