using BusinessEntities.Products;
using eShopApi.ResourceAccess.Products;
using eShopApi.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharedEntities.Enums;
using EntityDTO.Products;

namespace eShopApi.BusinessLogic.Products
{
    public class CategoryManager : ManagerBase<Category, CategoryRepository>
    {

        public async Task<IEnumerable<CategoryTreeItemDTO>> GetCategoryTree(int parentID = 0)
        {
            var topLevel = (await Repository.SelectWhereAsync(c => c.ParentID == parentID)).Select(c => new CategoryTreeItemDTO(c.ID, c.Name)).ToList();
            SetTreeChildrenRecursive(topLevel);
            return topLevel;
        }

        private async void SetTreeChildrenRecursive(IEnumerable<CategoryTreeItemDTO> treeItems)
        {
            if (treeItems == null) return;
            foreach (CategoryTreeItemDTO item in treeItems)
            {
                item.Children = (await Repository.SelectWhereAsync(c => c.ParentID == item.ID)).Select(c => new CategoryTreeItemDTO(c.ID, c.Name)).ToList();
                item.ProductCount = (await ProductRepository.Instance.LoadWith(p => p.Category).SelectCountWhereAsync(p => p.CategoryID == item.ID || p.Category.ParentID == item.ID));
                SetTreeChildrenRecursive(item.Children);
            }
        }

        //public IEnumerable<Category> GetTopLevel()
        //{
        //    return Repository.SelectWhere(i => i.ParentID == 0);    
        //}

        //internal IEnumerable<Product> GetCategoryProducts(int id, int limit, int start, ProductOrderBy orderBy = ProductOrderBy.Name, bool ascending = true)
        //{
        //    ProductRepository repo = ProductRepository.Instance.Limit(limit);
        //    if (start > 0) repo = repo.Offset(start);
        //    return repo.LoadWith(p=>p.Category).OrderBy(orderBy.ToString(), ascending).SelectWhere(p => p.CategoryID == id || p.Category.ParentID == id);
        //}

        //internal int GetCategoryProductsCount(int id)
        //{
        //    return ProductRepository.Instance.LoadWith(p => p.Category).SelectCountWhere(p => p.CategoryID == id || p.Category.ParentID == id);
        //}
    }
}
