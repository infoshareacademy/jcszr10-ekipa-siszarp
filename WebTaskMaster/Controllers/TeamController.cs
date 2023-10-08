using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public TeamController(ITeamService teamService, IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var models = CreateBasicModels();

            return View(models);
        }

        
        [HttpPost]
        public IActionResult Create(TeamNameModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(Create);

                var models = CreateBasicModels();

                return View("Index", models);
            }

            model.Description ??= string.Empty;

            _teamService.CreateTeam(model.Name, model.Description);

            TempData["ToastMessage"] = "Team added.";

            return RedirectToAction("Index");
        }

        private IEnumerable<TeamIndexModel> CreateBasicModels()
        {
            var teams = _teamService.GetAllTeams();

            List<TeamIndexModel> teamsModels = new();

            foreach (var team in teams)
            {
                var nameModel = new TeamNameModel
                {
                    Name = team.Name,
                    Description = team.Description
                };

                TeamMemberModel? leaderModel = null;

                if (team.Leader is not null)
                {
                    leaderModel = new TeamMemberModel
                    {
                        Id = team.Leader.Id,
                        FirstName = team.Leader.FirstName,
                        LastName = team.Leader.LastName
                    };
                }

                var numOfMembers = team.Members.Count;

                teamsModels.Add(new TeamIndexModel
                {
                    Id = team.Id,
                    NameModel = nameModel,
                    LeaderModel = leaderModel,
                    NumOfMembers = numOfMembers
                });
            }

            return teamsModels;
        }

        public IActionResult Details(Guid teamId)
        {
            var team = _teamService.GetTeamById(teamId);

            if (team is null)
            {
                return RedirectToAction("Index");
            }

            var teamModel = CreateDetailsModel(team);

            return View(teamModel);
        }

        public IActionResult Delete(Guid teamId)
        {
            _teamService.DeleteTeam(teamId);

            TempData["ToastMessage"] = "Team deleted.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(TeamNameModel model, Guid teamId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(Edit);

                var team = _teamService.GetTeamById(teamId);

                if (team is null)
                {
                    return RedirectToAction("Index");
                }

                var teamModel = CreateDetailsModel(team);

                return View("Details", teamModel);
            }

            var newDescription = model.Description ?? string.Empty;

            _teamService.EditNameAndDescription(teamId, model.Name, newDescription);

            TempData["ToastMessage"] = "Changes saved.";

            return RedirectToAction("Details", new { teamId });
        }

        [HttpPost]
        public IActionResult AddMembers(TeamAddMembersModel model, Guid teamId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(AddMembers);

                var team = _teamService.GetTeamById(teamId);

                if (team is null)
                {
                    return RedirectToAction("Index");
                }

                var teamModel = CreateDetailsModel(team);

                return View("Details", teamModel);
            }

            _teamService.AddMembersToTeam(teamId, model.MembersIdsToAdd);

            TempData["ToastMessage"] = "Team member added";

            return RedirectToAction("Details", new { teamId });
        }

        public IActionResult DeleteMember(Guid teamId, Guid memberId)
        {
            _teamService.DeleteMemberFromTeam(teamId, memberId);

            TempData["ToastMessage"] = "Team member removed.";

            return RedirectToAction("Details", new { teamId });
        }

        [HttpPost]
        public IActionResult ChangeLeader(TeamChangeLeaderModel model, Guid teamId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(ChangeLeader);

                var team = _teamService.GetTeamById(teamId);

                if (team is null)
                {
                    return RedirectToAction("Index");
                }

                var teamModel = CreateDetailsModel(team);

                return View("Details", teamModel);
            }

            _teamService.ChangeTeamLeader(teamId, model.NewLeaderId);

            TempData["ToastMessage"] = "Leader changed.";

            return RedirectToAction("Details", new { teamId });
        }

        private TeamDetailsModel CreateDetailsModel(Team team)
        {
            var members = team.Members.Select(m => new TeamMemberModel
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
            })
                .ToList();

            var leader = team.Leader is not null
                ? members.First(m => m.Id == team.Leader.Id)
                : null;

            var addMembersModel = CreateAddMembersModel(members);

            var changeLeaderModel = CreateChangeLeaderModel(leader, members);

            var nameModel = new TeamNameModel
            {
                Name = team.Name,
                Description = team.Description
            };

            var teamModel = new TeamDetailsModel
            {
                TeamId = team.Id,
                NameModel = nameModel,
                Leader = leader,
                Members = members,
                AddMembersModel = addMembersModel,
                ChangeLeaderModel = changeLeaderModel
            };

            return teamModel;
        }

        private static TeamChangeLeaderModel CreateChangeLeaderModel(TeamMemberModel? leader, IEnumerable<TeamMemberModel> members)
        {
            var availableLeaders = leader is not null
                ? members.Where(m => m.Id != leader.Id)
                : members;

            var availableLeadersList = availableLeaders.ToList();

            var changeLeaderModel = new TeamChangeLeaderModel
            {
                AvailableLeaders = availableLeadersList
            };

            return changeLeaderModel;
        }

        private TeamAddMembersModel CreateAddMembersModel(IEnumerable<TeamMemberModel> members)
        {
            var availableMembers = _userService.GetAllUsers()
                .ExceptBy(members.Select(m1 => m1.Id), m2 => m2.Id)
                .Select(m => new TeamMemberModel
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                })
                .ToList();

            var addMembersModel = new TeamAddMembersModel
            {
                AvailableMembers = availableMembers
            };
            return addMembersModel;
        }
    }
}
