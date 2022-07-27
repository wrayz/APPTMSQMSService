// See https://aka.ms/new-console-template for more information
using ModelLibrary;

namespace BusinessLogic
{
    public class ProductSoftware_BLL : IBusinessLogic<ProductSoftware>
    {
        public Task<int> DeleteListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductSoftware> Get(ProductSoftware condition)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductSoftware>> GetList(ProductSoftware condition)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertListAsync(IEnumerable<ProductSoftware> list)
        {
            throw new NotImplementedException();
        }

        public Task<int> ImportData(ExcelFileInfo excel)
        {
            throw new NotImplementedException();
        }

        public Task<int> MergeListAsync(IEnumerable<ProductSoftware> list)
        {
            throw new NotImplementedException();
        }
    }
}
