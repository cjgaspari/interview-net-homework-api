using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeworkAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace HomeworkAPI.Data.EFCore
{
  /// <summary>
  /// This abstract class sets up virtual methods with base functionality for all the repositories to use
  /// These methods can be overriden to extend, or modify as needed
  /// Notes and assignments will mainly inherit, whereas Assignments will override and add additional functionality
  /// </summary>
  public abstract class HomeworkRespository<TEntity, TContext> : IHomeworkRepository<TEntity>
    where TEntity : class, IHomeworkEntity
    where TContext : DbContext
  {
    private readonly TContext context;
    public HomeworkRespository(TContext context)
    {
      this.context = context;
    }

    /// <summary>
    /// Virtual method for Adding entity.
    /// Can be used as is, or modified if needed.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> Add(TEntity entity)
    {
      context.Set<TEntity>().Add(entity);
      await context.SaveChangesAsync();
      return entity;
    }

    /// <summary>
    /// Virtual method for Deleting entity.
    /// This will be used for attachment and note endpoints. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> Delete(int id)
    {
      var entity = await context.Set<TEntity>().FindAsync(id);
      if (entity == null)
      {
        return entity;
      }

      context.Set<TEntity>().Remove(entity);
      await context.SaveChangesAsync();

      return entity;
    }

    /// <summary>
    /// Virtual method for Getting entity.
    /// This will be used by attachment and note endpoints. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> Get(int id)
    {
      return await context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Virtual method for Getting all entities. 
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetAll()
    {
      return await context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Virtual method for updating entity. 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> Update(TEntity entity)
    {
      context.Entry(entity).State = EntityState.Modified;
      await context.SaveChangesAsync();
      return entity;
    }
  }
}
