using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace HW1_URL_Converter
{
    public class UrlDB
    {
        public int Id { get; set; }
        public string FullUrl { get; set; }
        public string ShortUrl { get; set; }
        public int Count { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpDate { get; set; }

    }
    public class URL_Converter
    {
        public string connectionString =
              "Integrated Security=SSPI;"
            + "Persist Security Info=False;"
            + "Initial Catalog = URL;"
            + "Data Source = DELL\\SQLEXPRESS";

        public void ConvertURL()
        {
            //Console.WriteLine("Что вы хотите сделать?");
            //Console.WriteLine("1. Получить короткую URL ссылку");
            //Console.WriteLine("2. Получить полную URL ссылку");

            //Console.ReadLine();

        }

        public string getFullLink(string shortLink)
        {
            string FullLink = string.Empty;


            string executeSql = $"SELECT * FROM [dbo].[Url] where [ShortUrl]='{shortLink}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(executeSql, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FullLink = reader["FullUrl"].ToString();
                    }
                    return FullLink;
                }
                else
                {
                    Console.WriteLine("Ссылка не найдена");
                    return FullLink;
                }
            }
        }

        public string getShortLink(string fullLink)
        {
            string ShortLink = string.Empty;

            string executeSql = $"SELECT * FROM [dbo].[Url] where [FullUrl]='{fullLink}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(executeSql, connection);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Int32.Parse(reader["Count_"].ToString()) > 5)
                        {
                            ShortLink = "Limit for link excedeed";
                            Console.WriteLine(ShortLink);
                        }
                        else
                        {
                            ShortLink = reader["ShortUrl"].ToString();
                        }

                    }
                    updateCountColumn(fullLink);
                    return ShortLink;
                }
                else
                {
                    Console.WriteLine("Ссылка не найдена");
                    return ShortLink;
                }
            }
        }

        public string generateShortLink()
        {
            string shortLink = string.Empty;
            int count = 0;
            Random r = new Random();
            while (count < 5)
            {
                char ch = (char)r.Next(64, 122);
                if (Char.IsLetter(ch))
                {
                    shortLink += ch;
                    count++;
                }

            }
            return shortLink;
        }

        public void fillTableInDB(string fullLink)
        {
            string shortLink = generateShortLink();
            int count = 1;
            DateTime createDate = DateTime.Now;
            DateTime expdate = new DateTime(2019, 4, 10);

            string executeSql = $"INSERT INTO [dbo].[Url](FullUrl, ShortUrl, Count_,CreateDate,ExpDate) VALUES " +
           $"('{fullLink}','{shortLink}','{count}','{createDate}','{expdate}');";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(executeSql, connection);
                command.ExecuteNonQuery();
            }
        }

        public void updateCountColumn(string fullLink)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string executeSql = $"Update [dbo].[Url] set Count_= Count_+ 1 where FullUrl='{fullLink}'";

                SqlCommand command = new SqlCommand(executeSql, connection);

                command.ExecuteNonQuery();

            }
        }
    }
}
