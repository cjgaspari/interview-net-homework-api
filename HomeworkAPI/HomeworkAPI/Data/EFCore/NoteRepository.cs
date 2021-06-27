using System;
using HomeworkAPI.Data.Models;

namespace HomeworkAPI.Data.EFCore
{
  /// <summary>
  /// Note was made as a seperate entity from assignment to allow teachers to add multiple comments. 
  /// </summary>
  public class NoteRepository : HomeworkRespository<Note, HomeworkContext>
  {
    public NoteRepository(HomeworkContext context) : base(context)
    {
    }
  }
}
