using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PlanetsController : ControllerBase
{
    
    private readonly IMediator _mediator;

    /// <summary>
    ///     Planet Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    
    public PlanetsController(IMediator mediator)
    {
        _mediator = mediator;        
    }

    /// <summary>
    ///     Get a list of every planet
    /// </summary>
    //[HttpGet]
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanetVm>> GetPlanets()
    {
        return Ok(await _mediator.Send(new GetPlanetsQuery()));
    }

    /// <summary>
    ///     Get a specific planet by an id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetPlanetById/{id}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanetDto>> GetPlanetById(int id)
    {
        return Ok(await _mediator.Send(new GetPlanetByIdQuery(id)));
    }

    /// <summary>
    ///     Create a new planet
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("Create")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PlanetDto>> CreatePlanet([FromBody] PlanetInput planetInput)
    {
        CreatePlanetCommand command = new CreatePlanetCommand(planetInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing planet
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanetDto>> UpdatePlanet([FromBody] PlanetInput planetInput)
    {
        UpdatePlanetCommand command = new UpdatePlanetCommand(planetInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing Planet
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePlanet([FromBody] DeletePlanetCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}