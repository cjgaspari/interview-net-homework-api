using System;
namespace HomeworkAPI.Authorization.Services
{
  public interface IUserService
  {
    bool IsValidUser(string userName, string password, string realm);
  }
}
