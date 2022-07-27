using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class ProductRobot_BLL : IBusinessLogic<ProductRobot>
    {
        private readonly ProductRobot_DAO _dao = new ProductRobot_DAO();

        public async Task<int> ImportData(ExcelFileInfo excel)
        {
            var reader = new TechmanExcelReader();

            var list = reader.GetRobotList(excel);

            return await MergeListAsync(list);
        }

        public async Task<ProductRobot> Get(ProductRobot condition)
        {
            DefaultCondition(ref condition);
            return await _dao.Get(condition);
        }

        public async Task<IEnumerable<ProductRobot>> GetList(ProductRobot condition)
        {
            DefaultCondition(ref condition);
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

        private void DefaultCondition(ref ProductRobot condition)
        {
            //不再出Win7版, DAO層邏輯是Not
            condition.OS = "Win7";
            //只出TRI相機
            condition.HardwardVersion = "3.2A";
            //一定只出抗靜電
            condition.IsESD = "Yes";
            //不會用Conti需求料號
            condition.UsbforYes = "No";
            //一般不會查Palletizing用手臂料號
            condition.PalletProtect = "No";
        }
    }
}
