using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

// This whole file runs once, at startup, to configure the app.
var builder = WebApplication.CreateBuilder(args);

// Registers everything MVC needs: controllers, views, model binding, etc.
builder.Services.AddControllersWithViews();

// This tells the app: "whenever a controller asks for an
// ApplicationDbContext, hand it one configured to talk to SQL Server
// using the connection string from appsettings.json." This is what
// makes the "public StudentController(ApplicationDbContext context)"
// constructor in Step 6 work without us ever writing "new".
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// From here down we're configuring the "middleware pipeline" — the chain
// of steps every HTTP request passes through, in order.
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();  // redirect http:// requests to https://
app.UseStaticFiles();       // serve files from wwwroot (css, js, images)
app.UseRouting();           // figure out WHICH endpoint matches this URL
app.UseAuthorization();     // check permissions (nothing restricted yet)

// CUSTOM ROUTE — must be registered BEFORE the default route,
// because routing matches top to bottom and stops at the first match.
// This says: any URL like "/greet/Something" should run
// GreetingController.Hello(), with "Something" bound to the name parameter.
app.MapControllerRoute(
    name: "greeting",
    pattern: "greet/{name}",
    defaults: new { controller = "Greeting", action = "Hello" });

// DEFAULT convention-based route: {controller}/{action}/{id?}
// The "?" makes id optional. This single line is what lets EVERY
// controller/action in the whole app be reachable without writing
// a custom route for each one.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // starts listening for requests