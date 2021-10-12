using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.ProjectViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class ProjectViewModelFactory: ViewModelFactory<ProjectCreateViewModel, ProjectUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<ProjectCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<ProjectCreateViewModel>());
        }

        public override async Task<OperationResult<ProjectUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<ProjectUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<Project>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<ProjectUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}