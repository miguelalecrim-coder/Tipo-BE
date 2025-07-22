

using Domain.Messages;
using MassTransit;

public class TipoCreatedConsumer : IConsumer<TipoCreatedMessage>
{
    public readonly ITipoService _tipoService;

    public TipoCreatedConsumer(ITipoService tipoService)
    {
        _tipoService = tipoService;
    }

    public async Task Consume(ConsumeContext<TipoCreatedMessage> context)
    {
        var msg = context.Message;
        await _tipoService.AddConsumed(msg.Id,msg.Descricao);
    }

    
}