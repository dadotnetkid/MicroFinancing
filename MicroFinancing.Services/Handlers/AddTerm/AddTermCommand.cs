using AutoMapper;
using MediatR;
using MicroFinancing.Interfaces.Services;

namespace MicroFinancing.Services.Handlers.AddTerm;

public class AddTermCommand : IRequest<TermDto>
{
    public TermDto Term { get; set; }
}

public class AddTermCommandHandler : IRequestHandler<AddTermCommand, TermDto>
{
    private readonly IRepository<Term, long> _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public AddTermCommandHandler(IRepository<Term, long> repository,
                                                                    IMapper mapper,
                                                                    ICurrentUser currentUser)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    public async Task<TermDto> Handle(AddTermCommand request, CancellationToken cancellationToken)
    {
        var term = new Term()
        {
            Name = request.Term.Name,
            TermEnum = request.Term.TermEnum,
            Number = request.Term.Number,
            CreatorUserId = _currentUser.UserId,

        };

        await _repository.AddAsync(term);

        return _mapper.Map<TermDto>(term);
    }
}