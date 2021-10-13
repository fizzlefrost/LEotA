using LEotA.Engine.Data.ModelConfigurations.Base;
using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LEotA.Engine.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for <see cref="AboutUs"/> entity
    /// </summary>
    public class EventParticipantModelConfiguration : IdentityModelConfigurationBase<EventParticipant>
    {
        protected override void AddBuilder(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.EventId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }

        protected override string TableName()
        {
            return "EventParticipant";
        }
    }
}