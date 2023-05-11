using System.Data;
using System.Data.SqlClient;
using System.Globalization;
//using System.Runtime.Intrinsics.Arm;

namespace SQL_Hackthon
{
    class keepnote
    {
        public static void createNote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from keepNote", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "note");
            var row = ds.Tables["note"].NewRow();

            Console.WriteLine("Enter Title");
            row["title"] = Console.ReadLine();
            Console.WriteLine("Enter description");
            row["note_description"] = Console.ReadLine();
            DateTime note_date = new DateTime();
            try
            {
                Console.Write("Enter Date(DD/MM/YYYY): ");
                row["note_date"] = DateTime.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Enter in format dd/mm/yyyy");
                return;
            }
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds, "note");
            Console.WriteLine("Row Added Successfully");
        }
        public static void view_all_Note(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from keepNote", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "note");
            for (int i = 0; i < ds.Tables["note"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["note"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["note"].Rows[i][j]}\t");
                }
                Console.WriteLine();
            }

        }
        public static void view_note(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from keepNote", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "note");
            Console.WriteLine("enter ID");
            int nid = Convert.ToInt16(Console.ReadLine());

            for (int i = 0; i < ds.Tables["note"].Columns.Count; i++)
            {
                try
                {
                    Console.Write($"{ds.Tables["note"].Rows[nid - 1][i]}\t");
                }
                catch (Exception)
                {
                    Console.WriteLine($"ID not exist with number{nid}");
                    return;
                }
            }
            Console.WriteLine();
        }
        public static void delete_note(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from keepNote", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "note");
            var row = ds.Tables[0].NewRow();
            Console.WriteLine("enter ID");

            int nid = Convert.ToInt16(Console.ReadLine());

            try
            {
                ds.Tables["note"].Rows[nid - 1].Delete();
            }
            catch (Exception)
            {
                Console.WriteLine($"ID not exist with number{nid}");
                return;
            }
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds, "note");
            Console.WriteLine("Row deleted Successfully");

        }
        public static void update_note(SqlConnection con)
        {
            Console.WriteLine("enter updated id");
            int nid = Convert.ToInt16(Console.ReadLine());
            
                SqlDataAdapter adp = new SqlDataAdapter($"Select * from keepNote where Id={nid}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "note");
            try 
            { 
                
                ds.Tables["note"].Rows[0][1] = Console.ReadLine();
                Console.WriteLine("Enter Title");
                Console.WriteLine("Enter description");
                ds.Tables["note"].Rows[0][2] = Console.ReadLine();
                DateTime date = new DateTime();
                Console.Write("Enter Date(DD/MM/YYYY): ");
                ds.Tables["note"].Rows[0][3] = DateTime.Parse(Console.ReadLine());
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds, "note");
                Console.WriteLine("Database Updated");
            }
            catch
            {
                Console.WriteLine($"ID not exist with number{nid}");
            }

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=TestDB; Integrated Security=true");
            string res ;
            do
            {
                Console.WriteLine("1 Create Note");
                Console.WriteLine("2 View Note");
                Console.WriteLine("3 View All Notes");
                Console.WriteLine("4 Update Note");
                Console.WriteLine("5 Delete Note");
                Console.WriteLine("Choose the option");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            keepnote.createNote(con);
                            break;
                        }
                    case 2:
                        {
                            keepnote.view_note(con);
                            break;
                        }
                    case 3:
                        {
                            keepnote.view_all_Note(con);
                            break;
                        }
                    case 4:
                        {
                            keepnote.update_note(con);
                            break;
                        }
                    case 5:
                        {
                            keepnote.delete_note(con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Select 1 to 5 numbers");
                            break;
                        }
                

                }
                Console.WriteLine("Do you wish to continue? [y/n] ");
                res = Console.ReadLine();
            } while (res.ToLower() == "y");
        }
    }

}