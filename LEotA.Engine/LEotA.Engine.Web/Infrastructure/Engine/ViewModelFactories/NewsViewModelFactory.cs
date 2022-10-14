using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class NewsViewModelFactory: ViewModelFactory<NewsCreateViewModel, NewsUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<NewsCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<NewsCreateViewModel>());
        }

        public override async Task<OperationResult<NewsUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<NewsUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<News>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<NewsUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}