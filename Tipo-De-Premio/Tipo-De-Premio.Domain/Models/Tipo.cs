

using Domain.Interfaces;

namespace Domain.Models;

public class Tipo : ITipo
{
    public Guid Id { get; private set; }

    public string Descricao { get; private set; }

    public Tipo(string descricao)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
    }

    public Tipo(Guid id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }

    

}