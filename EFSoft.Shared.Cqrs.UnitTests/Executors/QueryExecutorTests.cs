namespace EFSoft.Shared.Cqrs.UnitTests.Executors;

public class QueryExecutorTests
{
    [Fact]
    public void Constructor_WithNullServiceProvider_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new QueryExecutor(null));
    }

    [Fact]
    public async Task ExecuteAsync_CallsExpectedHandler()
    {
        var queryResult = new UnitTestsMocks.QueryResult();

        var handlerMock = new Mock<IQueryHandler<UnitTestsMocks.QueryParameters, UnitTestsMocks.QueryResult>>();
        handlerMock.Setup(h => h.HandleAsync(It.IsAny<UnitTestsMocks.QueryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        var serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock
            .Setup(p => p.GetService(typeof(IQueryHandler<UnitTestsMocks.QueryParameters, UnitTestsMocks.QueryResult>)))
            .Returns(handlerMock.Object);

        var executor = new QueryExecutor(serviceProviderMock.Object);

        var result = await executor.ExecuteAsync<UnitTestsMocks.QueryParameters, UnitTestsMocks.QueryResult>(new UnitTestsMocks.QueryParameters());

        Assert.Equal(queryResult, result);

        handlerMock.Verify(
            h => h.HandleAsync(It.IsAny<UnitTestsMocks.QueryParameters>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}