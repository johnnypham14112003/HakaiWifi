using Spectre.Console;

namespace HakaiWifi
{
    public static class CustomUI
    {
        public static void ShowScreenSizeWarning()
        {
            //string text = "Đang tiến hành giải mã chuẩn bảo mật WPA3...\n";

            //foreach (char c in text)
            //{
            //    Console.Write(c);
            //    Thread.Sleep(15);
            //}
            Console.Clear();

            AnsiConsole.MarkupLine("[white on red]>>> For best display UI size, adjust your prompt tab horizontal size until fit the green line  below <<<[/]");

            Console.WriteLine();

            AnsiConsole.MarkupLine("[green]---------------------------------------------------------------------------------------------------------------[/]");

            Console.WriteLine();

            AnsiConsole.MarkupLine("[yellow](Don't let this line enter new row or split into 2 line)[/]");

            Console.WriteLine();
            AnsiConsole.MarkupLine("[green]Press any key to continue![/]");

            Console.ReadKey(true);
        }
    }
}
