// public class WeatherForecastControllerTests
// {
//     [Fact]
//     public async Task GET_retrieves_weather_forecast()
//     {
//         await using var application = new WebApplicationFactory<PatientsController.GetExamData.Startup>();
//         using var client = application.CreateClient();
//         var response = await client.GetAsync("/myendpoint");
//         response.StatusCode.Should().Be(HttpStatusCode.OK);
//         var content = await response.Content.ReadAsStringAsync();
//         var result = JsonConvert.DeserializeObject<Exam>(content);
//         Assert.Equal("expected value", result.MyProperty);
//     }
// }