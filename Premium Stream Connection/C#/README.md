Premium Stream Connection
=========================

C# Examples
-----------
There are two C# examples provided for establishing a PowerTrack streaming connection.  Both use the System.Net HttpWebRequest class and its GetResponseStream mechanism.  

 
+ StreamingConnection.cs - A single class that streams data to the console using simple StreamReader.ReadLine() handling.  While this example works well for high volume data streams, where the buffer fills and gets read out quickly, it does not perform well for low volume streams.  See the next example for more information on this issue.

+ StreamingConnetion2.cs -

