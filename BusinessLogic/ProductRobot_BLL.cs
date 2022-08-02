using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class ProductRobot_BLL : IBusinessLogic<ProductRobot>
    {
        private readonly ProductRobot_DAO _dao = new ProductRobot_DAO();

        /// <summary>
        /// 匯入Excel資料
        /// </summary>
        /// <param name="excel"></param>
        /// <returns></returns>
        public async Task<int> ImportData(ExcelFileInfo excel)
        {
            var reader = new TechmanExcelReader();
            var list = reader.GetRobotList(excel);

            return await MergeListAsync(list);
        }

        /// <summary>
        /// 取得標準手臂清單
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductRobot>> GetStandardRobotList(ProductRobot condition)
        {
            condition.Vision = "Yes";
            condition.IsAgv = "No";
            condition.ComplexCable = "3M";
            condition.IsSemi = "No";
            condition.HasCommunication = "No";
            return await GetList(condition);
        }

        /// <summary>
        /// 取得手臂資料
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ProductRobot> Get(ProductRobot condition)
        {
            DefaultCondition(ref condition);
            return await _dao.Get(condition);
        }

        /// <summary>
        /// 取得手臂清單資料
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductRobot>> GetList(ProductRobot condition)
        {
            DefaultCondition(ref condition);
            return await _dao.GetList(condition);
        }

        /// <summary>
        /// 新增多筆手臂資料
        /// </summary>
        /// <param name="list">手臂清單</param>
        /// <returns></returns>
        public async Task<int> InsertListAsync(IEnumerable<ProductRobot> list)
        {
            return await _dao.InsertListAsync(list);
        }

        /// <summary>
        /// 合併多筆手臂資料
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> MergeListAsync(IEnumerable<ProductRobot> list)
        {
            return await _dao.MergeListAsync(list);
        }

        /// <summary>
        /// 刪除手臂清單
        /// </summary>
        /// <returns></returns>
        public async Task<int> DeleteListAsync()
        {
            return await _dao.DeleteListAsync();
        }

        /// <summary>
        /// 預設條件
        /// </summary>
        /// <param name="condition"></param>
        private void DefaultCondition(ref ProductRobot condition)
        {
            //台灣都用US
            condition.PlugType = "US";
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
