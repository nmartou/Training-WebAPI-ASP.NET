using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Store.Models;
using Store.Repositories;
using Store.Services;
using Xunit;

namespace Store.Tests.Unit.Services;
public class PriceServiceTests
{
    private readonly Mock<IRepository<Price>> _mockRepo;
    private readonly PriceService _service;

    public PriceServiceTests()
    {
        _mockRepo = new();
        _service = new(_mockRepo.Object);
    }

    [Fact]
    public async Task GetPriceByIdAsync_ReturnsPrice_WhenExists()
    {
        var expected = new Price { PriceID = 1, Value = 19.99m };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expected);

        var result = await _service.GetPriceByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(19.99m, result.Value);
    }
}