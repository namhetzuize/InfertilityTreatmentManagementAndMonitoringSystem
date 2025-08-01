﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfertilityTreatmentSystem.DAL.Models;

public partial class MedicalRecord
{
    public Guid RecordId { get; set; }

    public Guid AppointmentId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid CustomerId { get; set; }

    public string Prescription { get; set; }

    public string TestResults { get; set; }
    [Required(ErrorMessage = "Ghi chú không được để trống.")]
    public string Note { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Appointment Appointment { get; set; }

    public virtual User Customer { get; set; }

    public virtual User Doctor { get; set; }
}