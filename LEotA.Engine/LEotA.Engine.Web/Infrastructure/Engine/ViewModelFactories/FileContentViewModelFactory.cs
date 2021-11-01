using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.FileContentViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class FileContentViewModelFactory: ViewModelFactory<FileContentCreateViewModel, FileContentUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileContentViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<FileContentCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<FileContentCreateViewModel>());
        }

        public override async Task<OperationResult<FileContentUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<FileContentUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<FileContent>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<FileContentUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}