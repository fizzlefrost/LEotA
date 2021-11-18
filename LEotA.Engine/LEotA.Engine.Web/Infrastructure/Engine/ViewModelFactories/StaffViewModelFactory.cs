using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.StaffViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class StaffViewModelFactory: ViewModelFactory<StaffCreateViewModel, StaffUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<StaffCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<StaffCreateViewModel>());
        }

        public override async Task<OperationResult<StaffUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<StaffUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<Staff>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<StaffUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}