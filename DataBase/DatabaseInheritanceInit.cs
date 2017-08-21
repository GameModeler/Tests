using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities.Inheritance;

namespace Tests.DataBase
{
    public class DatabaseInheritanceInit
    {

        const string SQLITE_DB_PATH = @"C:\Users\Anne\SQLDatabase\inherit_test.db";

        private MySqlDatabase mySQLDbTest;
        private SqLiteDatabase sqLiteDbTest;


        public List<B> AllBs { get; set; }
        public List<C> AllCs { get; set; }
        public List<D> AllDs { get; set; }
        public E TheE { get; set; }
        public F TheF { get; set; }

        public MySqlDatabase MySQLDbTest
        {
            get { return mySQLDbTest; }
        }

        public SqLiteDatabase SqLiteDbTest
        {
            get { return sqLiteDbTest; }
        }

        public DatabaseInheritanceInit()
        {

            // Create B object

            B B1 = new B("B1", "A.B1", 1);
            B B2 = new B("B2", "A.B2", 2);

            List<B> Bs = new List<B>();
            Bs.Add(B1);
            Bs.Add(B2);

            C C1 = new C("C1", "A.C1", 3);
            C C2 = new C("C2", "A.C2", 4);
            C C3 = new C("C3", "A.C3", 5);

            List<C> Cs = new List<C>();
            Cs.Add(C1);
            Cs.Add(C2);
            Cs.Add(C3);

            List<C> Cs2 = new List<C>();
            Cs2.Add(C1);
            Cs2.Add(C3);

            D D1 = new D("D1", "B.D1", "A.D1", 7);
            D D2 = new D("D2", "B.2", "A.D2", 8);
            D D3 = new D("D3", "B.3", "A.D3", 9);

            List<A> As = new List<A>();
            As.Add(B1);
            As.Add(B2);
            As.Add(C1);

            D1.As = As;
            D2.As = As;

            D1.Cs = Cs;
            D2.Cs = Cs2;

            List<D> Ds = new List<D>();
            Ds.Add(D1);
            Ds.Add(D2);
            Ds.Add(D3);

            C1.Ds = Ds;
            C2.Ds = Ds;
            C3.Ds = Ds;

            E E1 = new E("E1", "B.E1", "A.E1", 6);
            E1.Bs = Bs;

            B1.e = E1;
            B2.e = E1;

            F F1 = new F("F1");
            F1.e = E1;

            E1.f = F1;

            AllBs = Bs;
            AllCs = Cs;
            AllDs = Ds;
            TheE = E1;
            TheF = F1;
            
            mySQLDbTest = DatabaseFactory.MySqlDb
                                        .Set
                                        .DatabaseName("inherit_test")
                                        .Server("localhost")
                                        .UserId("root")
                                        .ToMySqlDatabase;


            sqLiteDbTest = DatabaseFactory.SqLiteDb.Set.DatabaseName("inherit_test")
                                                   .DataSource(SQLITE_DB_PATH)
                                                   .ToSqLiteDatabase;
        }
    }
}

