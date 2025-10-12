using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord
{
  public class ContactInformation
    {

        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public SaveStatusContact Savestatus { get; set; }

    }
    public enum SaveStatusContact
    {
        Save_As_Draft,
        Save
    }
}
