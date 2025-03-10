﻿using LEotA.Engine.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LEotA.Engine.Data
{
    /// <summary>
    /// Abstraction for Database (EntityFramework)
    /// </summary>
    public interface IApplicationDbContext
    {
        #region System

        DbSet<ApplicationUser> Users { get; set; }
        DbSet<ApplicationUserProfile> Profiles { get; set; }
        DbSet<MicroservicePermission> Permissions { get; set; }
        DbSet<News> News { get; set; }
        DbSet<FileContent> FileContents { get; set; }
        DbSet<AboutUs> AboutUs { get; set; }
        DbSet<Album> Albums { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventParticipant> EventParticipants { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Publication> Publications { get; set; }
        DbSet<Staff> Staff { get; set; }
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        
        #endregion
    }
}