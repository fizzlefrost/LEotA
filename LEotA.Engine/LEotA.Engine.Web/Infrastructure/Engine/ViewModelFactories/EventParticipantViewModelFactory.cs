using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.EventParticipantViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class EventParticipantViewModelFactory: ViewModelFactory<EventParticipantCreateViewModel, EventParticipantUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventParticipantViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<EventParticipantCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<EventParticipantCreateViewModel>());
        }

        public override async Task<OperationResult<EventParticipantUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<EventParticipantUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<EventParticipant>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<EventParticipantUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}