namespace EFSoft.Shared.Cqrs.UnitTests.Executors;

public class CommandExecutorTests
{
    [Fact]
    public void Constructor_WithNullServiceProvider_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new CommandExecutor(null));
    }

    [Fact]
    public async Task ExecuteAsync_CallsExpectedHandler()
    {
        var handlerMock = new Mock<ICommandHandler<UnitTestsMocks.Command>>();

        var serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock.Setup(p => p.GetService(typeof(ICommandHandler<UnitTestsMocks.Command>)))
            .Returns(handlerMock.Object);

        var executor = new CommandExecutor(serviceProviderMock.Object);

        await executor.ExecuteAsync(new UnitTestsMocks.Command());

        handlerMock.Verify(h => h.HandleAsync(It.IsAny<UnitTestsMocks.Command>()), Times.Once);
    }
}