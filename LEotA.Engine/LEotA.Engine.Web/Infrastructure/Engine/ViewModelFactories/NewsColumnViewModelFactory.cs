using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsColumnViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class NewsColumnViewModelFactory: ViewModelFactory<NewsColumnCreateViewModel, NewsColumnUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsColumnViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<NewsColumnCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<NewsColumnCreateViewModel>());
        }

        public override async Task<OperationResult<NewsColumnUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<NewsColumnUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<NewsColumn>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<NewsColumnUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}