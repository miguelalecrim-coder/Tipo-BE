using Moq;
using Xunit;

namespace WebApi.IntegrationTests.TipoCreatedConsumerTests;


public class TipoCreatedConsumerConsumeTest
{
    [Fact]
    public async Task WhenMessageIsConsumed_ThenServiceMethodIsCalledWithData()
    {
        //Arrange
        var serviceMock = new Mock<ITipoService>();
        var consumer = new TipoCreatedConsumerConsumer(serviceMock.Object);

        var messageId = new Guid();
        var descricao = "Teste123";

        var message = new TipoCreatedMessage(messageId, descricao);

        var contextMock = Mock.Of<ConsumeContext<TipoCreatedMessage>>(c => c.Message == message);

        //Act

        await consumer.Consume(contextMock);

        //Assert

        serviceMock.Verify(s => s.AddConsumed(messageId, descricao));        

    }
}