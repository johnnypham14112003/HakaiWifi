using HakaiWifi;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        // Loading line
        AnsiConsole.Status()
            .SpinnerStyle(Style.Parse("green"))
            .Start("Initiating app...", ctx =>
            {
                Thread.Sleep(3000);
            });

        CustomUI.ShowScreenSizeWarning();
        bool isAppRunning = true;

        while (isAppRunning)
        {
            Console.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[green]=== CÔNG CỤ QUẢN LÝ WI-FI ===[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Quét mạng Wi-Fi xung quanh",
                        "2. Kết nối đến một mạng (Nhập mật khẩu)",
                        "3. Thoát ứng dụng"
                    }));

            // 2. Xử lý lựa chọn của người dùng
            switch (choice)
            {
                case "1. Quét mạng Wi-Fi xung quanh":
                    RunScanWifi(); // Gọi hàm quét Wi-Fi
                    break;

                case "2. Kết nối đến một mạng (Nhập mật khẩu)":
                    RunConnectWifi(); // Gọi hàm kết nối
                    break;

                case "3. Thoát ứng dụng":
                    isAppRunning = false; // Đổi cờ hiệu thành false để thoát vòng lặp
                    AnsiConsole.MarkupLine("[red]Đang thoát ứng dụng... Tạm biệt![/]");
                    break;
            }
        }
    }

    // --- CÁC HÀM XỬ LÝ LOGIC ---

    static void RunScanWifi()
    {
        AnsiConsole.MarkupLine("[yellow]Đang quét Wi-Fi... Vui lòng chờ...[/]");
        // Viết code gọi "netsh wlan show networks" ở đây...

        // Cực kỳ quan trọng: Dừng màn hình lại để người dùng đọc kết quả 
        // trước khi vòng lặp quay lại từ đầu và xóa mất màn hình.
        AnsiConsole.MarkupLine("\nNhấn phím [cyan]Enter[/] để quay lại menu chính...");
        Console.ReadLine();
    }

    static void RunConnectWifi()
    {
        // Giao diện nhập liệu ẩn mật khẩu bằng dấu *
        var ssid = AnsiConsole.Ask<string>("Nhập tên mạng [green](SSID)[/]: ");
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Nhập [red]mật khẩu[/]: ")
                .Secret()); // Che mật khẩu

        AnsiConsole.MarkupLine($"[yellow]Đang thử kết nối tới {ssid}...[/]");
        // Viết code tạo file XML và gọi lệnh kết nối ở đây...
        // Nếu kết nối sai, bạn có thể dùng một vòng lặp while() nhỏ bên trong hàm này 
        // để bắt người dùng nhập lại mật khẩu liên tục cho tới khi đúng thì thôi.

        AnsiConsole.MarkupLine("\nNhấn phím [cyan]Enter[/] để quay lại menu chính...");
        Console.ReadLine();
    }
}