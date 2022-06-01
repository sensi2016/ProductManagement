using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using ProductManagement.Entities;
using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DatabaseFacade Databases => this.Database;

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        #region db set

        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");


                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);


                entity.Property(e => e.FatherName).HasMaxLength(50);


                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .HasMaxLength(203)
                    .HasComputedColumnSql("(FirstName + ' ' + LastName)", false);

                entity.Property(e => e.Job).HasMaxLength(50);


                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.NationalId).HasMaxLength(50);

                entity.Property(e => e.Passport).HasMaxLength(50);

                entity.Property(e => e.PersonalCode).HasMaxLength(50);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Person_Gender");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_User_Person");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_User");
            });

            //var sha1 = new SHA1CryptoServiceProvider();
            //var sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes("871107077"));
            // var hash = Encryption.EnryptString("871107077");

            //var hashedPassword = ASCIIEncoding.GetString(md5data);

            #region seed

            modelBuilder.Entity<Gender>().HasData(
                            new Gender
                            {
                                Id = 1,
                                Title = "Man",
                                IsActive = true,
                            },
                              new Gender
                              {
                                  Id = 2,
                                  Title = "Women",
                                  IsActive = true,
                              }
                            );

            modelBuilder.Entity<ProductType>().HasData(
                        new ProductType
                        {
                            Id = 1,
                            Title = "Car",
                            IsActive = true,
                        },
                          new ProductType
                          {
                              Id = 2,
                              Title = "Motorcycle",
                              IsActive = true,
                          },
                            new ProductType
                            {
                                Id = 3,
                                Title = "Bicycle",
                                IsActive = true,
                            }
                        );

            modelBuilder.Entity<Role>().HasData(
                        new Role
                        {
                            Id = 1,
                            Title = "Admin",
                            IsActive = true,
                        }
                        );
            modelBuilder.Entity<Person>().HasData(
                       new Person
                       {
                           Id = 1,
                           FirstName = "ali",
                           LastName = "jafari",
                           GenderId = 1,
                           IsForeignNational = false,

                       }
                       );

            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        UserName = "admin",
                        Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                        CreateDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        IsDelete = false,
                        IsVerify = true,
                        IsActive = true,
                        PersonId = 1
                    });

            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole
                    {
                        Id = 1,
                        UserId = 1,
                        RoleId = 1
                    });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Benz",
                    ProductTypeId = 1,
                    Count = 10,
                    Price = 1000,
                    CreateDate = DateTime.Now,
                    ModifyDate=DateTime.Now,
                    IsActive=true
                });


            #endregion
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            int result = 0;
            try
            {

                //var entities = from e in ChangeTracker.Entries()
                //               where e.State == EntityState.Added
                //                   || e.State == EntityState.Modified || e.State==EntityState.Deleted
                //               select e.Entity;
                //foreach (var entity in entities)
                //{
                //    var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(entity);
                //    System.ComponentModel.DataAnnotations.Validator.ValidateObject(entity, validationContext);
                //}
                result = await base.SaveChangesAsync(cancellationToken);

            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            // validateEntities();
            // setShadowProperties();
            //this.ApplyCorrectYeKe();
        }
    }
    public static class MyExtensions
    {
        public static IQueryable<object> Set(this IDataBaseContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethods().Where(t => t.Name == "Set").FirstOrDefault().MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}