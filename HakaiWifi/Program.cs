using HakaiWifi;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();

        // Loading line
        AnsiConsole.Status()
            .SpinnerStyle(Style.Parse("green"))
            .Start("Initiating app...", ctx =>
            {
                Thread.Sleep(3000);
            });

        CustomUI.ShowScreenSizeWarning();
        CustomUI.WarningResponsibility();
        CustomUI.Trolling();

        CustomUI.LogoTitle();
        CustomUI.Credits();
        Console.WriteLine();

        bool firstRun = true;
        bool isAppRunning = true;

        while (isAppRunning)
        {
            if (firstRun == false)
                CustomUI.LogoTitle();
            firstRun = false;

            isAppRunning = HandleLogic.MainMenu();
            Console.Clear();
        }
    }
}