using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class TipoDataModelConverter : ITypeConverter<TipoDataModel, Tipo>
{
    public readonly ITipoFactory _TipoFactory;

    public TipoDataModelConverter(ITipoFactory tipoFactory)
    {
        _TipoFactory = tipoFactory;
    }

    public Tipo Convert(TipoDataModel source, Tipo destination, ResolutionContext context)
    {
        return _TipoFactory.Create(source);
    }

    public bool UpdateDataModel(TipoDataModel tipoDataModel, Tipo tipoDomain)
    {
        tipoDataModel.Id = tipoDomain.Id;

        return true;
    }
}

