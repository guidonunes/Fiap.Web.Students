using Fiap.Web.Students.Controllers;
using Fiap.Web.Students.Data;
using Fiap.Web.Students.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Fiap.Web.Students.Test;

public class ClientControllerTest
{

    private readonly Mock<DatabaseContext> _mockContext;
    
    private readonly ClientController _clientController;

    private readonly DbSet<ClientModel> _mockSet;
    
    public ClientControllerTest()
    {
        _mockContext = new Mock<DatabaseContext>();
        _mockSet = MockDbSet();
        _mockContext.Setup(m => m.Client).Returns(_mockSet);
        _clientController = new ClientController(_mockContext.Object);
    }
    
    private DbSet<ClientModel> MockDbSet()
    {
        // Lista de clientes para simular dados no banco de dados
        var data = new List<ClientModel>
        {
            new ClientModel { ClientId = 1, FirstName = "Cliente 1" },
            new ClientModel { ClientId = 2, FirstName = "Cliente 2" }
        }.AsQueryable();
        // Cria o mock do DbSet
        var mockSet = new Mock<DbSet<ClientModel>>();
        // Configura o comportamento do mock DbSet para simular uma consulta ao banco de dados
        mockSet.As<IQueryable<ClientModel>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<ClientModel>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<ClientModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<ClientModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        // Retorna o DbSet mock
        return mockSet.Object;
    }

    [Fact]
    public void Index_ReturnViewWithClients()
    {
        //Action
        var result = _clientController.Index();
        
        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        
        //var model = Assert.IsAssignableFrom<ClientModel>(viewResult.ViewData.Model);
        var model = Assert.IsAssignableFrom<IEnumerable<ClientModel>>(viewResult.ViewData.Model);
        
        Assert.Equal(2, model.Count());
    }
    
    [Fact]
    public void Index_ReturnViewWithoutClients()
    {
        
        //Arrange
        _mockSet.RemoveRange(_mockSet.ToList());
        _mockContext.Setup(m => m.Client).Returns(_mockSet);
        
        //Action
        var result = _clientController.Index();
        
        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        
        //var model = Assert.IsAssignableFrom<ClientModel>(viewResult.ViewData.Model);
        var model = Assert.IsAssignableFrom<IEnumerable<ClientModel>>(viewResult.ViewData.Model);
        
        Assert.Empty(model);
    }
}