using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PharmacySysyem.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PharmacySysyem.DAL
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<expence> expences { get; set; }
        public DbSet<ItemCardMaster> ItemCardMasters { get; set; }
        public DbSet<ItemCardDetalis> ItemCardDetaliss { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Stockmodel> Stockmodels { get; set; }
        public DbSet<OpenningBalanceM> OpBaM { get; set; }
        public DbSet<OpenningBalanceD> OpBaD { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PurchaseOrderM> PurOrM { get; set; }
        public DbSet<PurchaseOrderD> PurOrD { get; set; }
        public DbSet<ReceiptPurchaseM> RecPurM { get; set; }
        public DbSet<ReceiptPurchaseD> RecPurD { get; set; }
        public DbSet<PurchaseInvoiceM> PurInvMs { get; set; }
        public DbSet<PurchaseInvoiceD> PurInvDs { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}