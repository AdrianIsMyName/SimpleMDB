using System.Collections;
using System.Net;

namespace SimpleMDB;

public class AuthController
{
	public AuthController()
	{
		
	}

	public async Task LandingPageGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, Func<Task> next)
	{
		string html = "Hello World!";
		byte[] content = System.Text.Encoding.UTF8.GetBytes(html);

		res.StatusCode = (int) HttpStatusCode.OK;
		res.ContentEncoding = System.Text.Encoding.UTF8;
		res.ContentType = "text/plain";
		res.ContentLength64 = content.LongLength;
		await res.OutputStream.WriteAsync(content);
		res.Close();
	}
}