using System.Net;
using System.Collections;
using Shared.Http;

namespace SimpleMDB;

public class App
{
	private HttpListener server;
	private HttpRouter router;

	public App()
	{
		string host = "http://localhost:8080/";
		server = new HttpListener();
		router = new HttpRouter();

		server.Prefixes.Add(host);

		Console.WriteLine($"Server listening on... " + host);

		var authController = new AuthController();

		router.UseSimpleRouteMatching();
		router.MapGet("/", authController.LandingPageGet);
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