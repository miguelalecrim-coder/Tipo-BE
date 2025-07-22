


using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("Tipo")]
public class TipoDataModel : ITipoVisitor
{
    public Guid Id { get; set; }

    public string Descricao { get; set; }

    public TipoDataModel(ITipo tipo)
    {
        if (tipo.Id != Guid.Empty)
            Id = tipo.Id;

        Descricao = tipo.Descricao;
    }

    public TipoDataModel()
    {
        
    }

}
