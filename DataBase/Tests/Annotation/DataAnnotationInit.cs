using DataBase.Database;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.Repositories.Interfaces;
using System.Collections.Generic;
using Tests.DataBase.Entities.Annotation;

namespace Tests.DataBase.Tests.Annotation
{
    public class DataAnnotationInit
    {
        const string SQLITE_DB_CARS_PATH = @"C:\Users\Anne\SQLDatabase\sqlite_test_cars.db";
        const string SQLITE_DB_CATS_PATH = @"C:\Users\Anne\SQLDatabase\sqlite_test_cats.db";

        private Car car1;
        private Car car2;
        private Car car3;
        private Car car4;

        public Car Car1
        {
            get { return car1; }
        }
        public Car Car2
        {
            get { return car2; }
        }
        public Car Car3
        {
            get { return car3; }
        }
        public Car Car4
        {
            get { return car4; }
        }

        public Cat Cat1 { get; set; }
        public Cat Cat2 { get; set; }
        public Cat Cat3 { get; set; }
        public Cat Cat4 { get; set; }

        public Satellite Satellite1 { get; set; }
        public Satellite Satellite2 { get; set; }
        public Satellite Satellite3 { get; set; }
        public Satellite Satellite4 { get; set; }

        private MySqlDatabase mySQLDbCats;
        private MySqlDatabase mySQLDbCars;

        private SqLiteDatabase sqLiteDbCats;
        private SqLiteDatabase sqLiteDbCars;

        private DbManager dbManager = DbManager.Instance;

        private IUniversalContext mySqlContextCars;
        private IUniversalContext mySqlContextCats;

        private IUniversalContext sqliteContextCars;
        private IUniversalContext sqliteContextCats;

        public DataAnnotationInit()
        {

            car1 = new Car { Manufacturer = "Nissan", Model = "370Z", Year = 2012 };
            car2 = new Car { Manufacturer = "Ford", Model = "Mustang", Year = 2013 };
            car3 = new Car { Manufacturer = "Chevrolet", Model = "Camaro", Year = 2012 };
            car4 = new Car { Manufacturer = "Dodge", Model = "Charger", Year = 2013 };

            Cat1 = new Cat { Name = "Minou", Color = "Blanc", Year = 2012 };
            Cat2 = new Cat { Name = "Felix", Color = "Noir", Year = 2013 };
            Cat3 = new Cat { Name = "Berlioz", Color = "Gris", Year = 2010 };
            Cat4 = new Cat { Name = "Tigrou", Color = "Tigré", Year = 1999 };

            Satellite1 = new Satellite { Name = "Encelade", Distance = 1272, Rayon= 252 };
            Satellite2 = new Satellite { Name = "Titan", Distance = 1300, Rayon = 2576 };

            mySQLDbCats = DatabaseFactory.MySqlDb
                                            .Set
                                            .DatabaseName("db_Cats")
                                            .Server("localhost")
                                            .UserId("root")
                                            .ToMySqlDatabase;

            mySQLDbCars = DatabaseFactory.MySqlDb
                                           .Set
                                           .DatabaseName("db_Cars")
                                           .Server("localhost")
                                           .UserId("root")
                                           .ToMySqlDatabase;

            sqLiteDbCats = DatabaseFactory.SqLiteDb.Set.DatabaseName("db_Cats")
                                                   .DataSource(SQLITE_DB_CATS_PATH)
                                                   .ToSqLiteDatabase;

            sqLiteDbCars = DatabaseFactory.SqLiteDb
                                           .Set
                                           .DatabaseName("db_Cars")
                                           .DataSource(SQLITE_DB_CARS_PATH)
                                           .ToSqLiteDatabase;

            mySqlContextCars = DatabaseFactory.CreateContext(mySQLDbCars);
            mySqlContextCats = DatabaseFactory.CreateContext(mySQLDbCats);
            sqliteContextCars = DatabaseFactory.CreateContext(sqLiteDbCars);
            sqliteContextCats = DatabaseFactory.CreateContext(sqLiteDbCats);



        }

        /// <summary>
        /// Get a new Book Shelve
        /// </summary>
        public List<Car> Parking
        {
            get
            {
                List<Car> parking = new List<Car>();

                parking.Add(car1);
                parking.Add(car2);
                parking.Add(car3);
                parking.Add(car4);

                return parking;
            }
        }

        /// <summary>
        /// Get a new Book Shelve
        /// </summary>
        public List<Cat> CatsHouse
        {
            get
            {
                List<Cat> catHouse = new List<Cat>();

                catHouse.Add(Cat1);
                catHouse.Add(Cat2);
                catHouse.Add(Cat3);
                catHouse.Add(Cat4);

                return catHouse;
            }
        }

        /// <summary>
        /// Get a new Book Shelve
        /// </summary>
        public List<Satellite> Satellite
        {
            get
            {
                List<Satellite> satelitte = new List<Satellite>();

                satelitte.Add(Satellite1);
                satelitte.Add(Satellite1);

                return satelitte;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MySqlDatabase MySqlDbCats
        {
            get
            {
                return mySQLDbCats;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MySqlDatabase MysqlDbCars
        {
            get
            {
                return mySQLDbCars;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqLiteDatabase SqliteDbCars
        {
            get
            {
                return sqLiteDbCars;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqLiteDatabase SqliteDbCats
        {
            get
            {
                return sqLiteDbCats;
            }
        }

        /// <summary>
        /// Get a global Repo (mySQL + sqlite)
        /// </summary>
        public IGlobalRepository<T> getNewGlobalRepository_Cars<T>() where T : class
        {
            mySqlContextCars = dbManager.CreateContext(mySQLDbCars);
            //sqliteContextCars = dbManager.CreateContext(sqliteDbCars);

            return dbManager.CreateGlobalContext().Add(mySqlContextCars).Entity<T>();
                                                  //.Add(sqliteContextCars).Entity<T>();
        }

        /// <summary>
        /// Get a global Repo (sqlite + mysql)
        /// </summary>
        public IGlobalRepository<T> getNewGlobalRepository_Cats<T>() where T : class
        {
            mySqlContextCats = dbManager.CreateContext(mySQLDbCats);
            //sqliteContextCats = dbManager.CreateContext(sqLiteDbCats);

            return dbManager.CreateGlobalContext().Add(mySqlContextCats).Entity<T>();
                                                  // .Add(sqliteContextCats).Entity<T>();
        }

        public IUniversalContext MySqlContextCars
        {
            get { return mySqlContextCars; }
        }

        public IUniversalContext SqliteContextCars
        {
            get { return sqliteContextCars; }
        }

        public IUniversalContext MySqlContextCats
        {
            get { return mySqlContextCats; }
        }

        public IUniversalContext SqliteContextCats
        {
            get { return sqliteContextCats; }
        }


    }
}
