


using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITipoFactory
{
    public Task<Tipo> Create(string descricao);

    public Tipo Create(ITipoVisitor tipoVisitor);
}