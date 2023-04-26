// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// using Microsoft.Solutions.PatientHub.BatchInferenceService;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using Microsoft.Solutions.PatientHub.UtilityService;


// public class Program
// {
//     private static ExplanationService explanationService;
//     private static ColumnLookupValueService columnLookupValueService;


//     public void InitTest()
//     {
//         columnLookupValueService = new ColumnLookupValueService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "ColumnLookupValues");
//         explanationService = new ExplanationService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "Explanations");
//     }


//     public async Task Test_01_GetTop5ExplanationsTest()
//     {

//         var result = await explanationService.GetTop5Explanations(columnLookupValueService ,"28196");
//         Console.WriteLine(result.Count());
//         foreach (var item in result)
//         {
//             Console.WriteLine(JsonConvert.SerializeObject(item));
//         }


//     }
//      public static async Task Main() { 
//         Program minhaClasse = new Program();
//          minhaClasse.InitTest(); 
//          await minhaClasse.Test_01_GetTop5ExplanationsTest(); 
//          }
// }


// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Microsoft.Solutions.PatientHub.CognitiveService;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.Solutions.Test.MSTestV2;



// public class TTSServiceTests
// {
//     private static TTSService ttsService;

//     public void InitTest()
//     {
//         ttsService = new TTSService(Config["Values:TTSSubscriptionKey"], Config["Values:TTSServiceRegion"]);
//     }


//     public async Task<byte[]> GetSpeechStreamTest()
//     {
//         var result = await ttsService.GetSpeechStreamAsync("Hello John Doe, How are you?");
//        return result;
//     }

//     public static async Task Main() { 
//     TTSServiceTests minhaClasse = new TTSServiceTests();
//         minhaClasse.InitTest(); 
//         resultado = await minhaClasse.Test_01_GetTop5ExplanationsTest(); 
//         console.WriteLine(resultado);
//         }
// }



// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


// using Microsoft.Solutions.PatientHub.BatchInferenceService;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using Microsoft.Solutions.PatientHub.UtilityService;

// public class ExplanationServiceTests
// {
//     private static ExplanationService explanationService;
//     private static ColumnLookupValueService columnLookupValueService;


//     public void InitTest()
//     {
//         columnLookupValueService = new ColumnLookupValueService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "ColumnLookupValues");
//         explanationService = new ExplanationService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "Explanations");
//     }


//     public async Task Test_01_GetTop5ExplanationsTest()
//     {
//         var result = await explanationService.GetTop5Explanations(columnLookupValueService, "28196");
//         foreach (var item in result)
//         {
//             Console.WriteLine(JsonConvert.SerializeObject(item));
//         }

    
//     }

//     public static async Task Main()
//     {
//         ExplanationServiceTests minhaClasse = new ExplanationServiceTests();
//         minhaClasse.InitTest();
//         await minhaClasse.Test_01_GetTop5ExplanationsTest();
       
//     }
// }



// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.PatientHub.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using Microsoft.Solutions.PatientHub.PatientService.Models;



public class PatientServiceTests
{
    private static PatientService patientService;
    private static AdmissionService admissionTypeService;


    public void InitTest()
    {
        patientService = new PatientService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "Patient");
        admissionTypeService = new AdmissionService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "AdmissionType");
    }

 
    public async Task Test_01_GetAllPatients()
    {
        var result = await patientService.GetAllPatient();

        foreach (var item in result)
        {
            Console.WriteLine(JsonConvert.SerializeObject(item));
        }
    }


    public async Task Test_02_GetPatient()
    {
        var result = await patientService.GetPatient("5");
       
    }

   
    // public async Task Test_02_UpdateScore()
    // {
    //     var result = await patientService.UpdateScore("5", (decimal)0.5);
   
    // }

 
    public async Task Test_03_RegisterPatient()
    {
        BasicPatientProfile newPatient = new BasicPatientProfile() { LastName = "Teste", FirstName = "Silva", Age = 46, Gender = Gender.Male };

        var result = await patientService.RegisterPatient(newPatient);
        Console.WriteLine(JsonConvert.SerializeObject(result));
        Console.WriteLine("Registrou");

        // await patientService.DeletePatient(result.id, result.Id);

    }

       public static async Task Main()
    {
        PatientServiceTests minhaClasse = new PatientServiceTests();
        minhaClasse.InitTest();
        await minhaClasse.Test_03_RegisterPatient();
        Console.WriteLine("Registrou");
       
   }

}
