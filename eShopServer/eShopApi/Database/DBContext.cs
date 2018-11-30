using System.Linq;
using BusinessEntities;
using BusinessEntities.Bars;
using BusinessEntities.Global;
using BusinessEntities.Products;
using LinqToDB;
using LinqToDB.Mapping;
using Microsoft.Extensions.Configuration;

namespace eShopApi
{
    public class DBContext : LinqToDB.Data.DataConnection
    {

        public DBContext() : base("Default") 
        {
            FluentMappingBuilder mb = MappingSchema.GetFluentMappingBuilder();
            MappingSchema.EntityDescriptorCreatedCallback = (mappingSchema, entityDescriptor) =>
            {
                if (entityDescriptor.TypeAccessor.Type.IsSubclassOf(typeof(IDEntityBase)) && !entityDescriptor.TypeAccessor.Type.IsAbstract)
                {
                    var idCol = entityDescriptor.Columns.Where(c => c.MemberName == "ID").FirstOrDefault();
                    if (idCol.MemberName == idCol.ColumnName)
                        idCol.ColumnName = entityDescriptor.TypeAccessor.Type.Name + "ID";
                }
            }; 
        }


        #region Products

        public ITable<HomeSliderItem> HomeSlider => GetTable<HomeSliderItem>();
        public ITable<Category> Categories => GetTable<Category>();
        public ITable<Brand> Brands => GetTable<Brand>();
        public ITable<Product> Products => GetTable<Product>();
        public ITable<Image> Images => GetTable<Image>();

        #endregion

        #region Bars

        public ITable<BrandBarItem> BrandBar => GetTable<BrandBarItem>();
        
        #endregion
    }
}