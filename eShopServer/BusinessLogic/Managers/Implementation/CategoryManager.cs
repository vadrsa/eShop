using AutoMapper;
using BusinessEntities.Products;
using EntityDTO.Products;
using Facades.Managers;
using Facades.Repository;
using Microsoft.Extensions.DependencyInjection;
using ModelChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Transactions;

namespace Managers.Implementation
{
    public class CategoryManager : ICategoryManager
    {
        
        private IServiceProvider serviceProvider;

        public CategoryManager(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        public List<CategoryDTO> GetAll()
        {
            return Mapper.Map<List<CategoryDTO>>(
                    serviceProvider.GetService<ICategoryRepository>().GetAll());
        }

        public async Task<List<CategoryDTO>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<CategoryDTO>>(
                    await serviceProvider.GetService<ICategoryRepository>().GetAllAsync(token));
        }

        public List<CategoryTreeItemDTO> GetTree()
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryTreeItemDTO>> GetTreeAsync(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        private async Task InsertHierarchical(IEnumerable<CategoryDTO> categories)
        {
            foreach (var item in categories.Where(_ => _.ParentID >= 0).ToList())
            {
                await InsertHierarchicalItem(item, categories);
            }
        }

        private async Task InsertHierarchicalItem(CategoryDTO category, IEnumerable<CategoryDTO> categories)
        {
            int id = (await serviceProvider.GetService<ICategoryRepository>().InsertAsync(Mapper.Map<Category>(category))).ID;
            foreach (var child in categories.Where(_ => _.ParentID == category.ID).ToList())
            {
                child.ParentID = id;
                await InsertHierarchicalItem(child, categories);
            }
            category.ID = 0;

        }

        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task<bool> SaveEditableListAsync(ITrackableCollection<CategoryDTO> trackableCollection, CancellationToken token = new CancellationToken())
        {
            await InsertHierarchical(trackableCollection.New.ToList());
            foreach (var item in trackableCollection.Deleted)
            {
                await serviceProvider.GetService<ICategoryRepository>().RemoveAsync(Mapper.Map<Category>(item));
            }
            foreach (var item in trackableCollection.Changed)
            {
                await serviceProvider.GetService<ICategoryRepository>().UpdateAsync(Mapper.Map<Category>(item));
            }
            return true;
        }
    }
}
