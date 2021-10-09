using LEotA.Engine.Data;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.AccountViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Person
    /// </summary>
    public class ApplicationUserProfileMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public ApplicationUserProfileMapperConfiguration() => CreateMap<RegisterViewModel, ApplicationUserProfile>()
            .ForAllOtherMembers(x => x.Ignore());
    }
}