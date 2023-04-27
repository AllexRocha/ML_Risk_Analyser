using teste3.Models;
using Newtonsoft.Json;
string json = @"{'weight':'100.0','admission_type_id':'2','discharge_disposition_id':'1','admission_source_id':'8','time_in_hospital':'2','payer_code':'CP','medical_specialty':'?','num_lab_procedures':'20','num_procedures':'1','num_medications':'8','number_outpatient':'2','number_emergency':'1','number_inpatient':'0','diag_1':'584','diag_2':'276','diag_3':'250.52'}";



Exam exam_data = JsonConvert.DeserializeObject<Exam>(json);


// var paitnet = await GetPatient(PatientID);
// if (paitnet is null) return null;


// paitnet.DMPRW30Days_Score = Score;
// Console.WriteLine($"Patient {PatientID} has been updated.");
// return await this.EntityCollection.SaveAsync(paitnet);