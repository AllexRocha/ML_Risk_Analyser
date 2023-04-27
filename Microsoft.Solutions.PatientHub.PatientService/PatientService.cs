// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Solutions.CosmosDB;
using Microsoft.Solutions.CosmosDB.SQL;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using Microsoft.Solutions.PatientHub.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Newtonsoft.Json;

namespace Microsoft.Solutions.PatientHub.PatientService
{
    public class PatientService : SQLEntityCollectionBase<Patient>
    {
        private AdmissionSourceService admissionSourceService;
        private AdmissionService admissionTypeService;
        private DischargeDispositionService dischargeDispositionService;
        private ICD9CodeService IDC9CodeService;

        public PatientService(string DataConnectionString, string CollectionName, string ContainerName = "") : base(DataConnectionString, CollectionName, ContainerName)
        {
            admissionTypeService = new AdmissionService(DataConnectionString, CollectionName, "AdmissionType");
            admissionSourceService = new AdmissionSourceService(DataConnectionString, CollectionName, "AdmissionSource");
            dischargeDispositionService = new DischargeDispositionService(DataConnectionString, CollectionName, "DischargeDisposition");
            IDC9CodeService = new ICD9CodeService(DataConnectionString, CollectionName, "ICD9Code");

        }

        async public Task<IEnumerable<Patient>> GetAllPatient()
        {
            return await this.EntityCollection.GetAllAsync();
        }

        async public Task<Patient> GetPatient(string PatientID) 
        {
           
            var patient = await this.EntityCollection.FindAsync(new GenericSpecification<Patient>(x => x.Id.Equals(PatientID)));
            
            if (patient is null)
            {
                Console.WriteLine($"=============>>  Not found patient - {PatientID}");
                return null;
            }

            patient.admissionSource = admissionSourceService.GetAdmissionSource(patient.admission_source_id);
            patient.admission_type = admissionTypeService.GetAdmissionType(patient.admission_type_id);
            //for UI         
            var admissionType = admissionTypeService.GetAdmissionType(patient.discharge_disposition_id);
            patient.dischargeDisposition = new DischargeDisposition() {Id = patient.discharge_disposition_id };
            patient.dischargeDisposition.Description =((admissionType is null) || (admissionType.Description is null)) ? "-" : admissionType.Description ;

            patient.diag_1_Description = IDC9CodeService.GetDescription(patient.diag_1);
            patient.diag_2_Description = IDC9CodeService.GetDescription(patient.diag_2);
            patient.diag_3_Description = IDC9CodeService.GetDescription(patient.diag_3);
          
            return patient;
        }


        async public Task<Patient> UpdateScore(string PatientID, decimal Score)
        {
            var paitnet = await GetPatient(PatientID);
            if (paitnet is null) return null;

            
            paitnet.DMPRW30Days_Score = Score;
            Console.WriteLine($"Patient {PatientID} has been updated.");
            return await this.EntityCollection.SaveAsync(paitnet);
        }

        async public Task<Patient> AddNewPatient(Patient Patient)
        {
            return await this.EntityCollection.AddAsync(Patient);
        }

        async public Task<Patient> RegisterPatient(BasicPatientProfile Patient)
        {
            return await this.AddNewPatient(Patient.GenerateNewPatient());
        }

        async public Task DeletePatient(string EntityId, string PartitonKeyValue)
        {
            await this.EntityCollection.DeleteAsync(EntityId, PartitonKeyValue);
        }

        async public Task<Patient> UpdatePatient(Patient Patient)
        {
            return await this.EntityCollection.SaveAsync(Patient);
        }

        async public Task<Patient> BasicUpdate(string PatientId, BasicUpdatePatient medicamentos)
        {
            var varPatient = await GetPatient(PatientId);
            if (varPatient is null) return null;

            varPatient.insulin = medicamentos.insulin;
            varPatient.metformin = medicamentos.metformin;

            return await this.UpdatePatient(varPatient);
        }


        async public Task<string> GetExam(string PatientId)
        {
 
            string endpoint = "https://formsteste.cognitiveservices.azure.com/";
            string key = "6b4446031b3f47d5ae062269b7632ccb";

            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            // sample document
            Uri fileUri = new Uri($"https://raw.githubusercontent.com/AllexRocha/ML_Risk_Analyser/master/Forms_Recognizer/exames/{PatientId}.pdf");

            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-document", fileUri);

            AnalyzeResult result = operation.Value;

            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (DocumentKeyValuePair kvp in result.KeyValuePairs)
            {
                dict.Add(kvp.Key.Content, kvp.Value.Content);
            }

            String json = JsonConvert.SerializeObject(dict, new JsonSerializerSettings { Formatting = Formatting.None });
            
            return json;
        }

        async public Task<Patient> UpdateExam(string PatientID)
        {

            var patient = await GetPatient(PatientID);
            if (patient is null) return null;
 
            string  json = await this.GetExam(PatientID);
           
        
            var exam_data = JsonConvert.DeserializeObject<Dictionary<string,object>>(json);


            string jsonpaciente = JsonConvert.SerializeObject(patient);
            var patient_data = JsonConvert.DeserializeObject<Dictionary<string,object>>(jsonpaciente);
            foreach (KeyValuePair<string, object> exam in exam_data)
            {
                if(patient_data.ContainsKey(exam.Key)){
                    patient_data[exam.Key] = exam.Value;
                }
                
            }
            String patientData_string = JsonConvert.SerializeObject(patient_data, new JsonSerializerSettings { Formatting = Formatting.None });
            Patient patientData = JsonConvert.DeserializeObject<Patient>(patientData_string);
            Console.WriteLine($"Patient {PatientID} has been updated.");
            return await this.EntityCollection.SaveAsync(patientData);
            
        }
    }
}
