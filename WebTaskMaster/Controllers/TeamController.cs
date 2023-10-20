using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Controllers;

public class TeamController : Controller
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;

    public TeamController(ITeamService teamService, IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> List()
    {
        var dto = await _teamService.GetTeamList();

        return View(dto);
    }

    [Authorize(Roles = "User")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Add(TeamAddModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<TeamAddModel,TeamAddDto>(model);
        await _teamService.AddTeam(dto);

        TempData["ToastMessage"] = "Team added.";

        return RedirectToAction("List");
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> Details(Guid teamId)
    {
        var dto = await _teamService.GetTeamDetails(teamId);

        var model = _mapper.Map<TeamDetailsDto, TeamDetailsModel>(dto);

        return View(model);
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> Delete(Guid teamId)
    {
        await _teamService.DeleteTeam(teamId);

        TempData["ToastMessage"] = "Team deleted.";

        return RedirectToAction("List");
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> Edit(Guid teamId)
    {
        var dto = await _teamService.GetTeamBasic(teamId);

        var model = _mapper.Map<TeamBasicDto, TeamEditModel>(dto);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Edit(TeamEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<TeamEditModel, TeamNameEditDto>(model);
        await _teamService.EditTeam(dto);

        TempData["ToastMessage"] = "Name and description changed.";

        return RedirectToAction("Details", new { model.TeamId });
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> ChangeLeader(Guid teamId)
    {
        var dto = await _teamService.GetAvailableTeamLeaders(teamId);

        if (dto.Count == 0)
        {
            throw new InvalidOperationException($"There are no available leaders for the team. Team Id: {teamId}");
        }

        var model = new TeamChangeLeaderModel
        {
            TeamId = teamId
        };

        _mapper.Map(dto, model);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ChangeLeader(TeamChangeLeaderModel model)
    {
        if (!ModelState.IsValid)
        {
            _mapper.Map(await _teamService.GetAvailableTeamLeaders(model.TeamId), model);

            return View(model);
        }

        var dto = _mapper.Map<TeamChangeLeaderModel, TeamChangeLeaderDto>(model);

        await _teamService.ChangeTeamLeader(dto);

        TempData["ToastMessage"] = "Leader changed.";

        return RedirectToAction("Details", new { dto.TeamId });
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> RemoveLeader(Guid teamId)
    {
        await _teamService.RemoveLeader(teamId);

        TempData["ToastMessage"] = "Leader removed.";

        return RedirectToAction("Details", new { teamId });
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> AddMembers(Guid teamId)
    {
        var dto = await _teamService.GetAvailableTeamMembers(teamId);

        if (dto.Count == 0)
        {
            TempData["ToastMessage"] = "No available members.";
            TempData["ToastColorClass"] = "bg-danger";

            return RedirectToAction("Details", new { teamId });
        }

        var model = new TeamAddMembersModel
        {
            TeamId = teamId
        };

        _mapper.Map(dto, model);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> AddMembers(TeamAddMembersModel model)
    {
        if (!ModelState.IsValid)
        {
            _mapper.Map(await _teamService.GetAvailableTeamMembers(model.TeamId), model);

            return View(model);
        }

        var dto = _mapper.Map<TeamAddMembersModel, TeamAddMembersDto>(model);

        await _teamService.AddTeamMembers(dto);

        TempData["ToastMessage"] = "Members added.";

        return RedirectToAction("Details", new { model.TeamId });
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> RemoveMembers(Guid teamId)
    {
        var dto = await _teamService.GetAvailableTeamRemoveMembers(teamId);

        if (dto.Count == 0)
        {
            return RedirectToAction("Details", new { teamId });
        }

        var model = new TeamRemoveMembersModel
        {
            TeamId = teamId
        };

        _mapper.Map(dto, model);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> RemoveMembers(TeamRemoveMembersModel model)
    {
        if (!ModelState.IsValid)
        {
            _mapper.Map(await _teamService.GetAvailableTeamRemoveMembers(model.TeamId), model);

            return View(model);
        }

        var dto = _mapper.Map<TeamRemoveMembersModel, TeamRemoveMembersDto>(model);

        await _teamService.RemoveTeamMembers(dto);

        TempData["ToastMessage"] = "Members removed.";

        return RedirectToAction("Details", new { model.TeamId });
    }
}