using System.Collections.Generic;
using System.Linq;
using NHibernate;
using order_service_test.TestDomain;
using Xunit;

namespace order_service_test
{
    public class TestObjectFacts : FactBase
    {
        [Fact]
        public void should_return_test_object()
        {
            var testObject = new TestObject
            {
                Name = "test object"
            };

            using (ISession session = ResolveSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(testObject);
                transaction.Commit();
            }

            List<TestObject> testObjects = ResolveSession().Query<TestObject>().ToList();
            Assert.Single(testObjects);
            Assert.Equal("test object", testObjects[0].Name);
        }
    }
}