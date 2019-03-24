using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_URL_Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            URL_Converter u = new URL_Converter();
            string fullLink = "http://blog.vkuznetsov.ru/posts/2011/09/18/malenkie-chudesa-csharp-net-datetime-s-dopolnitelnymi-preimushhestvami#.XI-c4SIzZdg";
            string fullLink2 = "https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader.close?view=netframework-4.7.2";
            //if (u.getShortLink(fullLink2) == "")
            //{
            //    Console.WriteLine("Пустая строка!");
            //    u.fillTableInDB(fullLink2);
            //}
            //else
            //{
            //    Console.WriteLine(u.getShortLink(fullLink2));
            //}
            //u.fillTableInDB(fullLink2);
            //u.getShortLink(fullLink);
            Console.WriteLine(u.getFullLink("IhJoA"));
            
        }


    }
}
