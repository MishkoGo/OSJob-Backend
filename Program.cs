using aspnetserver.Data;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
//Настраиваем CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", 
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            //Пишем маршрут нашего локального приложения React
            .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
       SwaggerGenOptions =>
       {
           SwaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET React", Version = "v1" });
       }
    );

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(SwaggerUIOptions =>
{
    SwaggerUIOptions.DocumentTitle = "ASP.NET React";
    SwaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json","Web API serving very simple");
    SwaggerUIOptions.RoutePrefix = string.Empty;

});

app.UseHttpsRedirection();

//Здесь сообщаем ASP.NET что мы хотим использовать его при поступлении HTTP - запроса
app.UseCors("CORSPolicy");

//Точка получения сообщений

app.MapGet("/get-all-posts", async () => await PostsRepository.GetPostsAsync())
    .WithTags("Posts Endpoints");
app.MapGet("/get-all-posts-2", async () => await PostsRepository.GetPostsActionAsync())
    .WithTags("Posts Endpoints");

//Добавляем конечную точку получения для определенного сообщдения с идентификатором сообщения
app.MapGet("/get-post-by-id/{postId}", async (int postId) =>
{
    Post postToReturn = await PostsRepository.GetPostByIdAsync(postId);
    if(postToReturn != null)
    {
        //Позволяет нам возвращать определенные HTTP методы
        return Results.Ok(postToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts EndPoints");

//Создаем createPost
app.MapPost("/create-post", async (Post postToCreate) =>
{
    bool createSuccessful = await PostsRepository.CreatePostAsync(postToCreate);     
    if (createSuccessful)
    {
        //Позволяет нам возвращать определенные HTTP методы
        return Results.Ok("Create successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts EndPoints");
app.MapPost("/create-post-2", async (Posts_action postActionToCreate) =>
{
    bool createSuccessful = await PostsRepository.CreateActionPostAsync(postActionToCreate);
    if (createSuccessful)
    {
        //Позволяет нам возвращать определенные HTTP методы
        return Results.Ok("Create successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts EndPoints");
/*app.MapPost("/create-post_1", async (Posts_action postActionToCreate) =>
{
    bool createSuccessful = await PostsRepository.CreateActionPostAsync(postActionToCreate);
    if (createSuccessful)
    {
        //Позволяет нам возвращать определенные HTTP методы
        return Results.Ok("Create successful");
    }
    else
    {
        return Results.BadRequest();
    }

}).WithTags("Posts EndPoints");
*/

//Создаем UpdatePost
app.MapPut("/update-post", async (Post postToUpdate) =>
{
    bool updateSuccessful = await PostsRepository.UpdatePostAsync(postToUpdate);
    
    if (updateSuccessful)
    {
        //Позволяет нам возвращать определенные HTTP методы
        return Results.Ok("Update successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts EndPoints");


//Создаем DeletePost
app.MapDelete("/delete-post-by-id/{postId}", async (int postId) =>
{
    bool deleteSuccessful = await PostsRepository.DeletePostAsync(postId);

    if (deleteSuccessful)
    {
        //Позволяет нам возвращать определенные HTTP методы
        return Results.Ok("Delete successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts EndPoints");



app.Run();
