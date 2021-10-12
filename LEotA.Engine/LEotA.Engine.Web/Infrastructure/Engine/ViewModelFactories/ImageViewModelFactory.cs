using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.ImageViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class ImageViewModelFactory: ViewModelFactory<ImageCreateViewModel, ImageUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<ImageCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<ImageCreateViewModel>());
        }

        public override async Task<OperationResult<ImageUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<ImageUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<Image>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<ImageUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}