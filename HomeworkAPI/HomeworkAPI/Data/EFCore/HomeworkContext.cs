using System;
using System.IO;
using System.Reflection;
using HomeworkAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAPI.Data.EFCore
{
  /// <summary>
  /// DbContext to create the EntityFramework Core SQLite database
  /// </summary>
  public class HomeworkContext : DbContext
  {
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Note> Notes { get; set; }

    //Get the Assembly Name for creation of SQLite db
    static string dbFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.db";

    //Use Base Directory for DB path
    static string dbPath = Path.Combine(AppContext.BaseDirectory, dbFile);

    //This is used to setup our SQLite database, using the path created from above
    protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite($@"Data Source={dbPath}");

  }
}
