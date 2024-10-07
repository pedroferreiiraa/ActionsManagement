using _5W2H.Application.ProjectCommands.InsertProject;
using _5W2H.Application.ProjectQueries.GetAllProjects;
using _5W2H.Application.ProjectQueries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _5W2H.Api.Controllers;

[ApiController]
[Route("/api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
            
    }

    [HttpGet]
    public async Task<IActionResult> Get(string search = "")
    {
        var query = new GetAllProjectsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id));
    
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
    
        return Ok(result);
    }
    
    
    
    
    [HttpPost]
    public async Task<IActionResult> Post(InsertProjectCommand command)
    {
        var result = await _mediator.Send(command);
    
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }
    
    
}