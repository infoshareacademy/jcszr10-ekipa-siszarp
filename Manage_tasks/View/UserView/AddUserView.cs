using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.UserView;

public class AddUserView
{
    public void Run()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Dodawanie nowego użytkownika");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj imię użytkownika");
        var firstName = Console.ReadLine();

        DrawMessageAtCenter("Podaj nazwisko użytkownika");
        var lastName = Console.ReadLine();

        DrawMessageAtCenter("Podaj stanowisko użytkownika");
        var position = Console.ReadLine();

        Console.ResetColor();

        var newUser = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Position = position
        };

        Data.UserService.UpdateUser(newUser);
    }

    private void DrawMessageAtCenter(string message)
    {
        var cursorLeft = (Console.WindowWidth - message.Length) / 2;

        Console.CursorLeft = cursorLeft;

        Console.WriteLine(message);

        Console.CursorLeft = cursorLeft;
    }
}
