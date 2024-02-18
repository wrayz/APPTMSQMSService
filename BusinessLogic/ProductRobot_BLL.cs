using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class ProductRobot_BLL : IBusinessLogic<ProductRobot>
    {
        private readonly ProductRobot_DAO _dao = new();

        private readonly DataAccessContext _db = new();

        public async Task<int> CreateRobotList(IEnumerable<ProductRobot> list)
        {
            await _db.AddRangeAsync(list);
            return _db.SaveChanges();
        }

        /// <summary>
        /// 匯入Excel資料
        /// </summary>
        /// <param name="excel"></param>
        /// <returns></returns>
        public async Task<int> ImportData(ExcelFileInfo excel)
        {
            var reader = new TechmanExcelReader();
            var list = reader.GetRobotList(excel);

            return await CreateRobotList(list);
            //return await MergeListAsync(list);
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
        /// <returns></returns>
        public async Task<IEnumerable<ProductRobot>> GetList()
        {
            return await _dao.GetList();
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
            //一定只出抗靜電
            condition.IsESD = "Yes";
            //不會用Conti需求料號
            condition.UsbforYes = "No";
        }
    }
}
