using Microsoft.AspNetCore.Mvc;
using MyTestableApi.Api.Models;

namespace MyTestableApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LanguesParleesController : ControllerBase
{
    private readonly ILogger<LanguesParleesController> _logger;

    private readonly List<LanguesParleesModel> langues = new()
    {
        new() {Pays = "France", LanguesParlees = new() {"Français"}},
        new() {Pays = "États-Unis", LanguesParlees = new() {}},
        new() {Pays = "Suisse", LanguesParlees = new() {"Français", "Allemand", "Italien", "Romanche"}},
        new() {Pays = "Italie", LanguesParlees = new() {"Italien"}}
    };

    public LanguesParleesController(ILogger<LanguesParleesController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/Langues/{PaysDemande}")]
    public IActionResult Get(string PaysDemande)
    {
        if (string.IsNullOrWhiteSpace(PaysDemande)) {
            return BadRequest();
        }

        PaysDemande = PaysDemande.Trim();

        LanguesParleesModel ?RetourPays = langues.Find(NomPays => NomPays.Pays.Equals(PaysDemande));

        if (RetourPays is null) {
            return BadRequest();
        } else if (RetourPays.LanguesParlees is null) {
            return NotFound();
        }

        return Ok(RetourPays.LanguesParlees);
    }
}
