using System;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAPI.Authorization
{

  /// <summary>
  /// Creating an attribute for API authentication
  /// This will be for Authorized Student access
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class BasicAuthenticationAttribute : TypeFilterAttribute
  {
    public BasicAuthenticationAttribute(string realm = @"HomeworkAPI") : base(typeof(BasicAuthenticationFilter))
    {
      Arguments = new object[] { realm };
    }

    
  }

  /// <summary>
  /// This attribute will be for Teacher access
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class TeacherAuthenticationAttribute : TypeFilterAttribute
  {
    public TeacherAuthenticationAttribute(string realm = @"AdminHomeworkAPI") : base(typeof(BasicAuthenticationFilter))
    {
      Arguments = new object[] { realm };
    }
  }
}



