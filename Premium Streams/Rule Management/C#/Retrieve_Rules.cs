using System;
using System.Net;
using System.IO;
using System.Text;

namespace BasicOps
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MainClass op = new MainClass();
			op.retrieveRules();
		}

		public HttpWebRequest makeRequest()
		{
			string urlString = "ENTER_RULES_API_URL_HERE";			

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);

			string username = "ENTER_USERNAME_HERE";
			string password = "ENTER_PASSWORD_HERE";

			NetworkCredential nc = new NetworkCredential(username, password);
			request.Credentials = nc;
			request.PreAuthenticate = true;
			request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			request.Headers.Add("Accept-Encoding", "gzip");

			return request;			
		}


		public void retrieveRules()
		{
			HttpWebRequest request = makeRequest();
            request.Method = "GET";
			HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            // Display the status.
            Console.WriteLine (((HttpWebResponse)response).StatusDescription);
            // Read the content.
			StreamReader reader = new StreamReader(response.GetResponseStream());

            string responseFromServer = reader.ReadToEnd ();
            // Display the content.
            Console.WriteLine (responseFromServer);
			Console.WriteLine();
            // Clean up the streams.
            reader.Close ();
            response.Close ();
		}

	}
}
