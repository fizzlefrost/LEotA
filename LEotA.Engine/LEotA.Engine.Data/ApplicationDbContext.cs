using LEotA.Engine.Data.Base;
using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore;

namespace LEotA.Engine.Data
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #region System

        public DbSet<Log> Logs { get; set; }

        public DbSet<ApplicationUserProfile> Profiles { get; set; }

        /// <inheritdoc />
        public DbSet<MicroservicePermission> Permissions { get; set; }

        public DbSet<News> News { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<FileContent> FileContents { get; set; }
        

        #endregion
    }
}