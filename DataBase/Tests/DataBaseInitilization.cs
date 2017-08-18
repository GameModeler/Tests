using DataBase.Database;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.Utils;
using System.Collections.Generic;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests
{
    public class DataBaseInitilization
    {

        const string SQLITE_DB_PATH = @"C:\Users\Anne\SQLDatabase\sqlite_test.db";

        private DbManager dbManager = DbManager.Instance;

        private IUniversalContext universalContextMySql;
        private IUniversalContext universalContextSqlLite;

        private MySqlDatabase mySQLDbTest;
        private SqLiteDatabase sqLiteDbTest;

        private  Book book1;
        private  Book book2;
        private  Book book3;
        private  Book book4;

        private static DataBaseInitilization instance = null;
        private static readonly object padlock = new object();

        DataBaseInitilization()
        {
            mySQLDbTest = DatabaseFactory.MySqlDb
                                            .Set
                                            .DatabaseName("base_test")
                                            .Server("localhost")
                                            .UserId("root")
                                            .ToMySqlDatabase;


            universalContextMySql = dbManager.CreateContext(mySQLDbTest);

            sqLiteDbTest = DatabaseFactory.SqLiteDb.Set.DatabaseName("db")
                                                   .DataSource(SQLITE_DB_PATH)
                                                   .ToSqLiteDatabase;

            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");

            bookShelve = new List<Book>();

            bookShelve.Add(book1);
            bookShelve.Add(book2);
            bookShelve.Add(book3);
            bookShelve.Add(book4);
        }

        public static DataBaseInitilization Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataBaseInitilization();
                    }
                    return instance;
                }
            }
        }

        public IUniversalContext UniversalContextMySql
        {
            get {
                return universalContextMySql;
            }
        }

        public IUniversalContext UniversalContextSqlLite
        {
            get { return universalContextSqlLite; }
        }

        public MySqlDatabase MySQLDbTest
        {
            get {

                return mySQLDbTest;
            }
            
        }

        public SqLiteDatabase SQLiteTest
        {
            get
            {

                return sqLiteDbTest;
            }

        }

        /// <summary>
        /// Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public IRepository<T> getNewRepository<T>(ProviderType type) where T : class
        {

            switch (type)
            {
                case ProviderType.MySQL:
                    return dbManager.CreateContext(mySQLDbTest).Entity<T>();

                case ProviderType.SQLite:
                    return dbManager.CreateContext(sqLiteDbTest).Entity<T>();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Get a global Repo (mySQL + sqlite)
        /// </summary>
        public IGlobalRepository<T> getNewGlobalRepository_MySql_Sqlite<T>() where T: class
        {
            var mysqlContext = dbManager.CreateContext(mySQLDbTest);
            var sqliteContext = dbManager.CreateContext(sqLiteDbTest);

            return dbManager.CreateGlobalContext().Add(mysqlContext).Add(sqliteContext).Entity<T>();
        }

        /// <summary>
        /// Get a global Repo (sqlite + mysql)
        /// </summary>
        public IGlobalRepository<T> getNewGlobalRepository_Sqlite_MySql<T>() where T : class
        {
            var mysqlContext = dbManager.CreateContext(mySQLDbTest);
            var sqliteContext = dbManager.CreateContext(sqLiteDbTest);

            return dbManager.CreateGlobalContext().Add(sqliteContext).Add(mysqlContext).Entity<T>();
        }

        public string SQLiteDbPath
        {
            get { return SQLITE_DB_PATH;  }
        }


        private List<Book> bookShelve;

        public List<Book> BookShelve
        {
            get
            {
                return bookShelve;
            }

            set
            {
                bookShelve = value;
            }
        }
    }
}
