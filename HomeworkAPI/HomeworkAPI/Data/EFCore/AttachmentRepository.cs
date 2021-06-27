using System;
using HomeworkAPI.Data.Models;

namespace HomeworkAPI.Data.EFCore
{
  /// <summary>
  /// Students can add multiple attachments to their assignment
  /// For now, this class will inherit from HomeworkRepository 
  /// </summary>
  public class AttachmentRepository : HomeworkRespository<Attachment, HomeworkContext>
  {
    public AttachmentRepository(HomeworkContext context) : base(context)
    {
    }
  }
}
