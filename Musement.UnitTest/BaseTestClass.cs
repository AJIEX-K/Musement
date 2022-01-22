using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Musement.UnitTest
{
    /// <summary>
    /// Base class for UnitTests
    /// </summary>
    [TestClass]
    public class BaseTestClass
    {
        protected UnityContainer Container;

        [TestInitialize]
        public virtual void Initialize()
        {
            var configuration = new Configuration.Configuration();
            Container = configuration.Configurate();
        }
    }
}