// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.PatientService.Models
{
    public class BasicUpdatePatient
    {
        public string insulin { get; set; }
        public string metformin { get; set; }
    }
}
