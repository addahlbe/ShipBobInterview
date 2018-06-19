﻿// <auto-generated />
using AspCoreVue.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AspCoreVue.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180619032433_initial_create")]
    partial class initial_create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspCoreVue.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("CellPhone");

                    b.Property<string>("City");

                    b.Property<DateTime?>("RemovedDate");

                    b.Property<DateTime>("SavedDate");

                    b.Property<int>("SavedUserId");

                    b.Property<string>("StateCode");

                    b.Property<string>("ZipCode");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("AspCoreVue.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleInitial");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<DateTime?>("RemovedDate");

                    b.Property<DateTime>("SavedDate");

                    b.Property<int>("SavedUserId");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AspCoreVue.Entities.UserAddress", b =>
                {
                    b.Property<int>("UserAddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("UserId");

                    b.HasKey("UserAddressId");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("AspCoreVue.Entities.UserOrder", b =>
                {
                    b.Property<int>("UserOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<DateTime?>("RemovedDate");

                    b.Property<DateTime>("SavedDate");

                    b.Property<int>("SavedUserId");

                    b.Property<string>("TrackingId");

                    b.Property<int?>("UserAddressId");

                    b.Property<int>("UserId");

                    b.HasKey("UserOrderId");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserAddressId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrder");
                });

            modelBuilder.Entity("AspCoreVue.Entities.UserAddress", b =>
                {
                    b.HasOne("AspCoreVue.Entities.Address")
                        .WithMany("UserAddress")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspCoreVue.Entities.User", "User")
                        .WithMany("UserAddress")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspCoreVue.Entities.UserOrder", b =>
                {
                    b.HasOne("AspCoreVue.Entities.Address", "Address")
                        .WithMany("UserOrder")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspCoreVue.Entities.UserAddress")
                        .WithMany("UserOrder")
                        .HasForeignKey("UserAddressId");

                    b.HasOne("AspCoreVue.Entities.User", "User")
                        .WithMany("UserOrder")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
