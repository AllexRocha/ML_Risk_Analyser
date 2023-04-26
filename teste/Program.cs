using Microsoft.Solutions.PatientHub.RealtimeInferenceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Solutions.PatientHub.RealtimeInferenceService.Model;
using Microsoft.Solutions.PatientHub.UtilityService;

public class Program
{       
        static RealtimeInference realtimeInferenceService;
        static ColumnNameMapService columnNameMapService;
        static ColumnLookupValueService columnLookupValueService;

        public async Task<string> GetTop5RealtimeInferenceTest()
        {

         Program.realtimeInferenceService =
                new RealtimeInference("http://20.85.155.207:80/api/v1/service/diabetes-readmission-service-aks/score", "wik1hZk8fyztGBHa6YdHloYOcOizWz8E");
            Program.columnNameMapService =
                new ColumnNameMapService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "ColumnNameMap");
            Program.columnLookupValueService =
          new ColumnLookupValueService("AccountEndpoint=https://cdb-accelerator-001.documents.azure.com:443/;AccountKey=Mo7pobTO5CvFN56pLuRSmqdWZfiztdmZfApx6nn9fLQt5gBVzfqZGqjYU0IMZF2fugZzmMag7wjmACDbBvji2w==;", "PatientData", "ColumnLookupValues");

        string jsonString =  @"{
                                'race': 'Caucasian',
                                'gender': 'Male',
                                'age': '[70-80)',
                                'weight': '?',
                                'admission_type_id': 1,
                                'discharge_disposition_id': 24,
                                'admission_source_id': 7,
                                'time_in_hospital': 1,
                                'payer_code': 'MD',
                                'medical_specialty': '?',
                                'num_lab_procedures': 51,
                                'num_procedures': 0,
                                'num_medications': 19,
                                'number_outpatient': 0,
                                'number_emergency': 0,
                                'number_inpatient': 3,
                                'diag_1': '491',
                                'diag_2': '428',
                                'diag_3': '428',
                                'number_diagnoses': 9,
                                'max_glu_serum': 'None',
                                'A1Cresult': 'None',
                                'metformin': 'No',
                                'repaglinide': 'No',
                                'nateglinide': 'No',
                                'chlorpropamide': 'No',
                                'glimepiride': 'No',
                                'acetohexamide': 'No',
                                'glipizide': 'Steady',
                                'glyburide': 'No',
                                'tolbutamide': 'No',
                                'pioglitazone': 'Steady',
                                'rosiglitazone': 'No',
                                'acarbose': 'No',
                                'miglitol': 'No',
                                'troglitazone': 'No',
                                'tolazamide': 'No',
                                'examide': 'No',
                                'citoglipton': 'No',
                                'insulin': 2,
                                'glyburide-metformin': 'No',
                                'glipizide-metformin': 'No',
                                'glimepiride-pioglitazone': 'No',
                                'metformin-rosiglitazone': 'No',
                                'metformin-pioglitazone': 'No',
                                'change': 'Ch',
                                'diabetesMed': 'Yes',
                                'FirstName':'Tigran',
                                'LastName':'Arakelyan'
                            }"; 

            var patientData = JsonConvert.DeserializeObject<PatientData>(jsonString);

            var result = await Program.realtimeInferenceService.GetTop5RealtimeInference(columnNameMapService, columnLookupValueService, patientData);
            return JsonConvert.SerializeObject(result);

        }
    public static async Task Main()
    {
        Program minhaClasse = new Program();
        string resultado = await minhaClasse.GetTop5RealtimeInferenceTest();
        Console.WriteLine(resultado);
    }
}
