using Microsoft.AspNetCore.Mvc;
using MemcardRex;
using System.Buffers.Text;

namespace psavesync.Controllers;

[ApiController]
[Route("[controller]")]
public class SavesController : ControllerBase
{
    private readonly ILogger<SavesController> _logger;

    public SavesController(ILogger<SavesController> logger) {

        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> Get() {

        return new string[] {};
    }

    [HttpPost]
    public ActionResult<IEnumerable<SaveRecord>> Post([FromBody] string cardBase64Data) {

        var card = new ps1card();

        try {
            card.openMemoryCardStream(Convert.FromBase64String(cardBase64Data), false);
        } catch {
            return BadRequest("Bad card");
        }

        var saveRecords = Enumerable.Range(0, 15)
            .Where(i => card.saveType[i] == 1)
            .Select(SaveRecord.FromCardSlot(card));

        return Ok(saveRecords.ToArray());
    }
}
