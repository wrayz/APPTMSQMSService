// See https://aka.ms/new-console-template for more information
using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class ProductSoftware_BLL : IBusinessLogic<ProductSoftware>
    {
        private readonly ProductSoftware_DAO _dao = new ProductSoftware_DAO();

        public async Task<int> ImportData(ExcelFileInfo excel)
        {
            var reader = new TechmanExcelHelper();
            var list = reader.GetSoftwareList(excel);

            return await MergeListAsync(list);
        }

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

        public async Task<int> MergeListAsync(IEnumerable<ProductSoftware> list)
        {
            return await _dao.MergeListAsync(list);
        }
    }
}
