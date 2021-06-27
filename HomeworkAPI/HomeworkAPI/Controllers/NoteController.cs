using System.Collections.Generic;
using System.Threading.Tasks;
using HomeworkAPI.Authorization;
using HomeworkAPI.Data.EFCore;
using HomeworkAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAPI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class NoteController : HomeworkController<Note, NoteRepository>
  {
    public NoteController(NoteRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// allows a teacher the ability to add a note to an assignment
    /// </summary>
    /// <param name="homework"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("teacher/addNote")]
    [TeacherAuthentication]
    public override Task<ActionResult<Note>> Post(Note homework)
    {
      return base.Post(homework);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public override Task<ActionResult<IEnumerable<Note>>> Get()
    {
      return null;
    }
  }
}
