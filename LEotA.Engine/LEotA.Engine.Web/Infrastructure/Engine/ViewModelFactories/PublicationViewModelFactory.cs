using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.PublicationViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class PublicationViewModelFactory: ViewModelFactory<PublicationCreateViewModel, PublicationUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublicationViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<PublicationCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<PublicationCreateViewModel>());
        }

        public override async Task<OperationResult<PublicationUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<PublicationUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<Publication>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<PublicationUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}