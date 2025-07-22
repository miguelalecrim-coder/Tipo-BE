using Domain.Models;

namespace Application.IPublishers;

public interface IMessagePublisher
{
    Task PublishCreatedTipoMessageAsync(Guid id, string descricao);
}