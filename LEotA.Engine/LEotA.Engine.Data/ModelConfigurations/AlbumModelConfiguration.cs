using LEotA.Engine.Data.ModelConfigurations.Base;
using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LEotA.Engine.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for <see cref="AboutUs"/> entity
    /// </summary>
    public class AlbumModelConfiguration : IdentityModelConfigurationBase<Album>
    {
        protected override void AddBuilder(EntityTypeBuilder<Album> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        }

        protected override string TableName()
        {
            return "Album";
        }
    }
}