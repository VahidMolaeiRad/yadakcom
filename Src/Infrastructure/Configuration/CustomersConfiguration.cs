using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class CustomersConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(s => s.Id);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(255);
            builder.Property(p => p.BankAccountNumber).IsRequired().HasMaxLength(16);
        }
    }
}
