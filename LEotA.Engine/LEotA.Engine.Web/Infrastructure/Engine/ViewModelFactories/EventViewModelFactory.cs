using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.EventViewModel;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class EventViewModelFactory: ViewModelFactory<EventCreateViewModel, EventUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<EventCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<EventCreateViewModel>());
        }

        public override async Task<OperationResult<EventUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<EventUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<Event>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<EventUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}