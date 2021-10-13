using LEotA.Engine.Data.ModelConfigurations.Base;
using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LEotA.Engine.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for <see cref="AboutUs"/> entity
    /// </summary>
    public class EventModelConfiguration : IdentityModelConfigurationBase<Event>
    {
        protected override void AddBuilder(EntityTypeBuilder<Event> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Text).HasMaxLength(10000).IsRequired();
            builder.Property(x => x.EmbedLink).HasMaxLength(5000).IsRequired();
        }

        protected override string TableName()
        {
            return "Event";
        }
    }
}