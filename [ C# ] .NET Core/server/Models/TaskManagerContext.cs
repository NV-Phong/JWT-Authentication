using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Models;

public partial class TaskManagerContext : DbContext
{
	public TaskManagerContext() { }

	public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

	public virtual DbSet<Assignment> Assignments { get; set; }

	public virtual DbSet<Condition> Conditions { get; set; }

	public virtual DbSet<ListTemplate> ListTemplates { get; set; }

	public virtual DbSet<Member> Members { get; set; }

	public virtual DbSet<Permission> Permissions { get; set; }

	public virtual DbSet<Project> Projects { get; set; }

	public virtual DbSet<Status> Statuses { get; set; }

	public virtual DbSet<Data.Task> Tasks { get; set; }

	public virtual DbSet<TaskDetail> TaskDetails { get; set; }

	public virtual DbSet<Template> Templates { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<Workflow> Workflows { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		Guid IDTemplateDefault = Guid.NewGuid();
		Guid IDTemplateITManagement = Guid.NewGuid();
		Guid IDTemplateDesignManagement = Guid.NewGuid();

		modelBuilder.Entity<Template>().HasData
		(
			new Template
			{
				IDTemplate = IDTemplateDefault,
				TemplateName = "Default",
			},
			new Template
			{
				IDTemplate = IDTemplateITManagement,
				TemplateName = "IT Management",
			},
			new Template
			{
				IDTemplate = IDTemplateDesignManagement,
				TemplateName = "Design Management",
			}
		);
		modelBuilder.Entity<ListTemplate>().HasData
		(
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDefault,
				StatusName = "To Do",
				StatusOrder = 1,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDefault,
				StatusName = "In Progress",
				StatusOrder = 2,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDefault,
				StatusName = "In Preview",
				StatusOrder = 3,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDefault,
				StatusName = "Done",
				StatusOrder = 4,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateITManagement,
				StatusName = "To Do",
				StatusOrder = 1,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateITManagement,
				StatusName = "In Progress",
				StatusOrder = 2,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateITManagement,
				StatusName = "In Preview",
				StatusOrder = 3,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateITManagement,
				StatusName = "Test",
				StatusOrder = 4,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateITManagement,
				StatusName = "Bug",
				StatusOrder = 5,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateITManagement,
				StatusName = "Done",
				StatusOrder = 6,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDesignManagement,
				StatusName = "To Do",
				StatusOrder = 1,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDesignManagement,
				StatusName = "Idea",
				StatusOrder = 2,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDesignManagement,
				StatusName = "Sketch",
				StatusOrder = 3,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDesignManagement,
				StatusName = "In Preview",
				StatusOrder = 4,
			},
			new ListTemplate
			{
				IDListTemplate = Guid.NewGuid(),
				IDTemplate = IDTemplateDesignManagement,
				StatusName = "Done",
				StatusOrder = 5,
			}
		);



		modelBuilder.Entity<Assignment>(entity =>
		{
			entity.HasKey(e => new { e.IDUser, e.IDTask });

			entity.ToTable("ASSIGNMENT");

			entity.Property(e => e.IDUser).HasColumnName("IDUser");
			entity.Property(e => e.IDTask).HasColumnName("IDTask");
			entity.Property(e => e.IDAssignment)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDAssignment");

			entity.HasOne(d => d.IDTaskNavigation).WithMany(p => p.Assignments)
					.HasForeignKey(d => d.IDTask)
					.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.IDUserNavigation).WithMany(p => p.Assignments)
					.HasForeignKey(d => d.IDUser)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Condition>(entity =>
		{
			entity.HasKey(e => e.IDCondition);

			entity.ToTable("CONDITION");

			entity.Property(e => e.IDCondition)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDCondition");
			entity.Property(e => e.IDPermission).HasColumnName("IDPermission");
			entity.Property(e => e.IDWorkflow).HasColumnName("IDWorkflow");

			entity.HasOne(d => d.IDPermissionNavigation).WithMany(p => p.Conditions)
					.HasForeignKey(d => d.IDPermission)
					.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.IDWorkflowNavigation).WithMany(p => p.Conditions)
					.HasForeignKey(d => d.IDWorkflow)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<ListTemplate>(entity =>
		{
			entity.HasKey(e => e.IDListTemplate);

			entity.ToTable("LISTTEMPLATE");

			entity.Property(e => e.IDListTemplate)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDListTemplate");
			entity.Property(e => e.IDTemplate).HasColumnName("IDTemplate");
			entity.Property(e => e.StatusName).HasMaxLength(50);

			entity.HasOne(d => d.IDTemplateNavigation).WithMany(p => p.ListTemplates)
					.HasForeignKey(d => d.IDTemplate)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Member>(entity =>
		{
			entity.HasKey(e => e.IDMember);

			entity.ToTable("MEMBER");

			entity.Property(e => e.IDMember)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDMember");
			entity.Property(e => e.IDProject).HasColumnName("IDProject");
			entity.Property(e => e.IDUser).HasColumnName("IDUser");

			entity.HasOne(d => d.IDProjectNavigation).WithMany(p => p.Members)
					.HasForeignKey(d => d.IDProject)
					.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.IDUserNavigation).WithMany(p => p.Members)
					.HasForeignKey(d => d.IDUser)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Permission>(entity =>
		{
			entity.HasKey(e => e.IDPermission);

			entity.ToTable("PERMISSION");

			entity.Property(e => e.IDPermission)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDPermission");
			entity.Property(e => e.IDProject).HasColumnName("IDProject");
			entity.Property(e => e.IDUser).HasColumnName("IDUser");

			entity.HasOne(d => d.IDProjectNavigation).WithMany(p => p.Permissions)
					.HasForeignKey(d => d.IDProject)
					.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.IDUserNavigation).WithMany(p => p.Permissions)
					.HasForeignKey(d => d.IDUser)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Project>(entity =>
		{
			entity.HasKey(e => e.IDProject);

			entity.ToTable("PROJECT");

			entity.Property(e => e.IDProject)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDProject");
			entity.Property(e => e.DayCreate).HasColumnType("datetime");
			entity.Property(e => e.IDLeader).HasColumnName("IDLeader");
			entity.Property(e => e.ProjectName).HasMaxLength(50);
		});

		modelBuilder.Entity<Status>(entity =>
		{
			entity.HasKey(e => e.IDStatus);

			entity.ToTable("STATUS");

			entity.Property(e => e.IDStatus)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDStatus");
			entity.Property(e => e.IDProject).HasColumnName("IDProject");
			entity.Property(e => e.StatusName).HasMaxLength(50);

			entity.HasOne(d => d.IDProjectNavigation).WithMany(p => p.Statuses)
					.HasForeignKey(d => d.IDProject)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Data.Task>(entity =>
		{
			entity.HasKey(e => e.IDTask);

			entity.ToTable("TASK");

			entity.Property(e => e.IDTask)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDTask");
			entity.Property(e => e.DayCreate).HasColumnType("datetime");
			entity.Property(e => e.DayStart).HasColumnType("datetime");
			entity.Property(e => e.Deadline).HasColumnType("datetime");
			entity.Property(e => e.IDProject).HasColumnName("IDProject");
			entity.Property(e => e.IDStatus).HasColumnName("IDStatus");
			entity.Property(e => e.TaskName).HasMaxLength(50);

			entity.HasOne(d => d.IDProjectNavigation).WithMany(p => p.Tasks)
					.HasForeignKey(d => d.IDProject)
					.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.IDStatusNavigation).WithMany(p => p.Tasks)
					.HasForeignKey(d => d.IDStatus)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<TaskDetail>(entity =>
		{
			entity.HasKey(e => e.IDTask);

			entity.ToTable("TASKDETAILS");

			entity.Property(e => e.IDTask)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDTask");

			entity.HasOne(d => d.IDTaskNavigation).WithOne(p => p.TaskDetail)
					.HasForeignKey<TaskDetail>(d => d.IDTask)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Template>(entity =>
		{
			entity.HasKey(e => e.IDTemplate);

			entity.ToTable("TEMPLATE");

			entity.HasIndex(e => e.TemplateName, "UNIQUE_TemplateName").IsUnique();

			entity.Property(e => e.IDTemplate)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDTemplate");
			entity.Property(e => e.TemplateName).HasMaxLength(50);
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.IDUser);

			entity.ToTable("USER");

			entity.Property(e => e.IDUser)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDUser");
			entity.Property(e => e.Avatar).IsUnicode(false);
			entity.Property(e => e.DisplayName).HasMaxLength(50);
			entity.Property(e => e.Email)
					.HasMaxLength(50)
					.IsUnicode(false);
			entity.Property(e => e.Password).IsUnicode(false);
			entity.Property(e => e.Role).HasMaxLength(50);
			entity.Property(e => e.UserName).HasMaxLength(50);
		});

		modelBuilder.Entity<Workflow>(entity =>
		{
			entity.HasKey(e => e.IDWorkflow);

			entity.ToTable("WORKFLOW");

			entity.Property(e => e.IDWorkflow)
					.HasDefaultValueSql("(newid())")
					.HasColumnName("IDWorkflow");
			entity.Property(e => e.IDStatus).HasColumnName("IDStatus");

			entity.HasOne(d => d.IDStatusNavigation).WithMany(p => p.Workflows)
					.HasForeignKey(d => d.IDStatus)
					.OnDelete(DeleteBehavior.ClientSetNull);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
