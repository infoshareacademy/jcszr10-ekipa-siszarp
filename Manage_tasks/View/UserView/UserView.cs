using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.UserView;

public class UserView
{
    private readonly User _user;

    public UserView(User user)
    {
        _user = user;
    }

    public void Run()
    {
        var options = new string[] { "Edytuj imię", "Edytuj nazwisko", "Edytuj stanowisko", "Usuń użytkownika", "Wstecz" };

        var _goBackIndex = options.Length - 1;

        int optionIndex = int.MaxValue;

        while (optionIndex != _goBackIndex)
        {
            var prompt = $"Użytkownik {_user}";

            optionIndex = new ManageMenu(prompt, options).Run();

            switch (optionIndex)
            {
                case 0:
                    EditFirstName();
                    break;

                case 1:
                    EditLastName();
                    break;

                case 2:
                    EditPosition();
                    break;

                case 3:
                    Data.UserService.DeleteUser(_user.Id);
                    optionIndex = _goBackIndex;

                    break;

                default:
                    break;
            }
        }
    }

    public void EditFirstName()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Edycja imienia użytkownika");
        Console.WriteLine();

        DrawMessageAtCenter($"Aktualne imię: {_user.FirstName}");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj nowe imię użytkownika");
        var newFirstName = Console.ReadLine();

        Console.ResetColor();

        _user.FirstName = newFirstName;

        Data.UserService.UpdateUser(_user);
    }

    public void EditLastName()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Edycja nazwiska użytkownika");
        Console.WriteLine();

        DrawMessageAtCenter($"Aktualne nazwisko: {_user.LastName}");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj nowe nazwisko użytkownika");
        var newLastName = Console.ReadLine();

        Console.ResetColor();

        _user.LastName = newLastName;

        Data.UserService.UpdateUser(_user);
    }

    public void EditPosition()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Edycja stanowiska użytkownika");
        Console.WriteLine();

        DrawMessageAtCenter($"Aktualne stanowisko: {_user.Position}");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj nowe stanowisko użytkownika");
        var newPosition = Console.ReadLine();

        Console.ResetColor();

        _user.Position = newPosition;

        Data.UserService.UpdateUser(_user);
    }

    private void DrawMessageAtCenter(string message)
    {
        var cursorLeft = (Console.WindowWidth - message.Length) / 2;

        Console.CursorLeft = cursorLeft;

        Console.WriteLine(message);

        Console.CursorLeft = cursorLeft;
    }
}
