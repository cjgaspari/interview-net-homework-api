using System;
using System.Collections.Generic;
using HomeworkAPI.Data.Interfaces;

namespace HomeworkAPI.Data.Models
{
  public class Assignment : IHomeworkEntity
  {
    public int id { get; set; }
    public string assignmentName { get; set; }
    public string studentName { get; set; }
    public DateTime submissionTime { get; set; }
    public DateTime gradingTime { get; set; }
    public List<Attachment> attachments { get; } = new List<Attachment>();
    public string grade { get; set; }
    public List<Note> notes { get; } = new List<Note>();
  }
}
