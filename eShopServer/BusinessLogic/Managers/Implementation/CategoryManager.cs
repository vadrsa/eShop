using AutoMapper;
using EntityDTO.Products;
using Facades.Managers;
using Facades.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            using (var scope = serviceProvider.CreateScope())
            {
                return Mapper.Map<List<CategoryDTO>>(
                     scope.ServiceProvider.GetService<ICategoryRepository>().GetAll());
            }
        }

        public async Task<List<CategoryDTO>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            using (var scope = serviceProvider.CreateScope())
            {
                return Mapper.Map<List<CategoryDTO>>(
                     await scope.ServiceProvider.GetService<ICategoryRepository>().GetAllAsync(token));
            }
        }

        public List<CategoryTreeItemDTO> GetTree()
        {
            throw new NotImplementedException();

        }

        public Task<List<CategoryTreeItemDTO>> GetTreeAsync(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
