using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using RevenueRecognitionSystem.Data;
using RevenueRecognitionSystem.Models.Domain;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Tests;

public class RevenueServiceTests
{
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly Mock<ICurrencyService> _mockCurrencyService;
    private readonly RevenueService _revenueService;

    public RevenueServiceTests()
    {
        var contracts = new List<Contract>
        {
            new Contract
            {
                ContractId = 1,
                Version = "1.0",
                IsSigned = true,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 101
                    },
                    new Payment
                    {
                        Amount = 101
                    },
                    new Payment
                    {
                        Amount = 101
                    }
                }
            },
            new Contract
            {
                ContractId = 2,
                Version = "1.1",
                IsSigned = false,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 101
                    },
                    new Payment
                    {
                        Amount = 101
                    },
                    new Payment
                    {
                        Amount = 101
                    }
                }
            },
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Contract>>();
        mockSet.As<IQueryable<Contract>>().Setup(m => m.Provider).Returns(contracts.Provider);
        mockSet.As<IQueryable<Contract>>().Setup(m => m.Expression).Returns(contracts.Expression);
        mockSet.As<IQueryable<Contract>>().Setup(m => m.ElementType).Returns(contracts.ElementType);
        mockSet.As<IQueryable<Contract>>().Setup(m => m.GetEnumerator()).Returns(contracts.GetEnumerator());

        var mockContext = new Mock<DatabaseContext>();
        mockContext.Setup(c => c.Set<Contract>()).Returns(mockSet.Object);

        var mockCurrencyService = new Mock<ICurrencyService>();

        _revenueService = new RevenueService(mockContext.Object, mockCurrencyService.Object);

    }
    
    [Fact]
    public async Task CalculateRevenue_WithMockedValues_ReturnsExpectedValue()
    {
        // Arrange
        // _mockCurrencyService.Setup(service => service.GetExchangeRate(It.IsAny<string>()))
        //     .ReturnsAsync(1);

        // Act
        var actualRevenue = await _revenueService.CalculateRevenue(null);

        // Assert
        double expectedRevenue = 303 * 1; 
        Assert.Equal(expectedRevenue, actualRevenue);
    }

    [Fact]
    public async Task CalculateRevenue_ReturnsExpectedValue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var mockCurrencyService = new Mock<ICurrencyService>();
        
        mockCurrencyService.Setup(service => service.GetExchangeRate(It.IsAny<string>()))
            .ReturnsAsync(1);

        await using (var context = new DatabaseContext(options))
        {
            
            context.Contracts.Add(new Contract
            {
                Version = "1.0",
                IsSigned = true, 
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 101
                    },
                    new Payment
                    {
                        Amount = 101
                    },
                    new Payment
                    {
                        Amount = 101
                    }
                }
            });
            await context.SaveChangesAsync();
        }

        // Act
        double expectedRevenue = 303;

        await using (var context = new DatabaseContext(options))
        {
            var service = new RevenueService(context, mockCurrencyService.Object);
            var actualRevenue = await service.CalculateRevenue(null);

            // Assert
            Assert.Equal(expectedRevenue, actualRevenue);
        }
    }
}