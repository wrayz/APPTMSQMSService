using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class ProductRobot_BLL
    {
        private ProductRobot_DAO _dao = new ProductRobot_DAO();

        public async Task<int> ImportData(ExcelFileInfo excel)
        {
            var reader = new TechmanExcelReader();

            var list = reader.GetRobotList(excel);

            return await MergeListAsync(list);
        }

        public async Task<ProductRobot> Get(ProductRobot condition)
        {
            condition.OS = "Win7";
            return await _dao.Get(condition);
        }

        public async Task<IEnumerable<ProductRobot>> GetList(ProductRobot condition)
        {
            condition.OS = "Win7";
            return await _dao.GetList(condition);
        }

        public async Task<int> InsertListAsync(IEnumerable<ProductRobot> list)
        {
            return await _dao.InsertListAsync(list);
        }

        public async Task<int> MergeListAsync(IEnumerable<ProductRobot> list)
        {
            return await _dao.MergeListAsync(list);
        }

        public async Task<int> DeleteListAsync()
        {
            return await _dao.DeleteListAsync();
        }
    }
}
