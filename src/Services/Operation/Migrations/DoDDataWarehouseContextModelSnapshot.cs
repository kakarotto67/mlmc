﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Operation.Database;

namespace Operation.Migrations
{
    [DbContext(typeof(DoDDataWarehouseContext))]
    partial class DoDDataWarehouseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Operation.Models.Missile", b =>
                {
                    b.Property<long>("MissileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("InServiceDateEnd");

                    b.Property<DateTime>("InServiceDateStart");

                    b.Property<string>("Name");

                    b.Property<Guid>("ServiceIdentityNumber");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("MissileId");

                    b.ToTable("Missiles");
                });
#pragma warning restore 612, 618
        }
    }
}
