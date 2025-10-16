﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
   public interface IPaymentServices
    {
        Task<Invoice?> InvoiceAsync(Guid studentId, string courseCode, double payment);
        Task<Invoice?> ProvisionAsync(Guid studentId, string courseCode);
    }
}
