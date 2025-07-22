using Application.DTO;
using Application.IPublishers;
using Domain.Messages;
using MassTransit;

public class EdicaoWithoutTipoCreatedConsumer : IConsumer<EdicaoWithoutTipoCreatedMessage>
{
    private readonly ITipoService _tipoService;

    private readonly IMessagePublisher _publisher;

    public EdicaoWithoutTipoCreatedConsumer(ITipoService tipoService, IMessagePublisher publisher)
    {
        _tipoService = tipoService;
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<EdicaoWithoutTipoCreatedMessage> context)
    {
        var msg = context.Message;
        Console.WriteLine($"Received TipoCreatedMessage: Descricao = {msg.Descricao}");

        var tipoDTO = new TipoDTO
        {
            Descricao = msg.Descricao,
        };

        await _tipoService.Add(tipoDTO);
    }
}
