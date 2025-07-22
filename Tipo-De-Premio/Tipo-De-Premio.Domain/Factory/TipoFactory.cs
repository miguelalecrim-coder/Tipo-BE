
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;


public class TipoFactory : ITipoFactory
{

    private readonly ITipoRepository _tipoRepository;

    public TipoFactory(ITipoRepository tipoRepository)
    {
        _tipoRepository = tipoRepository;
    }

    public async Task<Tipo> Create(string descricao)
    {
        var existingTipo = await _tipoRepository.GetByDescricaoAsync(descricao);

        if (existingTipo != null)
            throw new ArgumentException("An Tipo with the same descricão already exists.");
            
        return new Tipo(descricao);
    }

    public Tipo Create(ITipoVisitor tipoVisitor)
    {
        return new Tipo(tipoVisitor.Id, tipoVisitor.Descricao);
    }
}