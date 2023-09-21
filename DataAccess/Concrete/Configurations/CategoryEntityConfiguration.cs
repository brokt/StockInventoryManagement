using Core.Entities.Concrete;
using Entities.Concrete;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            //builder.HasMany(c => c.Products)
            //       .WithMany(p => p.Categories)
            //       .UsingEntity(j => j.ToTable("ProductCategories"));

            builder.HasOne(c => c.ParentCategory)
                   .WithMany()
                   .HasForeignKey(c => c.ParentCategoryId);


            builder.HasData(GetCategories());
        }

        private List<Category> GetCategories()
        {
            return new List<Category>()
            {
                 new Category { Id = 1, Name = "Elektronik" },
        new Category { Id = 2, Name = "Giyim" },
        new Category { Id = 3, Name = "Ev & Yaşam" },
        new Category { Id = 4, Name = "Spor Malzemeleri" },
        new Category { Id = 5, Name = "Kozmetik" },
        new Category { Id = 6, Name = "Oyuncaklar" },
        new Category { Id = 7, Name = "Kitaplar" },
        new Category { Id = 8, Name = "Mobilya" },
        new Category { Id = 9, Name = "Saatler" },
        new Category { Id = 10, Name = "Mücevherat" },
        new Category { Id = 11, Name = "Cep Telefonları", ParentCategoryId = 1 },
        new Category { Id = 12, Name = "Bilgisayarlar", ParentCategoryId = 1 },
        new Category { Id = 13, Name = "Giyim", ParentCategoryId = 2 },
        new Category { Id = 14, Name = "Giyim", ParentCategoryId = 2 },
        new Category { Id = 15, Name = "Mutfak Eşyaları", ParentCategoryId = 3 },
        new Category { Id = 16, Name = "Mobilyalar", ParentCategoryId = 3 },
        new Category { Id = 17, Name = "Futbol Malzemeleri", ParentCategoryId = 4 },
        new Category { Id = 18, Name = "Basketbol Malzemeleri", ParentCategoryId = 4 },
        new Category { Id = 19, Name = "Makyaj Ürünleri", ParentCategoryId = 5 },
        new Category { Id = 20, Name = "Cilt Bakım Ürünleri", ParentCategoryId = 5 },
            };


       
    }
    }
}
