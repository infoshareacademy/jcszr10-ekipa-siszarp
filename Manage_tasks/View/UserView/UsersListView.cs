using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.UserView;

public class UsersListView
{
    private readonly List<User> _users = new();

    private readonly string[] _staticOptions = { "Dodaj nowego użytkownika", "Wstecz" };

    private string[] _allOptions;

    private int _goBackIndex;
    private int _addUserIndex;

    public void Run()
    {
        var prompt = "Lista użytkowników";

        int optionIndex = int.MaxValue;
        _goBackIndex = int.MinValue;

        while (optionIndex != _goBackIndex)
        {
            UpdateOptions();

            optionIndex = new ManageMenu(prompt, _allOptions).Run();

            if (optionIndex == _addUserIndex)
            {
                var addUserView = new AddUserView();
                addUserView.Run();
            }
            else if (optionIndex < _users.Count)
            {
                var userView = new UserView(_users[optionIndex]);
                userView.Run();
            }
        }
    }

    public void UpdateOptions()
    {
        _users.Clear();
        _users.AddRange(Data.UserService.GetAllUsers());

        var options = new List<string>();

        if (_users.Count > 0)
        {
            options.AddRange(_users.Select(user => user.ToString()));
        }

        options.AddRange(_staticOptions);

        _goBackIndex = options.Count - 1;
        _addUserIndex = options.Count - 2;

        _allOptions = options.ToArray();
    }
}
