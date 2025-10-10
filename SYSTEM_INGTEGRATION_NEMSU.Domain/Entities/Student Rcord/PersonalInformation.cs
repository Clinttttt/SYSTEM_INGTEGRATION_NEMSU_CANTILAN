﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
  public  class PersonalInformation
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public GenderChoice Gender { get; set; }
        public CivilStatusChoice CivilStatus { get; set; }
        public string? Nationality { get; set; }
        public string? PermanentAddress { get; set; }
        public string? GuardianName { get; set; }
        public string? GuardianContact { get; set; }

    }
    public enum GenderChoice
    {
        Male,
        Female,
        Prefer_not_to_say
    }
    public enum CivilStatusChoice
    {
        Single,
        Taken,
        Married
    }
}
