Console.WriteLine($"My Version:\t{Generated.REV}");
Console.WriteLine($"Latest Version:\t{ReleaseChecker.GetLatestRelease()}");

ReleaseChecker.CheckForUpdates();

Console.ReadLine();

class ReleaseChecker
{
    const string API_PATH = "https://api.github.com/repos/Invalid-Entertainment/Chatter/releases/latest";
    const string REL_PATH = "https://github.com/Invalid-Entertainment/Chatter/releases/latest";

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

    public static void OpenNewRelease(string newVersion)
    {
        var result = Windows.Win32.PInvoke.MessageBox(Windows.Win32.Foundation.HWND.Null, $"An update is available! {Generated.REV} (current) -> {newVersion} (new). Would you like to update?", "Update available!", Windows.Win32.UI.WindowsAndMessaging.MESSAGEBOX_STYLE.MB_YESNO);

        if (result != Windows.Win32.UI.WindowsAndMessaging.MESSAGEBOX_RESULT.IDYES)
            return;

        Windows.Win32.PInvoke.MessageBox(Windows.Win32.Foundation.HWND.Null, "Chatter will now open both the new release download location and the current installed Chatter.", "Opening new release", Windows.Win32.UI.WindowsAndMessaging.MESSAGEBOX_STYLE.MB_OK);

        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = REL_PATH, UseShellExecute = true });
        System.Diagnostics.Process.Start("explorer", ".");
    }

    public static void CheckForUpdates()
    {
        string? latest = GetLatestRelease();
        if (latest != null && latest != Generated.REV)
            OpenNewRelease(latest);
    }
}
