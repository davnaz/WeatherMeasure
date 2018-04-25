using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace WeatherMeasure
{
    static class WebHelper
    {

        public static string GetHtml(string link) //получаем страницу в виде строки, которую будем парсить
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(link);
            req.Method = "GET";
            req.Timeout = 2000;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            req.KeepAlive = true;
            req.AllowAutoRedirect = true;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Console.WriteLine(res.StatusCode + " , " + (int)res.StatusCode);
                System.IO.Stream ReceiveStream = res.GetResponseStream();
                System.IO.StreamReader sr2 = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
                //Кодировка указывается в зависимости от кодировки ответа сервера
                Char[] read = new Char[256];
                int count = sr2.Read(read, 0, 256);
                string htmlString = String.Empty;
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    htmlString += str;
                    count = sr2.Read(read, 0, 256);
                }
                return htmlString;
            }
            catch
            {
                return "Not found";
            }
        }
    }
}
