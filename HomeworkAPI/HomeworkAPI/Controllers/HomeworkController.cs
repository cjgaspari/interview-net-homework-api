using System.Collections.Generic;
using System.Threading.Tasks;
using HomeworkAPI.Authorization;
using HomeworkAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAPI.Controllers
{
  /// <summary>
  /// Attachments, and Notes will share the bulk of these methods.
  /// Assignments will mainly override, or create entirely new endpoint methods
  /// Created as an abstract class, with virtual methods, so that it can be overriden, extended, or completely replaced. 
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  /// <typeparam name="TRepository"></typeparam>
  [Route("[controller]")]
  [ApiController]
  public abstract class HomeworkController<TEntity, TRepository> : ControllerBase
    where TEntity : class, IHomeworkEntity
    where TRepository : IHomeworkRepository<TEntity>
  {

    private readonly TRepository repository;

    public HomeworkController(TRepository repository)
    {
      this.repository = repository;
    }

    // GET:
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
    {
      return await repository.GetAll();
    }

    // GET:
    /// <summary>
    /// get a specific entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> Get(int id)
    {
      var homework = await repository.Get(id);
      if (homework == null)
      {
        return NotFound();
      }
      return homework;
    }

    // PUT:
    [HttpPut("teacher/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [TeacherAuthentication]
    public virtual async Task<IActionResult> Put(int id, TEntity homework)
    {
      if (id != homework.id)
      {
        return BadRequest();
      }
      await repository.Update(homework);
      return Ok();
    }

    // POST:
    /// <summary>
    /// post an entity
    /// </summary>
    /// <param name="homework"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Post(TEntity homework)
    {
      try
      {
        await repository.Add(homework);
        return CreatedAtAction("Get", new { id = homework.id }, homework);
      } catch
      {
        return BadRequest("Invalid entity. Please check values.");
      }
      
    }

    // DELETE:
    /// <summary>
    /// delete an entity by id, if you are a teacher
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("teacher/{id}")]
    [TeacherAuthentication]
    public virtual async Task<ActionResult<TEntity>> Delete(int id)
    {
      var homework = await repository.Delete(id);
      if (homework == null)
      {
        return NotFound();
      }
      return homework;
    }
  }
}
