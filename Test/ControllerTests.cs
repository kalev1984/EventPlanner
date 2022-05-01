using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using Xunit;

namespace Test;

public class ControllerTests
{
    private IEnumerable<Event> GetTestEvents()
    {
        var sessions = new List<Event>();
        sessions.Add(new Event
        {
            Time = new DateTime(2016, 7, 2),
            Id = 1,
            EventName = "Test One",
            Comments = "Test Comment",
            Place = "Test"
        });
        sessions.Add(new Event
        {
            Time = DateTime.Now.AddDays(1),
            Id = 2,
            EventName = "Test Two",
            Comments = "Test Comment 2",
            Place = "Test2"
        });
        return sessions;
    }
    
    [Fact]
    public async Task TestReturnEvents()
    {
        var mockRepo = new Mock<IAppUnitOfWork>();
        mockRepo.Setup(repo => repo.Events.GetAllAsync(true))
            .ReturnsAsync(GetTestEvents());
        var controller = new HomeController(mockRepo.Object);
        
        var result = await controller.Index();
        
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Event>>(
            viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }
}