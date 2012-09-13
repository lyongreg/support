using System;
using System.IO;
using System.Net;
using System.Text;


namespace StreamingConnection
{
  class MainClass
  {
    public static void Main (string[] args)
    {
      HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create("YOUR_STREAM_URL_HERE");
      request.Method = "GET";
       
      //Setup Credentials
      NetworkCredential nc = new NetworkCredential("YOUR_USERNAME_HERE", "YOUR_PASSWORD_HERE");
      request.Credentials = nc;
      
      request.PreAuthenticate = true;
      request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
      request.Headers.Add("Accept-Encoding", "gzip");
      request.Accept = "application/json";
      request.ContentType = "application/json";
          
      Stream objStream;
      objStream = request.GetResponse().GetResponseStream();

      StreamReader objReader = new StreamReader(objStream);

      string sLine = "";
       
      while (!objReader.EndOfStream)
      {
        sLine = objReader.ReadLine();
        if (sLine!=null)
          Console.WriteLine(sLine);
      }
      Console.ReadLine();      
    }
  }
}
