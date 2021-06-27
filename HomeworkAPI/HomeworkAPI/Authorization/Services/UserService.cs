namespace HomeworkAPI.Authorization.Services
{
  public class UserService : IUserService
  {
    public UserService()
    {
    }

    /// <summary>
    /// This is just a stub service that will always return true if a username and password are provided
    /// In some cases, "teacher" or "admin" can be used to test certain features
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool IsValidUser(string userName, string password, string realm)
    {
      if (string.IsNullOrWhiteSpace(userName))
      {
        return false;
      }

      if (string.IsNullOrWhiteSpace(password))
      {
        return false;
      }

      if(!realm.Equals("AdminHomeworkAPI"))
      {
        //If we are not in the AdminHomeworkAPI realm, we can consider this user valid.
        //This would be a "student" 
        return true;
      }

      //If we are in the "AdminHomeworkAPI" realm, and the username is either "admin" or "teacher" we will consider them valid
      //This is to mock up authentication and roles of a user
      if(userName.Equals("admin") || userName.Equals("teacher"))
      {
        return true;
      }
      else
      {
        return false; 
      }
    }
  }
}
