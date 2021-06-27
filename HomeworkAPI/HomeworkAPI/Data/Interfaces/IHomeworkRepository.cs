using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeworkAPI.Data.Interfaces
{
  /// <summary>
  /// Created an interface, as we will have three entities: Assignments, Notes, and Attachments.
  /// Assignments can have a list of Notes and Attachments.
  /// This interface sets up the general structure for the implementation for these entities. 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IHomeworkRepository<T> where T : class, IHomeworkEntity
  {
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(int id);
  }
}
