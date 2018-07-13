using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlClient;

namespace ContactAppDB
{
    class Program
    {

        static void Main(string[] args)
        {
            

            try
            {

                if (args.Length > 0)
                {
                    
                    if (args[0] == "-add")
                    {
                        AddContactDetails(args[1], args[2]);
                    }
                    else if(args[0] == "-list")
                    {
                        ListContacts();
                    }
                    else if (args[0] == "-update")
                    {
                        UpdateContact(args[1], args[2]);
                    }
                    else if (args[0] == "-remove")
                    {
                        DeleteContactDetails(args[1]);
                    }
                    

                }
                else
                {
                    Console.WriteLine("prompt \n-add [name] [cell] \n-list \n-update[name] [cell] \n-remove[name]");
                }

            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("An index was out of range!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Please enter contact name and number:");
            }
        }



        public static void ListContacts()
        {
            try
            {
                string sConnectionString = "Server=(localdb)\\ProjectsV13;Database=Contacts;TrustServerCertificate=True";
                SqlConnection objConn = new SqlConnection(sConnectionString);
                objConn.Open();

                string query = "SELECT * FROM Contact";
                SqlCommand command = new SqlCommand(query, objConn);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["contact_name"]);
                        Console.WriteLine(reader["contact_no"]);
                    }
                }

                reader.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
          

        }

        public static void AddContactDetails(string name, string number)
        {
            try
            {
                bool exist = true;

                string sConnectionString = "Server=(localdb)\\ProjectsV13;Database=Contacts;TrustServerCertificate=True";
                SqlConnection objConn = new SqlConnection(sConnectionString);
                objConn.Open();

                String query = "INSERT INTO dbo.Contact (contact_name,contact_no) VALUES (@name,@no)";

                if (!exist)
                {
                    SqlCommand command = new SqlCommand(query, objConn);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@no", number);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Added");
                }else
                {
                    Console.WriteLine("Contact exist");
                }

                objConn.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void UpdateContact(string name, string number)
        {

           
            string sConnectionString = "Server=(localdb)\\ProjectsV13;Database=Contacts;TrustServerCertificate=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();

            
            String query = " Update Contact Set contact_no = @number where contact_name = @name";

            SqlCommand command = new SqlCommand(query, objConn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@number", number);
            command.ExecuteNonQuery();
            Console.WriteLine("Contact Updated");


        }
        public static void DeleteContactDetails(String name)
        {
            

            try
            {
                string sConnectionString = "Server=(localdb)\\ProjectsV13;Database=Contacts;TrustServerCertificate=True";
                SqlConnection objConn = new SqlConnection(sConnectionString);
                objConn.Open();
                

                string query = "DELETE FROM Contact WHERE contact_name = @name";

                SqlCommand command = new SqlCommand(query, objConn);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
                Console.WriteLine("Contact Removed");

                objConn.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

    }
}