//=========================================
//  Copyright (c) Tarteeb LLC
//  Empowering Genuine Leadership
//=========================================

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Tarteeb.Exchanger.Models.Clients;

namespace Tarteeb.Exchanger.Brokers.Storages
{
    internal class StorageBroker : EFxceptionsContext
    {
        public DbSet<Client> Clients { get; set; }
        public StorageBroker()=>
            this.Database.EnsureCreated();

        public async Task<Client> InsertClientAsync(Client client)
        {
            await this.Clients.AddAsync(client);
            await this.SaveChangesAsync();

            return client;
        }

        public IQueryable<Client> SelectAllClients()
        {
            return this.Clients.AsQueryable();
        }

        public async Task<Client> SelectClientByIdAsync(Guid clientId) =>
            await this.Clients.FindAsync(clientId);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source = ..\\..\\..\\Tarteeb Db";
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
