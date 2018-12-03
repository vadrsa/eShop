using BusinessEntities.Products;
using Facades.Repository;
using LinqToDB;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class CategoryRepository : RepositoryBase<Category, ICategoryRepository> , ICategoryRepository
    {
        protected override Expression<Func<DBContext, ITable<Category>>> TableExpression => c => c.Categories;

    }
}
