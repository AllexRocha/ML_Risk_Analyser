// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.PatientHub.UtilityService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste3.Models
{
    public class Exam
    {
        public string weight { get; set; }
        public int admission_type_id { get; set; }
        public int discharge_disposition_id { get; set; }
        public int admission_source_id { get; set; }
        public int time_in_hospital { get; set; }
        public string payer_code { get; set; }
        public string medical_specialty { get; set; }
        public int num_lab_procedures { get; set; }
        public int num_procedures { get; set; }
        public int num_medications { get; set; }
        public int number_outpatient { get; set; }
        public int number_emergency { get; set; }
        public int number_inpatient { get; set; }
        public string diag_3 { get; set; }
    }
}

