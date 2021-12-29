namespace EFSoft.Shared.Cqrs.UnitTests.Executors;

public class CommandExecutorWithResultTests
{
    [Fact]
    public void Constructor_WithNullServiceProvider_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new CommandExecutorWithResult(null));
    }

    [Fact]
    public async Task ExecuteAsync_CallsExpectedHandler()
    {
        var commandResult = new UnitTestsMocks.CommandResult();

        var handlerMock = new Mock<ICommandHandlerWithResult<UnitTestsMocks.Command, UnitTestsMocks.CommandResult>>();
        handlerMock.Setup(h => h.HandleAsync(It.IsAny<UnitTestsMocks.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        var serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock
            .Setup(p => p.GetService(typeof(ICommandHandlerWithResult<UnitTestsMocks.Command, UnitTestsMocks.CommandResult>)))
            .Returns(handlerMock.Object);

        var executor = new CommandExecutorWithResult(serviceProviderMock.Object);

        var result = await executor.ExecuteAsync<UnitTestsMocks.Command, UnitTestsMocks.CommandResult>(new UnitTestsMocks.Command());

        Assert.Equal(commandResult, result);

        handlerMock.Verify(
            h => h.HandleAsync(It.IsAny<UnitTestsMocks.Command>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}