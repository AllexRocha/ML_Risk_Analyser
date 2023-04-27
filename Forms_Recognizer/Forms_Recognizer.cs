/*
  This code sample shows Prebuilt Document operations with the Azure Form Recognizer client library. 

  To learn more, please visit the documentation - Quickstart: Form Recognizer C# client library SDKs
  https://docs.microsoft.com/en-us/azure/applied-ai-services/form-recognizer/quickstarts/try-v3-csharp-sdk
*/

using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Newtonsoft.Json;

/*
  Remember to remove the key from your code when you're done, and never post it publicly. For production, use
  secure methods to store and access your credentials. For more information, see 
  https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-security?tabs=command-line%2Ccsharp#environment-variables-and-application-configuration
*/
string endpoint = "https://formsteste.cognitiveservices.azure.com/";
string key = "6b4446031b3f47d5ae062269b7632ccb";

AzureKeyCredential credential = new AzureKeyCredential(key);
DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

// sample document
Uri fileUri = new Uri("https://raw.githubusercontent.com/AllexRocha/ML_Risk_Analyser/master/Forms_Recognizer/exames/exame_2.pdf");

AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-document", fileUri);

AnalyzeResult result = operation.Value;

 Dictionary<string, object> dict = new Dictionary<string, object>();

foreach (DocumentKeyValuePair kvp in result.KeyValuePairs)
{
       dict.Add(kvp.Key.Content, kvp.Value.Content);
}

String json = JsonConvert.SerializeObject(dict, new JsonSerializerSettings { Formatting = Formatting.None });

Console.WriteLine(json);