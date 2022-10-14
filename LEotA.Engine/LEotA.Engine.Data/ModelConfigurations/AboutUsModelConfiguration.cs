using LEotA.Engine.Data.ModelConfigurations.Base;
using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LEotA.Engine.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for <see cref="AboutUs"/> entity
    /// </summary>
    public class AboutUsModelConfiguration : IdentityModelConfigurationBase<AboutUs>
    {
        protected override void AddBuilder(EntityTypeBuilder<AboutUs> builder)
        {
            builder.Property(x => x.Id).IsRequired();
        }

        protected override string TableName()
        {
            return "AboutUs";
        }
    }
}