using System;
using System.Data;
using System.Data.Common;
using System.Security.Principal;
using System.Xml.Linq;
using Snowflake.Data.Client;
using static System.Net.WebRequestMethods;

namespace SnowflakeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Boolean testStatus = true;
            try
            {
                using (IDbConnection conn = new SnowflakeDbConnection())
                {
                    conn.ConnectionString = "scheme = https; account = XXXXXXX; HOST = XXXXXXX.ca-central-1.aws.snowflakecomputing.com; port = 443; user=XXXXX; password =xxxxxx; ROLE = ACCOUNTADMIN; db =frostbyte_tasty_bytes; schema =raw_pos";
                    conn.Open();
                    Console.WriteLine("Connection successful!");
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "USE WAREHOUSE TASTY_DE_WH";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"select * from menu WHERE truck_brand_name = 'Cheeky Greek'";   // sql opertion fetching 
                        //data from an existing table
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }
                        conn.Close();
                    }
                }
            }
            catch (DbException exc)
            {
                Console.WriteLine("Error Message: {0}", exc.Message);
                testStatus = false;
            }
        }
    }
}