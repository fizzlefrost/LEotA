using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.AlbumViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.ViewModelFactories
{
    public class AlbumViewModelFactory: ViewModelFactory<AlbumCreateViewModel, AlbumUpdateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlbumViewModelFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public override Task<OperationResult<AlbumCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(new OperationResult<AlbumCreateViewModel>());
        }

        public override async Task<OperationResult<AlbumUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<AlbumUpdateViewModel>();
            var entity = await _unitOfWork.GetRepository<Album>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entity == null)
            {
                operation.AddWarning("Entity not found");
                return (operation);
            }

            var model = _mapper.Map<AlbumUpdateViewModel>(entity);
            operation.Result = model;
            return operation;
        }
    }
}