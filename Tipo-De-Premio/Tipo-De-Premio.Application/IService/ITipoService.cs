

using Application.DTO;
using Domain.Interfaces;

public interface ITipoService
{
    Task<TipoDTO> Add(TipoDTO tipoDTO);
    Task<bool> Exists(Guid Id);
    Task<IEnumerable<ITipo>> GetAll();
    Task<ITipo?> GetById(Guid id);
    Task AddConsumed(Guid Id, string descricao);
    

 
}