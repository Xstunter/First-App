using Board.Host.Data;
using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;
using Board.Host.Models.Requests;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services;
using Board.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;

namespace Board.Tests.Services
{
    public class ListServiceUnitTest
    {
        private readonly IListService _listService;

        private readonly Mock<IListRepository> _listRepository;
        private readonly Mock<ILogger<ListService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;

        private readonly ListDto _testList = new ListDto()
        {
            ListId = 1,
            StatusName = "test",
            BoardId = 1
        };

        public ListServiceUnitTest()
        {
            _listRepository = new Mock<IListRepository>();
            _logger = new Mock<ILogger<ListService>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            _listService = new ListService(_dbContextWrapper.Object, _logger.Object, _listRepository.Object);
        }

        [Fact]

        public async Task CreateListAsync_Success()
        {
            var testResult = 1;

            _listRepository.Setup(s => s.CreateListAsync(
                It.IsAny<string>(),
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _listService.CreateListAsync(_testList.StatusName, _testList.BoardId);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task CreateListAsync_Failed()
        {
            int? testResult = null;

            _listRepository.Setup(s => s.CreateListAsync(
                It.IsAny<string>(),
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _listService.CreateListAsync(_testList.StatusName, _testList.BoardId);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task DeleteListAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            _listRepository.Setup(s => s.DeleteListAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _listService.DeleteListAsync(testId);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteListAsync_Failed()
        {
            bool testResult = false;
            int testId = 9999;

            _listRepository.Setup(s => s.DeleteListAsync(
               It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _listService.DeleteListAsync(testId);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task UpdateListAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            _listRepository.Setup(s => s.UpdateListAsync(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _listService.UpdateListAsync(testId, _testList.StatusName);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateListAsync_Failed()
        {
            bool testResult = false;
            int testId = 9999;

            _listRepository.Setup(s => s.UpdateListAsync(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _listService.UpdateListAsync(testId, _testList.StatusName);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task GetAllListAsync_Success()
        {
            int testId = 1;
            var expectedListDtos = new List<ListDto> { _testList };

            _listRepository.Setup(s => s.GetAllListAsync(It.IsAny<int>())).ReturnsAsync(expectedListDtos);

            var result = await _listService.GetAllListAsync(testId);

            result.Should().BeEquivalentTo(expectedListDtos);
        }

        [Fact]
        public async Task GetAllListAsync_Failed()
        {
            int testId = 9999;
            var expectedListDtos = new List<ListDto> { };

            _listRepository.Setup(s => s.GetAllListAsync(It.Is<int>(id => id == testId))).ReturnsAsync(expectedListDtos);

            var result = await _listService.GetAllListAsync(testId);

            result.Should().BeEmpty();
        }


        [Fact]

        public async Task GetListAsync_Success()
        {
            int testId = 1;
            var expectedListDtos = _testList ;

            _listRepository.Setup(s => s.GetListAsync(It.IsAny<int>())).ReturnsAsync(expectedListDtos);

            var result = await _listService.GetListAsync(testId);

            result.Should().BeEquivalentTo(expectedListDtos);
        }

        [Fact]
        public async Task GetListAsync_Failed()
        {
            int testId = 9999;
            ListDto expectedListDto = null;

            _listRepository.Setup(s => s.GetListAsync(It.Is<int>(id => id == testId))).ReturnsAsync(expectedListDto);

            var result = await _listService.GetListAsync(testId);

            result.Should().BeNull();
        }
    }
}
