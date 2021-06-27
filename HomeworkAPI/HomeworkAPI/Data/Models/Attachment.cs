using System;
using System.Text.Json.Serialization;
using HomeworkAPI.Data.Interfaces;

namespace HomeworkAPI.Data.Models
{
  public class Attachment : IHomeworkEntity
  {
    public int id { get; set; }
    public int assignmentId { get; set; }
    [JsonIgnore]
    public Assignment assignment { get; set; }
    public string attachmentUrl { get; set; }

  }
}
