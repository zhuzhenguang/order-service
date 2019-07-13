using FluentNHibernate.Mapping;
using order_service.Domains;

namespace order_service.Infrastructures.Mappings
{
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