using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.AboutUsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class AboutUsViewModelFactory: ViewModelFactory<AboutUsCreateViewModel, AboutUsUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AboutUsViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<AboutUsCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<AboutUsCreateViewModel>());
        }

        public override async Task<OperationResult<AboutUsUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<AboutUsUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<AboutUs>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<AboutUsUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}