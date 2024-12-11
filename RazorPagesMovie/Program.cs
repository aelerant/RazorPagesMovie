using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//向依赖关系注入 (DI) 容器添加 Razor Pages 支持，并生成应用
builder.Services.AddRazorPages();
//通过依赖关系注入注册的上下文
builder.Services.AddDbContext<RazorPagesMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//开发人员异常页可能会泄露敏感信息
//将异常终结点设置为 /Error，并且当应用未在开发模式中运行时，启用 HTTP 严格传输安全协议 (HSTS)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//将 HTTP 请求重定向到 HTTPS
app.UseHttpsRedirection();
//使能够提供 HTML、CSS、映像和 JavaScript 等静态文件
app.UseStaticFiles();
//向中间件管道添加路由匹配
app.UseRouting();
//授权用户访问安全资源
app.UseAuthorization();
//为 Razor Pages 配置终结点路由
app.MapRazorPages();
//运行应用
app.Run();
