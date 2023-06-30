﻿using Microsoft.EntityFrameworkCore;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Corp.ERP.Inventory.Persistence;

public class InventoryContext : DbContext
{
    private readonly InventoryDbConfiguration _configuration;

    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<User> Users { get; set; }

    public InventoryContext(InventoryDbConfiguration configuration)
    {
        _configuration = configuration;
        if (_configuration is not null && (_configuration.EnsureCreated | _configuration.EnsureDeleted))
        {
            if (_configuration.EnsureDeleted)
                Database.EnsureDeleted();
            if(_configuration.EnsureCreated)
                Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_configuration.UseInMemoryDatabase)
        {
            optionsBuilder
                .UseInMemoryDatabase(databaseName: _configuration.InMemoryDatabaseName)
                .LogTo(Console.WriteLine, LogLevel.Information)
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                ;
        }
        else
        {
            var connectionString = _configuration.ConnectionString;

            optionsBuilder
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                ;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
