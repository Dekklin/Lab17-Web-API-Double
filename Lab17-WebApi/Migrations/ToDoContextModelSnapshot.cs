﻿// <auto-generated />
using System;
using Lab17_WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab17WebApi.Migrations
{
    [DbContext(typeof(ToDoContext))]
    partial class ToDoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab17_WebApi.Data.ToDoList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("Lab17_WebApi.ToDo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Finished");

                    b.Property<int>("ListId");

                    b.Property<string>("Title");

                    b.Property<int?>("ToDoListID");

                    b.HasKey("ID");

                    b.HasIndex("ToDoListID");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("Lab17_WebApi.ToDo", b =>
                {
                    b.HasOne("Lab17_WebApi.Data.ToDoList")
                        .WithMany("Contents")
                        .HasForeignKey("ToDoListID");
                });
#pragma warning restore 612, 618
        }
    }
}
