using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeworkAPI.Authorization;
using HomeworkAPI.Data.EFCore;
using HomeworkAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;


namespace HomeworkAPI.Controllers
{
  /// <summary>
  /// Handles the API endpoints to get the list of assignments, update grades if you are a teacher, and upload assignments as a student. 
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class AssignmentController : HomeworkController<Assignment, AssignmentRepository>
  {
    private readonly AssignmentRepository repository;
    public AssignmentController(AssignmentRepository repository) : base(repository)
    {
      this.repository = repository;
    }

    /// <summary>
    /// allows a student to submit an assignment
    /// </summary>
    /// <param name="homework"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("student/submit")]
    [BasicAuthentication]
    public Task<ActionResult<Assignment>> Post(string assignmentName)
    {
      //Automatically populate assignment name with the student username
      //This was done to simplify the parameters that are passed
      return repository.SubmitAssignment(assignmentName, User.Identity.Name);
    }

    /// <summary>
    /// allows a teacher to delete an assignment submission
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("teacher/{assignmentId}")]
    [TeacherAuthentication]
    public override Task<ActionResult<Assignment>> Delete(int assignmentId)
    {
      return base.Delete(assignmentId);
    }

    /// <summary>
    /// allows a student to get their assignments
    /// It also allows for filtering by Assignment Name, and Grade
    /// </summary>
    /// <returns></returns>    
    [HttpGet]
    [Route("student/getAssignments")]
    [BasicAuthentication]
    public Task<ActionResult<IEnumerable<Assignment>>> GetStudentAssignments(string assignmentName, string grade)
    {
      //When getting assignments for a student, it is filtered by student username "User.Identity.Name" so that they only see their assignments
      return repository.GetAssignments(assignmentName, grade, User.Identity.Name);
    }

    /// <summary>
    /// allows a teacher to get all assignments
    /// It also allows for filtering by Assignment Name, Grade, Student Name, and Date range
    /// </summary>
    /// <returns></returns> 
    [HttpGet]
    [Route("teacher/getAssignments")]
    [TeacherAuthentication]
    public Task<ActionResult<IEnumerable<Assignment>>> GetAssignments(string assignmentName, string grade, string studentName, DateTime minDate, DateTime maxDate)
    {
      return repository.GetAssignments(assignmentName, grade, studentName, minDate, maxDate);
    }

    /// <summary>
    /// allows a teacher to update an assignments grade
    /// </summary>
    /// <param name="id"></param>
    /// <param name="homework"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("teacher/updateGrade")]
    [TeacherAuthentication]
    public async Task<ActionResult<Assignment>> UpdateGrade(int assignmentId, string grade)
    {
      try
      {
        await repository.UpdateGrade(assignmentId, grade);
        return Ok($"Assignment ID: {assignmentId} has been updated with a grade of: {grade}");
      }
      catch
      {
        return BadRequest("Invalid Assignment ID or grade. Please try again.");
      }

    }

    /// <summary>
    /// The following code can be ignored.
    /// These are endpoints that we are overriding, and returning null for this demo project
    /// </summary>
    /// <returns></returns>
    #region Overriden Endpoints that return nothing.
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<Assignment>>> Get()
    {
      return null;
    }

    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<Assignment>> Get(int id)
    {
      return null;
    }

    [HttpPost]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<Assignment>> Post(Assignment homework)
    {
      return null;
    }

    [HttpPut]
    [Route("teacher/updateAssignmentDetails")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [BasicAuthentication]
    public override Task<IActionResult> Put(int id, Assignment homework)
    {
      return null;
      //return base.Put(id, homework);
    }
    #endregion
  }
}
