﻿using System;
using System.Collections.Generic;
using HR_Medical_Records_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_Medical_Records_Management_System.Context;

//This class is used to configure the database context
public partial class HRMedicalRecordsContext : DbContext
{
    //This constructor is used to create a new instance of the context
    public HRMedicalRecordsContext()
    {
    }

    //This constructor is used to create a new instance of the context with options
    public HRMedicalRecordsContext(DbContextOptions<HRMedicalRecordsContext> options)
        : base(options)
    {
    }

    //all this properties are used to create the tables in the database
    public virtual DbSet<MedicalRecordType> MedicalRecordTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TMedicalRecord> TMedicalRecords { get; set; }

    //This method is used to configure the database context with fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicalRecordType>(entity =>
        {
            entity.HasKey(e => e.MedicalRecordTypeId).HasName("medical_record_type_pkey");

            entity.ToTable("medical_record_type");

            entity.Property(e => e.MedicalRecordTypeId).HasColumnName("medical_record_type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("status_pkey");

            entity.ToTable("status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TMedicalRecord>(entity =>
        {
            entity.HasKey(e => e.MedicalRecordId).HasName("t_medical_record_pkey");

            entity.ToTable("t_medical_record");

            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.AreaChange)
                .HasMaxLength(2)
                .HasColumnName("area_change");
            entity.Property(e => e.Audiometry)
                .HasMaxLength(2)
                .HasColumnName("audiometry");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(2000)
                .HasColumnName("created_by");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(2000)
                .HasColumnName("deleted_by");
            entity.Property(e => e.DeletionDate).HasColumnName("deletion_date");
            entity.Property(e => e.DeletionReason)
                .HasMaxLength(2000)
                .HasColumnName("deletion_reason");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(100)
                .HasColumnName("diagnosis");
            entity.Property(e => e.Disability)
                .HasMaxLength(2)
                .HasColumnName("disability");
            entity.Property(e => e.DisabilityPercentage)
                .HasPrecision(10)
                .HasColumnName("disability_percentage");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ExecuteExtra)
                .HasMaxLength(2)
                .HasColumnName("execute_extra");
            entity.Property(e => e.ExecuteMicros)
                .HasMaxLength(2)
                .HasColumnName("execute_micros");
            entity.Property(e => e.FatherData)
                .HasMaxLength(2000)
                .HasColumnName("father_data");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.MedicalBoard)
                .HasMaxLength(200)
                .HasColumnName("medical_board");
            entity.Property(e => e.MedicalRecordTypeId).HasColumnName("medical_record_type_id");
            entity.Property(e => e.ModificationDate).HasColumnName("modification_date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(2000)
                .HasColumnName("modified_by");
            entity.Property(e => e.MotherData)
                .HasMaxLength(2000)
                .HasColumnName("mother_data");
            entity.Property(e => e.Observations)
                .HasMaxLength(2000)
                .HasColumnName("observations");
            entity.Property(e => e.OtherFamilyData)
                .HasMaxLength(2000)
                .HasColumnName("other_family_data");
            entity.Property(e => e.PositionChange)
                .HasMaxLength(2)
                .HasColumnName("position_change");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.VoiceEvaluation)
                .HasMaxLength(2)
                .HasColumnName("voice_evaluation");

            entity.HasOne(d => d.MedicalRecordType).WithMany(p => p.TMedicalRecords)
                .HasForeignKey(d => d.MedicalRecordTypeId)
                .HasConstraintName("fk_medical_record_type");

            entity.HasOne(d => d.Status).WithMany(p => p.TMedicalRecords)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status_id_record");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
