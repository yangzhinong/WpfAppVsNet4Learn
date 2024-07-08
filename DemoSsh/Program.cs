using Renci.SshNet;
using System.Text;

namespace DemoSsh;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Please input root user's password:");
        var pwd = GetPwdFromConsole();

        var b = await DownloadShhFileAsync("/root/v2rayN-With-Core.zip", "c:\\Tool\\v2rayN-With-Core.zip",
            "192.210.203.81","root", pwd);
        Console.WriteLine("Hello, World!");
    }

    private static string GetPwdFromConsole()
    {
        StringBuilder passwordBuilder = new StringBuilder();
        bool continueReading = true;
        char newLineChar = '\r';
        while (continueReading)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            char passwordChar = consoleKeyInfo.KeyChar;

            if (passwordChar == newLineChar)
            {
                continueReading = false;
            }
            else
            {
                passwordBuilder.Append(passwordChar.ToString());
            }
        }
        return passwordBuilder.ToString();
    }


    /// <summary>从SFtp服务器下载文件</summary>
    public static Task<bool> DownloadShhFileAsync(string uri, string localPath, string ip, string user, string pwd, int timeoutSecond=60000)
    {
        Task<bool> ftpTask = Task.Run(() =>
        {
            try
            {
                using var sftp = new SftpClient(ip, user, pwd);
                sftp.Connect();
                
                using var file = File.OpenWrite(localPath);
                sftp.DownloadFile(uri, file);
                sftp.Disconnect();
                return true;
            }
            catch
            {
                return false;
            }
        });
        Task delayTask = Task.Delay(TimeSpan.FromSeconds(timeoutSecond));
        var completedTask = Task.WhenAny(ftpTask, delayTask).GetAwaiter().GetResult();
        if (completedTask == ftpTask)
        {
            return ftpTask;
        }
        else
        {
            return Task.FromResult(false);
        }
    }
}
