using System.Collections;
using System.Net;
using Shared.Http;
using Smdb.Core.Users;

namespace SimpleMDB;

public class AuthController
{
	private readonly IUserService userService;

	public AuthController(IUserService userService)
	{
		this.userService = userService;
	}

	public async Task LandingPageGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, Func<Task> next)
	{
		string html = HtmlTemplates.Base("SimpleMDB", "Landing Page", "Hello World!");
		await HttpUtils.SendOkResponse(req, res, options, html, "text/html");
	}
}