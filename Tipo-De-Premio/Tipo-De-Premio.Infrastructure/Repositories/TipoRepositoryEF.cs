

using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class TipoRepositoryEF : GenericRepositoryEF<ITipo, Tipo, TipoDataModel>, ITipoRepository
{

    private readonly IMapper _mapper;


    public TipoRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> Exists(Guid ID)
    {
        var tipoDM = await _context.Set<TipoDataModel>().FirstOrDefaultAsync(u => u.Id == ID);

        if (tipoDM == null)
            return false;

        return true;

    }

    public async Task<Guid?> GetByDescricaoAsync(string descricao)
    {
        var tipoDM = await _context.Set<TipoDataModel>().FirstOrDefaultAsync(c => c.Descricao == descricao);

        if (tipoDM == null)
            return null;

        return tipoDM.Id;
    }

    public override ITipo? GetById(Guid id)
    {
        var tipoDM = _context.Set<TipoDataModel>().FirstOrDefault(c => c.Id == id);

        if (tipoDM == null)
            return null;

        var tipo = _mapper.Map<TipoDataModel, Tipo>(tipoDM);
        return tipo;

    }

    public override async Task<ITipo?> GetByIdAsync(Guid id)
    {
        var tipoDM = await _context.Set<TipoDataModel>().FirstOrDefaultAsync(u => u.Id == id);

        if (tipoDM == null)
            return null;

        var tipo = _mapper.Map<TipoDataModel, Tipo>(tipoDM);

        return tipo;
    }
}