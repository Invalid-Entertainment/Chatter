Console.WriteLine(ReleaseChecker.GetLatestRelease());
Console.ReadLine();

class ReleaseChecker
{
    const string API_PATH = "https://api.github.com/repos/Invalid-Entertainment/Chatter/releases/latest";

    public static string? GetLatestRelease()
    {
        try
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Chatter");
            string res = client.GetStringAsync(API_PATH).Result;
            res = res[(res.IndexOf(",\"tag_name\":\"") + 13)..];
            res = res[..res.IndexOf('"')];
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}
