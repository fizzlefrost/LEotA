using LEotA.Engine.Data.ModelConfigurations.Base;
using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LEotA.Engine.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for <see cref="FileContent"/> entity
    /// </summary>
    public class FileContentModelConfiguration : IdentityModelConfigurationBase<FileContent>
    {
        protected override void AddBuilder(EntityTypeBuilder<FileContent> builder)
        {
            builder.Property(x => x.Id).IsRequired();
        }

        protected override string TableName()
        {
            return "FileContent";
        }
    }
}