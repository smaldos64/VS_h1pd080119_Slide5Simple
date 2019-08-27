using System;
using System.Data.SqlClient;

namespace Slide5Simple
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder Builder;
            SqlConnection Connection;
            string SQLServerName;
            string SQLCommandString;
            string OutputString;
            string CourseName;

            try
            {
                SQLServerName = "sql.itcn.dk\\TCAA";
                
                // Build connection string
                Builder = new SqlConnectionStringBuilder();
                Builder.DataSource = "PCM15715";   // update me
                Builder.InitialCatalog = "h1pd080119";
                //Builder.IntegratedSecurity can be used when working on your Local SQL Server
                Builder.IntegratedSecurity = true;

                // When workiong on a "public" SQL Server you myst supply Password and UserID as shown below. And further you must
                // set IntegratedSecurity to false.
                Builder.DataSource = SQLServerName;
                Builder.InitialCatalog = "ltpe.TCAA";
                Builder.Password = "xxx";
                Builder.UserID = "xxx";
                Builder.IntegratedSecurity = false;

                Connection = new SqlConnection(Builder.ConnectionString);

                // Connect to SQL
                Console.WriteLine("Connecting to SQL Server ... {0}", SQLServerName);

                Connection.Open();
                Console.WriteLine("Done connectiong to Server ... {0}.", SQLServerName);

                SQLCommandString = "SELECT * FROM Courses";
                using (SqlCommand Command = new SqlCommand(SQLCommandString, Connection))
                {
                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            OutputString = "";
                            OutputString = Reader.GetInt32(0) + " ";
                            OutputString += Reader.GetString(1) + " ";
                            Console.WriteLine(OutputString);
                        }
                    }
                }

                Console.WriteLine();
                Console.Write("Indtast nyt kusus navn : ");
                CourseName = Console.ReadLine();

                SQLCommandString = "INSERT INTO Courses (CourseName) values('" + CourseName + "')";

                using (SqlDataAdapter Adapter = new SqlDataAdapter())
                {
                    SqlCommand Command = new SqlCommand(SQLCommandString, Connection);
                    Adapter.InsertCommand = Command;
                    Adapter.InsertCommand.ExecuteNonQuery();
                }

                Console.WriteLine();

                SQLCommandString = "SELECT * FROM Courses";
                using (SqlCommand Command = new SqlCommand(SQLCommandString, Connection))
                {
                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            OutputString = "";
                            OutputString = Reader.GetInt32(0) + " ";
                            OutputString += Reader.GetString(1) + " ";
                            Console.WriteLine(OutputString);
                        }
                    }
                }

                Connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            
            Console.ReadLine();
        }
    }
}
