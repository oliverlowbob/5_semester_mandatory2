﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Skoleprotokol.Models;

#nullable disable

namespace Skoleprotokol.Data
{
    public partial class Scool_ProtocolContext : DbContext
    {
        public Scool_ProtocolContext()
        {
        }

        public Scool_ProtocolContext(DbContextOptions<Scool_ProtocolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AttendanceKey> AttendanceKeys { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Idaddress)
                    .HasName("PRIMARY");

                entity.ToTable("address");

                entity.HasIndex(e => e.SchoolIdschool, "fk_address_school_idx");

                entity.HasIndex(e => e.Idaddress, "idaddress_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idaddress).HasColumnName("idaddress");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("country")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.PostalCode).HasColumnName("postal_code");

                entity.Property(e => e.SchoolIdschool).HasColumnName("school_idschool");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("street")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.SchoolIdschoolNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.SchoolIdschool)
                    .HasConstraintName("fk_address_school");
            });

            modelBuilder.Entity<AttendanceKey>(entity =>
            {
                entity.HasKey(e => e.IdattendanceKey)
                    .HasName("PRIMARY");

                entity.ToTable("attendance_key");

                entity.HasIndex(e => new { e.LessonUserIduser, e.LessonClassIdclass }, "fk_attendance_key_lesson1_idx");

                entity.HasIndex(e => e.IdattendanceKey, "idattendance_key_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdattendanceKey)
                    .HasColumnType("varchar(10)")
                    .HasColumnName("idattendance_key")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LessonClassIdclass).HasColumnName("lesson_class_idclass");

                entity.Property(e => e.LessonUserIduser).HasColumnName("lesson_user_iduser");

                entity.Property(e => e.ValidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("valid_until")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.AttendanceKeys)
                    .HasForeignKey(d => new { d.LessonUserIduser, d.LessonClassIdclass })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_attendance_key_lesson1");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Idclass)
                    .HasName("PRIMARY");

                entity.ToTable("class");

                entity.HasIndex(e => e.CourseIdcourse, "fk_class_course1_idx");

                entity.HasIndex(e => e.Idclass, "idclass_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idclass).HasColumnName("idclass");

                entity.Property(e => e.CourseIdcourse).HasColumnName("course_idcourse");

                entity.Property(e => e.End)
                    .HasColumnType("datetime")
                    .HasColumnName("end");

                entity.Property(e => e.NumberOfClass).HasColumnName("number_of_class");

                entity.Property(e => e.Start)
                    .HasColumnType("datetime")
                    .HasColumnName("start");

                entity.HasOne(d => d.CourseIdcourseNavigation)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.CourseIdcourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_class_course1");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Idcourse)
                    .HasName("PRIMARY");

                entity.ToTable("course");

                entity.HasIndex(e => e.Idcourse, "idcourse_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idcourse).HasColumnName("idcourse");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => new { e.UserIduser, e.ClassIdclass })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("lesson");

                entity.HasIndex(e => e.ClassIdclass, "fk_user_class_class1_idx");

                entity.Property(e => e.UserIduser).HasColumnName("user_iduser");

                entity.Property(e => e.ClassIdclass).HasColumnName("class_idclass");

                entity.Property(e => e.Present).HasColumnName("present");

                entity.HasOne(d => d.ClassIdclassNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ClassIdclass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_class_class1");

                entity.HasOne(d => d.UserIduserNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.UserIduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_class_user1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Idrole)
                    .HasName("PRIMARY");

                entity.ToTable("role");

                entity.HasIndex(e => e.Idrole, "idrole_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Role1, "role_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idrole).HasColumnName("idrole");

                entity.Property(e => e.Role1)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasColumnName("role")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.Idschool)
                    .HasName("PRIMARY");

                entity.ToTable("school");

                entity.HasIndex(e => e.Idschool, "idschool_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idschool)
                    .ValueGeneratedNever()
                    .HasColumnName("idschool");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SchoolIdschool, "fk_user_school1_idx");

                entity.HasIndex(e => e.Iduser, "iduser_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("email")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("first_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("last_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("password")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SchoolIdschool).HasColumnName("school_idschool");

                entity.HasOne(d => d.SchoolIdschoolNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SchoolIdschool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_school1");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserIduser, e.RoleIdrole })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_role");

                entity.HasIndex(e => e.RoleIdrole, "fk_user_role_role1_idx");

                entity.Property(e => e.UserIduser).HasColumnName("user_iduser");

                entity.Property(e => e.RoleIdrole).HasColumnName("role_idrole");

                entity.HasOne(d => d.RoleIdroleNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleIdrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_role_role1");

                entity.HasOne(d => d.UserIduserNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserIduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_role_user1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string mySqlConnectionStr = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseLazyLoadingProxies().UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
            optionsBuilder.ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }

    }
}