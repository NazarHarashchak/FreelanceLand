﻿// <auto-generated />
using System;
using FreelanceLand.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190322083546_TaskChanges")]
    partial class TaskChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FreelanceLand.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateAndTime");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FreelanceLand.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateAndTime");

                    b.Property<int?>("GetterUserId");

                    b.Property<int?>("SenderUserId");

                    b.HasKey("Id");

                    b.HasIndex("GetterUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FreelanceLand.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerUserId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("ExecutorUserId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserId");

                    b.HasIndex("ExecutorUserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("FreelanceLand.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<int?>("ExecutorId");

                    b.Property<int>("Price");

                    b.Property<int?>("TaskCategoryId");

                    b.Property<int?>("TaskStatusId");

                    b.Property<string>("Title");

                    b.Property<int?>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("TaskCategoryId");

                    b.HasIndex("TaskStatusId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("FreelanceLand.Models.TaskCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("TaskCategories");
                });

            modelBuilder.Entity("FreelanceLand.Models.TaskHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int?>("FinalTaskStatusId");

                    b.Property<int?>("StartTaskStatusId");

                    b.Property<int?>("TaskCustomerId");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("UpdatedByUserId");

                    b.HasKey("Id");

                    b.HasIndex("FinalTaskStatusId");

                    b.HasIndex("StartTaskStatusId");

                    b.HasIndex("TaskCustomerId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("TaskHistories");
                });

            modelBuilder.Entity("FreelanceLand.Models.TaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("TaskStatuses");
                });

            modelBuilder.Entity("FreelanceLand.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birth_Date");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone_Number");

                    b.Property<string>("Sur_Name");

                    b.Property<int?>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FreelanceLand.Models.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("FreelanceLand.Models.Comment", b =>
                {
                    b.HasOne("FreelanceLand.Models.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId");

                    b.HasOne("FreelanceLand.Models.User", "User")
                        .WithMany("UserComments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FreelanceLand.Models.Message", b =>
                {
                    b.HasOne("FreelanceLand.Models.User", "GetterUser")
                        .WithMany("UserMessages")
                        .HasForeignKey("GetterUserId");

                    b.HasOne("FreelanceLand.Models.User", "SenderUser")
                        .WithMany()
                        .HasForeignKey("SenderUserId");
                });

            modelBuilder.Entity("FreelanceLand.Models.Review", b =>
                {
                    b.HasOne("FreelanceLand.Models.User", "CustomerUser")
                        .WithMany("UserReviews")
                        .HasForeignKey("CustomerUserId");

                    b.HasOne("FreelanceLand.Models.User", "ExecutorUser")
                        .WithMany()
                        .HasForeignKey("ExecutorUserId");
                });

            modelBuilder.Entity("FreelanceLand.Models.Task", b =>
                {
                    b.HasOne("FreelanceLand.Models.User", "Customer")
                        .WithMany("CustomerTasks")
                        .HasForeignKey("CustomerId");

                    b.HasOne("FreelanceLand.Models.User", "Executor")
                        .WithMany("UserTasks")
                        .HasForeignKey("ExecutorId");

                    b.HasOne("FreelanceLand.Models.TaskCategory", "TaskCategory")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskCategoryId");

                    b.HasOne("FreelanceLand.Models.TaskStatus", "TaskStatus")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskStatusId");

                    b.HasOne("FreelanceLand.Models.User", "UpdatedBy")
                        .WithMany("UpdateTasks")
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("FreelanceLand.Models.TaskHistory", b =>
                {
                    b.HasOne("FreelanceLand.Models.TaskStatus", "FinalTaskStatus")
                        .WithMany("FinalTaskHistories")
                        .HasForeignKey("FinalTaskStatusId");

                    b.HasOne("FreelanceLand.Models.TaskStatus", "StartTaskStatus")
                        .WithMany("StarTaskHistories")
                        .HasForeignKey("StartTaskStatusId");

                    b.HasOne("FreelanceLand.Models.User", "TaskCustomer")
                        .WithMany("UserHistories")
                        .HasForeignKey("TaskCustomerId");

                    b.HasOne("FreelanceLand.Models.Task", "Task")
                        .WithMany("TaskHistories")
                        .HasForeignKey("TaskId");

                    b.HasOne("FreelanceLand.Models.User", "UpdatedByUser")
                        .WithMany("UpdatedTaskHistories")
                        .HasForeignKey("UpdatedByUserId");
                });

            modelBuilder.Entity("FreelanceLand.Models.User", b =>
                {
                    b.HasOne("FreelanceLand.Models.UserRoles", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
