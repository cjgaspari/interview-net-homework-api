using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeworkAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAPI.Data.EFCore
{
  /// <summary>
  /// This repository handles the bulk of the business logic
  /// In order to return associated notes and attachments, the Get methods have been overriden from the abstract class.
  /// </summary>
  public class AssignmentRepository : HomeworkRespository<Assignment, HomeworkContext>
  {
    private readonly HomeworkContext context; 
    public AssignmentRepository(HomeworkContext context) : base(context)
    {
      this.context = context;
    }

    /// <summary>
    /// Overriding the GetAll method from HomeworkRepository, as an Assignment can include attachments and notes.
    /// Because of this, we need to make sure we include these associated items when we get the list of assignments. 
    /// </summary>
    /// <returns></returns>
    public override Task<List<Assignment>> GetAll()
    {
      return context.Set<Assignment>().Include(x => x.attachments).Include(n => n.notes).ToListAsync();
    }

    /// <summary>
    /// Use this method to get assignments, filter by assignment name, grade, and student name
    /// This method will mainly be used as an example for retreiving student data
    /// </summary>
    /// <param name="assignmentName"></param>
    /// <param name="grade"></param>
    /// <param name="studentName"></param>
    /// <returns></returns>
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments(string assignmentName, string grade, string studentName)
    {
      return await context.Set<Assignment>()
        .Where(sName => sName.studentName == studentName)
        .Where(aName => assignmentName != null ? aName.assignmentName == assignmentName : true)
        .Where(grd => grade != null ? grd.grade == grade : true)
        .Include(x => x.attachments)
        .Include(n => n.notes)
        .ToListAsync();
    }

    /// <summary>
    /// Use this method to get assignments for teachers
    /// Filter by assignment name, grade, student name, min, and max date
    /// </summary>
    /// <param name="assignmentName"></param>
    /// <param name="grade"></param>
    /// <param name="studentName"></param>
    /// <param name="minDate"></param>
    /// <param name="maxDate"></param>
    /// <returns></returns>
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments(string assignmentName, string grade, string studentName, DateTime minDate, DateTime maxDate)
    {
      return await context.Set<Assignment>()
        .Where(sName => studentName != null ? sName.studentName == studentName : true)
        .Where(aName => assignmentName != null ? aName.assignmentName == assignmentName : true)
        .Where(grd => grade != null ? grd.grade == grade : true)
        .Where(dateRange => (minDate != DateTime.Parse("0001-01-01T00:00:00") && maxDate != DateTime.Parse("0001-01-01T00:00:00")) ? (dateRange.submissionTime <= maxDate && dateRange.submissionTime >= minDate) : true)
        .Include(x => x.attachments)
        .Include(n => n.notes)
        .ToListAsync();
    }


    /// <summary>
    /// This method updates an assignments grade
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <param name="grade"></param>
    /// <returns></returns>
    public async Task<Assignment> UpdateGrade(int assignmentId, string grade)
    {
      //Get the existing entity and add the grade, and grading time
      var entity = await base.Get(assignmentId);
      entity.grade = grade;
      entity.gradingTime = DateTime.Now;

      context.Entry(entity).State = EntityState.Modified;
      await context.SaveChangesAsync();
      return entity;
    }

    /// <summary>
    /// This method is to submit an assignment
    /// Student needs to only provide the assignmentName
    /// studentName is passed from login (username) 
    /// </summary>
    /// <param name="assignmentName"></param>
    /// <param name="studentName"></param>
    /// <returns></returns>
    public async Task<ActionResult<Assignment>> SubmitAssignment(string assignmentName, string studentName)
    {
      var entity = new Assignment();
      entity.assignmentName = assignmentName;
      entity.studentName = studentName;
      entity.submissionTime = DateTime.Now;
      entity.grade = "ungraded";


      context.Set<Assignment>().Add(entity);
      await context.SaveChangesAsync();
      return entity;

    }
  }
}
