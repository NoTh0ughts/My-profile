using System.Collections;
using AutoMapper;
using Data;
using Data.Models;
using Data.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MyProfile.Controllers;
using MyProfile.Services.Cache;

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
            new MyProjectsContext(optionsBuilder.Options), new Mock<IMapper>().Object, new Mock<ICacheService>().Object);
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
        var cacheService = new Mock<ICacheService>();
        cacheService.Setup(service => 
            service.GetCachedDataAsync<IEnumerable<ProjectDTO>>(It.IsAny<string>()))
            .ReturnsAsync(() => null!);
        
        // Создаем мок для IMapper
        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<IEnumerable<ProjectDTO>>(It.IsAny<IEnumerable<Project>>()))
            .Returns((IEnumerable<Project> sourceProjects) =>
            {
                return sourceProjects.Select(p => new ProjectDTO
                {
                    CreatedAt = p.CreatedAt,
                    MainTitle = p.MainTitle,
                    RepositoryName = p.RepositoryName,
                    SubTitle = p.SubTitle,
                    UpdatedAt = p.UpdatedAt
                }).ToList();
            });
        
        var controller = new RepositoryController(
            new Mock<ILogger<RepositoryController>>().Object , 
            new MyProjectsContext(optionsBuilder.Options), 
            mapper.Object, cacheService.Object);
        Assert.NotNull(controller);
        
        // Получаем данные с контроллера
        var actionResult = await controller.Get(default);
        
        // Если запрос не успешный, то тест провален
        Assert.NotNull(actionResult);
        Assert.IsType<OkObjectResult>(actionResult);

        // Преобразуем результат и проверяем
        var okResult = actionResult as OkObjectResult;
        var projects = okResult?.Value as IEnumerable<ProjectDTO>;
        
        Assert.NotNull(projects);
        Assert.Equal(project.CreatedAt, projects.FirstOrDefault()?.CreatedAt);
    }
}