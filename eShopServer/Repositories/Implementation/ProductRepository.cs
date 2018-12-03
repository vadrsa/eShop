using BusinessEntities.Products;
using Facades.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Implementation
{
    public class ProductRepository : RepositoryBase<Product, IProductRepository>, IProductRepository
    {
        protected override Expression<Func<DBContext, ITable<Product>>> TableExpression => c => c.Products;
        
    }
}
