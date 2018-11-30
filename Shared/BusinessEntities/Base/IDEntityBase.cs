using eShop.BusinessEntities.Base;
using LinqToDB.Mapping;

namespace BusinessEntities
{
    public abstract class IDEntityBase : IIdEntityBase
    {
        [PrimaryKey, Identity]
        public int ID { get;set; }
    }
}