using Board.Host.Controllers;
using Board.Host.Data;
using Board.Host.Models.Dtos;
using Board.Host.Models.Requests;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services;
using Board.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Board.Tests.Services
{
    
    public class ListIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Mock<IBoardRepository> _mockRepository;
        private readonly BoardService _boardService;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ILogger<BoardService>> _loggerService;
        private readonly Mock<ILogger<BoardController>> _loggerController;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;


        private const string apiUrl = "http://localhost:5000";

        public ListIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _loggerService = new Mock<ILogger<BoardService>>();
            _loggerController = new Mock<ILogger<BoardController>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _mockRepository = new Mock<IBoardRepository>();

            // Setup mock repository behavior
            _mockRepository.Setup(repo => repo.CreateBoardAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(1);

            // Initialize the board service with mock objects
            _boardService = new BoardService(_dbContextWrapper.Object, _loggerService.Object, _mockRepository.Object);

            // Configure the WebApplicationFactory to use the mock services
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Replace the real services with mock services
                    services.AddSingleton<IBoardRepository>(_mockRepository.Object);
                    services.AddSingleton<BoardService>(_boardService);
                    services.AddSingleton<ILogger<BoardController>>(_loggerController.Object);
                    services.AddSingleton<IDbContextWrapper<ApplicationDbContext>>(_dbContextWrapper.Object);
                });
            });

            // Create the HttpClient after configuring the factory
            _client = _factory.CreateClient();
        }


        [Fact]
        public async Task CreateBoard_Success()
        {
            var createRequest = new CreateBoardRequest { Name = "NewBoard", Description = "TestTest" };

            var createResponse = await _client.PostAsJsonAsync($"{apiUrl}/CreateBoard", createRequest);
            createResponse.EnsureSuccessStatusCode();

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var boardId = await createResponse.Content.ReadFromJsonAsync<int>();

            boardId.Should().BeGreaterThan(0);

            // Verify that the repository's CreateBoardAsync method was called
            _mockRepository.Verify(repo => repo.CreateBoardAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }


        /*[Fact]
        public async Task DeleteBoard_Success()
        {
        
            var deleteRequest = new DeleteListRequest { ListId = createdList.ListId };

            var deleteResponse = await _client.PostAsJsonAsync($"{apiUrl}/DeleteList", deleteRequest);

            deleteResponse.EnsureSuccessStatusCode();
        }*/

        [Fact]
        public async Task UpdateList_Success()
        {
            var createRequest = new CreateListRequest { StatusName = "List to Update", BoardId = 1 };

            var createResponse = await _client.PostAsJsonAsync($"{apiUrl}/CreateList", createRequest);
            createResponse.EnsureSuccessStatusCode();

            var createdList = await createResponse.Content.ReadFromJsonAsync<ListDto>();

            var updateRequest = new UpdateListRequest { ListId = createdList.ListId, StatusName = "Updated List" };

            var updateResponse = await _client.PutAsJsonAsync($"{apiUrl}/UpdateList", updateRequest);

            updateResponse.EnsureSuccessStatusCode();

            var updatedList = await updateResponse.Content.ReadFromJsonAsync<ListDto>();
            updatedList.StatusName.Should().Be("Updated List");
        }

        /*[Fact]
        public async Task GetAllLists_Success()
        {
            var response = await _client.GetAsync("/lists");

            response.EnsureSuccessStatusCode();

            var lists = await response.Content.ReadFromJsonAsync<IEnumerable<ListDto>>();
            lists.Should().NotBeNull();
        }

        [Fact]
        public async Task GetListById_Success()
        {
            var createRequest = new CreateListRequest { StatusName = "List to Get", BoardId = 1 };

            var createResponse = await _client.PostAsJsonAsync("/lists", createRequest);
            createResponse.EnsureSuccessStatusCode();

            var createdList = await createResponse.Content.ReadFromJsonAsync<ListDto>();

            var response = await _client.GetAsync($"/lists/{createdList.ListId}");

            response.EnsureSuccessStatusCode();

            var list = await response.Content.ReadFromJsonAsync<ListDto>();
            list.Should().NotBeNull();
            list.ListId.Should().Be(createdList.ListId);
            list.StatusName.Should().Be("List to Get");
        }*/

    }
}
