﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using datagenerator.core.repository;

#nullable disable

namespace datagenerator.core.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("datagenerator.core.models.Item", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Идентификатор");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Признак удаленного предмета");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasComment("Имя предмета");

                    b.Property<Guid>("TemplateID")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Ссылка на шаблон");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TemplateID");

                    b.ToTable("ITEMS");
                });

            modelBuilder.Entity("datagenerator.core.models.Template", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Идентификатор");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Признак удаленного шаблона");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasComment("Имя шаблона");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TEMPLATES");
                });

            modelBuilder.Entity("datagenerator.core.models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Идентификатор");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Признак удаленного пользователя");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasComment("Псевдоним пользователя");

                    b.HasKey("ID");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("datagenerator.core.models.UserData", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Идентификатор");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Хешированный пароль");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Ссылка на пользователя");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("USERDATA");
                });

            modelBuilder.Entity("datagenerator.core.models.Item", b =>
                {
                    b.HasOne("datagenerator.core.models.Template", "Template")
                        .WithMany("Items")
                        .HasForeignKey("TemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("datagenerator.core.models.UserData", b =>
                {
                    b.HasOne("datagenerator.core.models.User", "User")
                        .WithOne("UserData")
                        .HasForeignKey("datagenerator.core.models.UserData", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("datagenerator.core.models.Template", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("datagenerator.core.models.User", b =>
                {
                    b.Navigation("UserData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
