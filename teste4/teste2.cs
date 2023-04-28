public class WeatherForecastControllerTests
{
    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        await using var application = new WebApplicationFactory<PatientsController.GetExamData.Startup>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/Patients/Exam/1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}