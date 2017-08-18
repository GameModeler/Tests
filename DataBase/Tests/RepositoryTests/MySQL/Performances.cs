using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DataBase.Entities.Mapping;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    /// <summary>
    /// Description résumée pour Performances
    /// </summary>
    [TestClass]
    public class Performances
    {

        List<Student> allStudents;

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {


            for (int i = 0; i < 100; i++)
            {
                StudentAddress stuAdd = new StudentAddress();
                stuAdd.Address1 = Faker.Address.StreetAddress();
                stuAdd.City = Faker.Address.City();
                stuAdd.Zipcode = Faker.RandomNumber.Next(1000, 9000);

                string name = Faker.Name.First();

                Student stu = new Student(name, stuAdd);

                allStudents.Add(stu);
                  
            }


        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: ajoutez ici la logique du test
            //
        }
    }
}
