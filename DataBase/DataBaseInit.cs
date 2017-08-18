using DataBase.Database;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.Repositories.Interfaces;
using System.Collections.Generic;
using Tests.DataBase.Entities;

namespace Tests.DataBase
{
    public class DataBaseInit
    {
        const string SQLITE_DB_PATH = @"C:\Users\Anne\SQLDatabase\sqlite_test.db";

        private Book book1;
        private Book book2;
        private Book book3;
        private Book book4;


        public Book Book1
        {
            get { return book1; }
        }
        public Book Book2
        {
            get { return book2; }
        }
        public Book Book3
        {
            get { return book3; }
        }
        public Book Book4
        {
            get { return book4; }
        }


        private MySqlDatabase mySQLDbTest;
        private SqLiteDatabase sqLiteDbTest;

        private DbManager dbManager = DbManager.Instance;

        private IUniversalContext mySqlContext;
        private IUniversalContext sqliteContext;

        public DataBaseInit()
        {
            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");

            mySQLDbTest = DatabaseFactory.MySqlDb
                                            .Set
                                            .DatabaseName("base_test")
                                            .Server("localhost")
                                            .UserId("root")
                                            .ToMySqlDatabase;


            sqLiteDbTest = DatabaseFactory.SqLiteDb.Set.DatabaseName("db")
                                                   .DataSource(SQLITE_DB_PATH)
                                                   .ToSqLiteDatabase;

        }

        /// <summary>
        /// Get a new Book Shelve
        /// </summary>
        public List<Book> BookShelve
        {
            get
            {
                List<Book> bookShelve = bookShelve = new List<Book>();

                bookShelve.Add(book1);
                bookShelve.Add(book2);
                bookShelve.Add(book3);
                bookShelve.Add(book4);

                return bookShelve;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MySqlDatabase mySqlDbSettings
        {
            get
            {
                return mySQLDbTest;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqLiteDatabase sqliteDbSettings
        {
            get
            {
                return sqLiteDbTest;
            }
        }

        /// <summary>
        /// Get a global Repo (mySQL + sqlite)
        /// </summary>
        public IGlobalRepository<T> getNewGlobalRepository_MySql_Sqlite<T>() where T : class
        {
            mySqlContext = dbManager.CreateContext(mySQLDbTest);
            sqliteContext = dbManager.CreateContext(sqLiteDbTest);

            return dbManager.CreateGlobalContext().Add(mySqlContext).Add(sqliteContext).Entity<T>();
        }

        /// <summary>
        /// Get a global Repo (sqlite + mysql)
        /// </summary>
        public IGlobalRepository<T> getNewGlobalRepository_Sqlite_MySql<T>() where T : class
        {
            mySqlContext = dbManager.CreateContext(mySQLDbTest);
            sqliteContext = dbManager.CreateContext(sqLiteDbTest);

            return dbManager.CreateGlobalContext().Add(sqliteContext).Add(mySqlContext).Entity<T>();
        }

        /// <summary>
        /// Get a repo with MySQL context
        /// </summary>
        public IRepository<T> getRepositoryMySql<T>() where T : class
        {
            mySqlContext = dbManager.CreateContext(mySQLDbTest);    
            return mySqlContext.Entity<T>();
        }

        /// <summary>
        /// Get a repo with SQLite context
        /// </summary>
        public IRepository<T> getRepositorySqlite<T>() where T : class
        {         
            sqliteContext = dbManager.CreateContext(sqLiteDbTest);
            return sqliteContext.Entity<T>();
        }

        public IUniversalContext MySqlContext
        {
            get { return mySqlContext; }
        }

        public IUniversalContext SqliteContext
        {
            get { return sqliteContext; }
        }


    }
}
