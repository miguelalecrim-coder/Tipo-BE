using Application.DTO;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Application.IPublishers;

namespace Application.Services;


public class TipoService : ITipoService
{

    private ITipoRepository _tipoRepository;

    private ITipoFactory _tipoFactory;

    private  IMapper _mapper;
    private readonly IMessagePublisher _publisher;



    public TipoService(ITipoRepository tipoRepository, ITipoFactory tipoFactory, IMapper mapper,IMessagePublisher publisher)
    {
        _tipoRepository = tipoRepository;
        _tipoFactory = tipoFactory;
        _mapper = mapper;
        _publisher = publisher;
    }


    public async Task<TipoDTO> Add(TipoDTO tipoDTO)
    {
        var tipo = await _tipoFactory.Create(tipoDTO.Descricao);
        await _tipoRepository.AddAsync(tipo);
        await _tipoRepository.SaveChangesAsync();

        await _publisher.PublishCreatedTipoMessageAsync(tipo.Id, tipo.Descricao);

        return _mapper.Map<Tipo, TipoDTO>(tipo);
    }

    public async Task AddConsumed(Guid Id, string descricao)
    {
        if (await Exists(Id)) return;

        var visitor = new TipoDataModel()
        {
            Id = Id,
            Descricao = descricao
        };

        var tipo = _tipoFactory.Create(visitor);

        await _tipoRepository.AddAsync(tipo);
        await _tipoRepository.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid Id)
    {
        return await _tipoRepository.Exists(Id);
    }

    public async Task<IEnumerable<ITipo>> GetAll()
    {
        var Tipo = await _tipoRepository.GetAllAsync();
        return Tipo;
    }

    public async Task<ITipo?> GetById(Guid id)
    {
        var Tipo = await _tipoRepository.GetByIdAsync(id);
        return Tipo;
    }
}