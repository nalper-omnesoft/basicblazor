namespace DevOpsAssignment.Web;

public class GreetingApiClient(HttpClient httpClient)
{
    public async Task<string> GetGreetingAsync()
    {
        var response = await httpClient.GetStringAsync("/greeting");
        return response;
    }
}