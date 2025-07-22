using Application.IPublishers;
using Domain.Messages;
using MassTransit;

namespace WebApi.Publishers;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishCreatedTipoMessageAsync(Guid id, string descricao)
    {
        Console.WriteLine("Publishing TipoCreatedMessage...");
        await _publishEndpoint.Publish(new TipoCreatedMessage(id, descricao));
    }
    
}