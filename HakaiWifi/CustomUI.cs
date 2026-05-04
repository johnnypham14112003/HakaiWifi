using Spectre.Console;

namespace HakaiWifi
{
    public static class CustomUI
    {
        public static void ShowScreenSizeWarning()
        {
            Console.Clear();

            TypeWrite(">>> For best display UI size, adjust your prompt tab horizontal size until fit the green line  below <<<", "white on red");

            Console.WriteLine();

            WriteLineColor("---------------------------------------------------------------------------------------------------------------", "green");

            Console.WriteLine();

            TypeWrite("(Don't let this line enter new row or split into 2 line)", "yellow");

            WaitForUser();
        }

        public static void WarningResponsibility()
        {
            Console.Clear();
            Console.Write("                                           ");
            WriteLineColor(">>> WARNING <<<", "white on red");
            TypeWrite(" => Before using this tool, you should notice that:", "red", 30);
            Console.WriteLine();
            TypeWrite("    + This software is made and developed by JohnnyPham. Only use if you trust him. :>>", "yellow");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine();
            TypeWrite("    + An illegal network attack tool so you can be arrested if you use it for the purpose of illegally", "yellow");
            Console.WriteLine();
            TypeWrite("      accessing other people's wifi. You will have to take full responsibility for what you cause.", "yellow");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine();
            TypeWrite("    + It may not always work successfully but it is still a working brute force tool", "yellow");
            Console.WriteLine();
            TypeWrite("      on Windows if you give it a time to run enough.", "yellow");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine();
            TypeWrite("    + This tool can access personal data and requires system access permissions. Continue using means", "yellow");
            Console.WriteLine();
            TypeWrite("      that you agree to my terms of use and privacy policy.", "yellow");
            Console.WriteLine();
            TypeWrite("      Actually, there is no terms or policy, just don't leak to the goverment, otherwise don't mind them. :D", "yellow");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine();
            TypeWrite("    + The developer is not responsible for any damage arising from the use of the application, including but", "yellow");
            Console.WriteLine();
            TypeWrite("      not limited to data loss, service interruptions, or legal consequences. Users are fully responsible for", "yellow");
            Console.WriteLine();
            TypeWrite("      their actions and decisions when using the tool.", "yellow");
            Console.WriteLine();

            WaitForUser();
        }

        public static void Trolling()
        {
            AnsiConsole.Markup("[green]Installing Virus[/]");
            for (short i = 0; i < 3; i++)
            {
                AnsiConsole.Markup("[green].[/]");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
            Thread.Sleep(3000);

            WriteLineColor("Virus Installed!", "green");
            Thread.Sleep(3000);

            AnsiConsole.Markup("[green]Virus Initiating[/]");
            for (short i = 0; i < 3; i++)
            {
                AnsiConsole.Markup("[green].[/]");
                Thread.Sleep(1000);
            }
            Thread.Sleep(3000);

            WriteLineColor(" (Done)","green");
            Thread.Sleep(3000);

            WriteLineColor(" :)","green");
            Thread.Sleep(2000);
            WriteLineColor(" :)","red");
            Thread.Sleep(1000);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)","blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)","blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)","green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)","red");
            Thread.Sleep(100);
            WriteLineColor(" :)", "green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)", "red");
            Thread.Sleep(100);
            WriteLineColor(" :)", "green");
            Thread.Sleep(100);
            WriteLineColor(" :)", "blue");
            Thread.Sleep(100);
            WriteLineColor(" :)", "red");

            Console.WriteLine();
            WriteLineColor(" JUST KIDDING :D", "green");
            Thread.Sleep(3000);
            WriteLineColor("  THERE ARE NO VIRUS, SO JUST RELAX", "green");
            Thread.Sleep(3000);

            WaitForUser();
        }

        public static void LogoTitle()
        {
            WriteLineColor("   __        __        __ (O)  _____  (O)      _   _       __       _   __      __       _ ", "green");
            Thread.Sleep(500);
            WriteLineColor("   \\ \\      /  \\      / /  _  |  ___|  _      | | | |     /  \\     | | / /     /  \\     | |", "green");
            Thread.Sleep(500);
            WriteLineColor("    \\ \\    / /\\ \\    / /  | | | |___  | |     | |_| |    / /\\ \\    | |/ /     / /\\ \\    | |", "green");
            Thread.Sleep(500);
            WriteLineColor("     \\ \\  / /  \\ \\  / /   | | |  ___| | |     |  _  |   / /__\\ \\   | ( (     / /__\\ \\   | |", "green");
            Thread.Sleep(500);
            WriteLineColor("      \\ \\/ /    \\ \\/ /    | | | |     | |     | | | |  / ______ \\  | |\\ \\   / ______ \\  | |", "green");
            Thread.Sleep(500);
            WriteLineColor("       \\__/      \\__/     |_| |_|     |_|     |_| |_| /_/      \\_\\ |_| \\_\\ /_/      \\_\\ |_|", "green");
        }

        public static void Credits()
        {
            AnsiConsole.Markup("[green]                                                                                        version [/]");
            AnsiConsole.Markup("[bold Fuchsia]Console App[/]");
            Console.WriteLine();
            Thread.Sleep(500);
            AnsiConsole.Markup("[green]                                                                                        created by [/]");
            AnsiConsole.Markup("[bold Fuchsia]JP Dev[/]");
        }


        // ---------- HELPER METHODS ----------
        public static void TypeWrite(string text, string color, int delayMilliseconds = 15)
        {
            foreach (char c in text)
            {
                if (c == '\n')
                {
                    Console.WriteLine();
                    continue;
                }

                AnsiConsole.Markup($"[{color}]{c}[/]");
                Thread.Sleep(delayMilliseconds);
            }
        }

        public static void WriteLineColor(string text, string color)
        {
            AnsiConsole.MarkupLine($"[{color}]{text}[/]");
        }

        /// <summary>
        /// Help pause the screen before next command running
        /// </summary>
        public static void WaitForUser()
        {
            Console.WriteLine();
            AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
