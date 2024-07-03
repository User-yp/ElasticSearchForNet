using Elasticsearch.Domain.Entity;
using Elasticsearch.Domain.IService;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.WebApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IActorsService actorsService;

    public ActorsController(IActorsService actorsService )
    {
        this.actorsService = actorsService;
    }
    [HttpGet]
    public async Task<ActionResult> Connection()
    {
        var res = await actorsService.TestConnectionAsync();
        return Ok(res.DebugInformation);
    }
    [HttpPost("sample")]
    public async Task<ActionResult> PostSampleData()
    {
        await actorsService.InsertManyAsync();

        return Ok(new { Result = "Data successfully registered with Elasticsearch" });
    }

    [HttpPost("exception")]
    public IActionResult PostException()
    {
        throw new Exception("Generate sample exception");
    }

    [HttpGet("")]
    public async Task<ActionResult<Actors>> GetAllAct()
    {
        var result = await actorsService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("name-match")]
    public async Task<ActionResult> GetByNameWithMatch([FromQuery] string name)
    {
        var result = await actorsService.GetByNameWithMatch(name);

        return Ok(result);
    }

    [HttpGet("name-multimatch")]
    public async Task<ActionResult> GetByNameAndDescriptionMultiMatch([FromQuery] string term)
    {
        var result = await actorsService.GetByNameAndDescriptionMultiMatch(term);

        return Ok(result);
    }

    [HttpGet("name-matchphrase")]
    public async Task<ActionResult> GetByNameWithMatchPhrase([FromQuery] string name)
    {
        var result = await actorsService.GetByNameWithMatchPhrase(name);

        return Ok(result);
    }

    [HttpGet("name-matchphraseprefix")]
    public async Task<ActionResult> GetByNameWithMatchPhrasePrefix([FromQuery] string name)
    {
        var result = await actorsService.GetByNameWithMatchPhrasePrefix(name);

        return Ok(result);
    }

    [HttpGet("name-term")]
    public async Task<ActionResult> GetByNameWithTerm([FromQuery] string name)
    {
        var result = await actorsService.GetByNameWithTerm(name);

        return Ok(result);
    }

    [HttpGet("name-wildcard")]
    public async Task<ActionResult> GetByNameWithWildcard([FromQuery] string name)
    {
        var result = await actorsService.GetByNameWithWildcard(name);

        return Ok(result);
    }

    [HttpGet("name-fuzzy")]
    public async Task<ActionResult> GetByNameWithFuzzy([FromQuery] string name)
    {
        var result = await actorsService.GetByNameWithFuzzy(name);

        return Ok(result);
    }

    [HttpGet("description-match")]
    public async Task<ActionResult> GetByDescriptionMatch([FromQuery] string description)
    {
        var result = await actorsService.GetByDescriptionMatch(description);

        return Ok(result);
    }

    [HttpGet("all-fields")]
    public async Task<ActionResult> SearchAllProperties([FromQuery] string term)
    {
        var result = await actorsService.SearchInAllFiels(term);

        return Ok(result);
    }

    [HttpGet("condiction")]
    public async Task<ActionResult> GetByCondictions([FromQuery] string name, [FromQuery] string description, [FromQuery] DateTime? birthdate)
    {
        var result = await actorsService.GetActorsCondition(name, description, birthdate);

        return Ok(result);
    }

    [HttpGet("term")]
    public async Task<ActionResult> GetByAllCondictions([FromQuery] string term)
    {
        var result = await actorsService.GetActorsAllCondition(term);

        return Ok(result);
    }

    [HttpGet("aggregation")]
    public async Task<ActionResult> GetActorsAggregation()
    {
        var result = await actorsService.GetActorsAggregation();

        return Ok(result);
    }
}
