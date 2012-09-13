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
			op.addRule();
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

		public void addRule()
		{
			HttpWebRequest request = makeRequest();
			
			string postData = "";
			string rule = "testRule";
                        string tag = "testTag";

			request.Method = "POST";
			postData = "{\"rules\":[{\"tag\":\"" + tag + "\",\"value\":\"" + rule + "\"}]}";
   


			byte[] byteArray = Encoding.UTF8.GetBytes (postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream ();			
            // Write the data to the request stream.
            dataStream.Write (byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close ();

            // Get the response.
            WebResponse response = request.GetResponse ();
            // Display the status.
            Console.WriteLine (((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream ();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader (dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd ();
            // Display the content.
            Console.WriteLine (responseFromServer);
			Console.WriteLine();
            // Clean up the streams.
            reader.Close ();
            dataStream.Close ();
            response.Close ();
		}		
	}
}
