using Microsoft.EntityFrameworkCore;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SVC.Domain.Entities;
using Undersoft.SVC.Domain.Entities.Catalogs;
using Undersoft.SVC.Domain.Entities.Inventory;
using Undersoft.SVC.Domain.Entities.Vaccination;
using Undersoft.SVC.Service.Infrastructure.Stores.Mappings;

namespace Undersoft.SVC.Service.Infrastructure.Stores
{
    /// <summary>
    /// The store base.
    /// </summary>
    /// <typeparam name="TStore"/>
    /// <typeparam name="TContext"/>
    public class StoreBase<TStore, TContext> : DbStore<TStore, TContext>
        where TStore : IDataServerStore
        where TContext : DbContext
    {
        public StoreBase(DbContextOptions<TContext> options) : base(options) { }

        public virtual DbSet<Appointment>? Appointments { get; set; }
        public virtual DbSet<Campaign>? Campaigns { get; set; }
        public virtual DbSet<Certificate>? Certificates { get; set; }
        public virtual DbSet<Cost>? Costs { get; set; }
        public virtual DbSet<Manufacturer>? Manufacturers { get; set; }
        public virtual DbSet<Office>? Offices { get; set; }
        public virtual DbSet<Payment>? Payments { get; set; }
        public virtual DbSet<Personal>? Personals { get; set; }
        public virtual DbSet<PostSymptom>? PostSymptoms { get; set; }
        public virtual DbSet<Price>? Prices { get; set; }
        public virtual DbSet<Procedure>? Procedures { get; set; }
        public virtual DbSet<Request>? Requests { get; set; }
        public virtual DbSet<Safety>? Safety { get; set; }
        public virtual DbSet<Schedule>? Schedules { get; set; }
        public virtual DbSet<Specification>? Specifications { get; set; }
        public virtual DbSet<Stock>? Stocks { get; set; }
        public virtual DbSet<Term>? Terms { get; set; }
        public virtual DbSet<Traffic>? Traffics { get; set; }
        public virtual DbSet<Supplier>? Suppliers { get; set; }
        public virtual DbSet<Organization>? Organizations { get; set; }
        public virtual DbSet<Address>? Addresses { get; set; }
        public virtual DbSet<Professional>? Professionals { get; set; }
        public virtual DbSet<Vaccine>? Vaccines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyMapping(new AppointmentMappings());
            modelBuilder.ApplyMapping(new CampaignMappings());
            modelBuilder.ApplyMapping(new CertificateMappings());
            modelBuilder.ApplyMapping(new CostMappings());
            modelBuilder.ApplyMapping(new ManufacturerMappings());
            modelBuilder.ApplyMapping(new OfficeMappings());
            modelBuilder.ApplyMapping(new PaymentMappings());
            modelBuilder.ApplyMapping(new PersonalMappings());
            modelBuilder.ApplyMapping(new PostSymptomMappings());
            modelBuilder.ApplyMapping(new PriceMappings());
            modelBuilder.ApplyMapping(new ProcedureMappings());
            modelBuilder.ApplyMapping(new RequestMappings());
            modelBuilder.ApplyMapping(new SafetyMappings());
            modelBuilder.ApplyMapping(new ScheduleMappings());
            modelBuilder.ApplyMapping(new SpecificationMappings());
            modelBuilder.ApplyMapping(new StockMappings());
            modelBuilder.ApplyMapping(new TermMappings());
            modelBuilder.ApplyMapping(new TrafficMappings());
            modelBuilder.ApplyMapping(new SupplierMappings());
            modelBuilder.ApplyMapping(new AddressMappings());
            modelBuilder.ApplyMapping(new OrganizationMappings());
            modelBuilder.ApplyMapping(new ProfessionalMappings());
            modelBuilder.ApplyMapping(new VaccineMappings());

            base.OnModelCreating(modelBuilder);
        }
    }
}