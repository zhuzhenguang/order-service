using FluentNHibernate.Mapping;

namespace order_service_test.TestDomain
{
    public class TestObject
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class TestObjectMapping : ClassMap<TestObject>
    {
        public TestObjectMapping()
        {
            Table("test_object");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name);
        }
    }
}