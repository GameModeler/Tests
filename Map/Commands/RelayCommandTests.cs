using System;
using System.Windows.Input;
using Map.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Map.Commands
{
    [TestClass]
    public class RelayCommandTests
    {
        public ICommand TestCommand;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RelayCommandExceptionTest()
        {
            Assert.IsNull(TestCommand);
            TestCommand = new RelayCommand(null, null);
        }

        public void RelayCommandTest()
        {
            
        }

        public void 
    }
}