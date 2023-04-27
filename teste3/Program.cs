using Microsoft.Solutions.PatientHub.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using Microsoft.Solutions.PatientHub.PatientService.Models;
using Microsoft.Solutions.PatientHub.BatchInferenceService;
using Microsoft.Solutions.PatientHub.UtilityService;



public class PatientServiceTests
{
    private static PatientService patientService;
    private static AdmissionService admissionTypeService;
    private static ExplanationService explanationService;
    private static ColumnLookupValueService columnLookupValueService;


    public void InitTest()
    {
        patientService = new PatientService("AccountEndpoint=https://90d9a871-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=kNIEVzRYgQo4XyctXVd4qqNQeadwZPFbacSI2ByCu4mBOAqYMabzDpB2OyqAaeF35Glr7A57R2o9ACDblQMi2Q==;", "PatientData", "Patient");
        admissionTypeService = new AdmissionService("AccountEndpoint=https://90d9a871-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=kNIEVzRYgQo4XyctXVd4qqNQeadwZPFbacSI2ByCu4mBOAqYMabzDpB2OyqAaeF35Glr7A57R2o9ACDblQMi2Q==;", "PatientData", "AdmissionType");
         explanationService = new ExplanationService("AccountEndpoint=https://90d9a871-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=kNIEVzRYgQo4XyctXVd4qqNQeadwZPFbacSI2ByCu4mBOAqYMabzDpB2OyqAaeF35Glr7A57R2o9ACDblQMi2Q==;", "PatientData", "Explanations");
        columnLookupValueService = new ColumnLookupValueService("AccountEndpoint=https://90d9a871-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=kNIEVzRYgQo4XyctXVd4qqNQeadwZPFbacSI2ByCu4mBOAqYMabzDpB2OyqAaeF35Glr7A57R2o9ACDblQMi2Q==;", "PatientData", "ColumnLookupValues");
    }

 
    public async Task Test_01_GetAllPatients()
    {
        var result = await patientService.GetAllPatient();
        int count =0;

        foreach (var item in result)
        {
            // Console.WriteLine(JsonConvert.SerializeObject(item));
            count +=1;

        }
        Console.WriteLine(count);
    }

    public async Task Test_02_GetPatient()
    {
        var result = await patientService.GetPatient("1");
     
         
             Console.WriteLine(JsonConvert.SerializeObject(result));
        
       
    }

   
    public async Task Test_02_UpdateScore()
    {
     var result = await patientService.UpdateScore("41044", (decimal)0.6);
    }

 
    public async Task Test_03_RegisterPatient()
    {
        BasicPatientProfile newPatient = new BasicPatientProfile() { LastName = "testando", FirstName = "teste", Age = 10, Gender = Gender.Male};

        var result = await patientService.RegisterPatient(newPatient);
        Console.WriteLine(JsonConvert.SerializeObject(result));

        // await patientService.DeletePatient(result.id, result.Id);

    }

        public async Task Test_01_GetTop5ExplanationsTest()
    {
        var result = await explanationService.GetTop5Explanations(columnLookupValueService ,"1643712811");
        foreach (var item in result)
        {
            Console.WriteLine(JsonConvert.SerializeObject(item));
        }

    }

            public async Task Test_01_GetExamTest()
    {
        var result = await patientService.GetExam("1643712811");

        Console.WriteLine(result);
        // foreach (var item in result)
        // {
        //     Console.WriteLine(JsonConvert.SerializeObject(item));
        // }

    }

       public static async Task Main()
    {
        PatientServiceTests minhaClasse = new PatientServiceTests();
        minhaClasse.InitTest();
        await minhaClasse.Test_01_GetExamTest();
       
   }

}

