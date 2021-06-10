using Microsoft.EntityFrameworkCore;
using System;

namespace TransactionsModule.Models
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        public DbSet<Counterparties> Counterparties { get; set; }
        public DbSet<Ref_Payment_Methods> Ref_Payment_Methods { get; set; }
        public DbSet<Ref_Transaction_Status> Ref_Transaction_Status { get; set; }
        public DbSet<Ref_Transaction_Types> Ref_Transaction_Types { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Financial_Transaction> Financial_Transactions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ref_Transaction_Status>()
                .HasData(
                    new Ref_Transaction_Status { Trans_Status_Code = 1, Trans_Status_Description = Trans_Status_Description.Completed },
                    new Ref_Transaction_Status { Trans_Status_Code = 2, Trans_Status_Description = Trans_Status_Description.Cancelled },
                    new Ref_Transaction_Status { Trans_Status_Code = 3, Trans_Status_Description = Trans_Status_Description.Disputed }
                );
            modelBuilder.Entity<Ref_Transaction_Types>()
                .HasData(
                    new Ref_Transaction_Types { Trans_Type_Code = 1, Trans_Type_Description = Trans_Type_Description.Adjustment },
                    new Ref_Transaction_Types { Trans_Type_Code = 2, Trans_Type_Description = Trans_Type_Description.Payment },
                    new Ref_Transaction_Types { Trans_Type_Code = 3, Trans_Type_Description = Trans_Type_Description.Refund }
                );
            modelBuilder.Entity<Ref_Payment_Methods>()
                .HasData(
                    new Ref_Payment_Methods { Payment_Method_Code = 1, Payment_Method_Name = Payment_Method_Name.Amex },
                    new Ref_Payment_Methods { Payment_Method_Code = 2, Payment_Method_Name = Payment_Method_Name.Bank_Transfer },
                    new Ref_Payment_Methods { Payment_Method_Code = 3, Payment_Method_Name = Payment_Method_Name.Cash },
                    new Ref_Payment_Methods { Payment_Method_Code = 4, Payment_Method_Name = Payment_Method_Name.Diners_Club },
                    new Ref_Payment_Methods { Payment_Method_Code = 5, Payment_Method_Name = Payment_Method_Name.MasterCard },
                    new Ref_Payment_Methods { Payment_Method_Code = 6, Payment_Method_Name = Payment_Method_Name.Visa }
                );
            modelBuilder.Entity<Counterparties>()
                .HasData(
                    new Counterparties { Counterparty_ID = 1, Other_Details = "Other Details 1" },
                    new Counterparties { Counterparty_ID = 2, Other_Details = "Other Details 2" },
                    new Counterparties { Counterparty_ID = 3, Other_Details = "Other Details 3" }
                );
            modelBuilder.Entity<Services>()
                .HasData(
                    new Services { Service_ID = 1, Date_Service_Provided = DateTime.Now, Other_Details = "Other Details 1" },
                    new Services { Service_ID = 2, Date_Service_Provided = DateTime.Now, Other_Details = "Other Details 1" },
                    new Services { Service_ID = 3, Date_Service_Provided = DateTime.Now, Other_Details = "Other Details 1" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
