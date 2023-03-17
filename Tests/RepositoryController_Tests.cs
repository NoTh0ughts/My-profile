using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MyProfile.Controllers;

namespace Tests;

public class RepositoryController_Tests
{
    [Fact]
    public async Task ResultNotNull()
    {
        // Создание нового источника данных
        var optionsBuilder = new DbContextOptionsBuilder<MyProjectsContext>();
        optionsBuilder.UseInMemoryDatabase("temp_projects");
        
        var controller = new RepositoryController(new Mock<ILogger<RepositoryController>>().Object , 
            new MyProjectsContext(optionsBuilder.Options));
        var result = await controller.Get(default);
        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ResultEquals()
    {
        // Создание нового источника данных
        var optionsBuilder = new DbContextOptionsBuilder<MyProjectsContext>();
        optionsBuilder.UseInMemoryDatabase("temp_projects");
        
        // Открываем источник данных
        var ctx = new MyProjectsContext(optionsBuilder.Options);
        Assert.NotNull(ctx);
        
        // Добавляем новую запись в БД
        var project = new Project
        {
            CreatedAt = DateTime.Now,
            MainTitle = "Hello",
            RepositoryName = "World",
            SubTitle = "Idk",
            UpdatedAt = DateTime.Now
        };
        ctx.Projects.Add(project);
        await ctx.SaveChangesAsync();
        
        // Создаем контроллер
        var controller = new RepositoryController(new Mock<ILogger<RepositoryController>>().Object, ctx);
        Assert.NotNull(controller);
        
        // Получаем данные с контроллера
        var actionResult = await controller.Get(default);
        
        // Если запрос не успешный, то тест провален
        Assert.NotNull(actionResult);
        Assert.IsType<OkObjectResult>(actionResult);

        // Преобразуем результат и проверяем
        var okResult = actionResult as OkObjectResult;
        var projects = okResult.Value as IEnumerable<Project>;
        
        Assert.NotNull(projects);
        Assert.Equal(projects.FirstOrDefault().CreatedAt, project.CreatedAt);
    }
}