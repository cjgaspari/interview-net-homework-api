using System.Collections.Generic;
using System.Threading.Tasks;
using HomeworkAPI.Data.EFCore;
using HomeworkAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAPI.Controllers
{
  /// <summary>
  /// The attachment controller mainly inherits from the HomeworkController
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class AttachmentController : HomeworkController<Attachment, AttachmentRepository>
  {
    public AttachmentController(AttachmentRepository repository) : base(repository)
    {
    }

    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<Attachment>>> Get()
    {
      return null;
    }
  }
}
