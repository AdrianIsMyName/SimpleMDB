using System.Collections;
using System.Net;
using Shared.Http;

namespace SimpleMDB;

public class AuthController
{
	public AuthController()
	{
		
	}

	public async Task LandingPageGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, Func<Task> next)
	{
    string html = HtmlTemplates.Base("SimpleMDB", "Landing Page", "Hello World!");
    await HttpUtils.SendOkResponse(req, res, options, html, "text/html");
	}
}