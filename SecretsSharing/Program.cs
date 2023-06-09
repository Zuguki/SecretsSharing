using SecretsSharing.BL.Auth;
using SecretsSharing.BL.File;
using SecretsSharing.BL.General;
using SecretsSharing.BL.Security;
using SecretsSharing.BL.Session;
using SecretsSharing.DAL;
using SecretsSharing.DAL.File;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddSingleton<IWebCookie, WebCookie>();
builder.Services.AddSingleton<IFile, SecretsSharing.BL.File.File>();

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IFileDAL, FileDAL>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

DbHelper.ConnectionString = app.Configuration["ConnectionStrings:Default"] ?? "";

app.Run();