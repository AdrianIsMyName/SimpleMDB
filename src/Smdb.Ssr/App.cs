using System.Net;
using Shared.Http;

using Smdb.Core.Db;
using Smdb.Core.Users;
using Smdb.Api.Users;

namespace SimpleMDB;

public class App
{
	private HttpListener server;
	private HttpRouter router;

	public App()
	{
		string host = "http://localhost:8081/";
		server = new HttpListener();
		router = new HttpRouter();

		server.Prefixes.Add(host);

		Console.WriteLine($"Server listening on... " + host);

		var db = new MemoryDatabase();

		var userRepo = new MemoryUserRepository(db);
		var userServ = new DefaultUserService(userRepo);
		var userCtrl = new UsersController(userServ);
		var authController = new AuthController(userServ);

		// Add middleware
		router.Use(HttpUtils.StructuredLogging);
		router.Use(HttpUtils.CentralizedErrorHandling);
		router.Use(HttpUtils.AddResponseCorsHeaders);
		router.Use(HttpUtils.DefaultResponse);
		router.Use(HttpUtils.ParseRequestUrl);
		router.Use(HttpUtils.ParseRequestQueryString);

		// Enable route matching
		router.UseSimpleRouteMatching();

		// Define routes
		router.MapGet("/", authController.LandingPageGet);
		router.MapGet("/users", userCtrl.ReadUsers);
	}

	public async Task Start()
	{
		server.Start();

		while (server.IsListening)
		{
			var ctx = server.GetContext();
			await HandleContextAsync(ctx);
		}
	}

	public void Stop()
	{
		server.Stop();
		server.Close();
	}

	private async Task HandleContextAsync(HttpListenerContext ctx)
	{
		await router.HandleContextAsync(ctx);
	}

}