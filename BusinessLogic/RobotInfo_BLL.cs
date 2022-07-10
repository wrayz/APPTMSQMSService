using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class RobotInfo_BLL
    {
        private RobotInfo_DAO _dao = new RobotInfo_DAO();

        public async Task<int> ImportData(ExcelFileInfo excel)
        {
            var reader = new TechmanExcelReader();

            var list = reader.GetRobotList(excel);

            return await MergeListAsync(list);
        }

        public async Task<RobotInfo> Get(RobotInfo condition)
        {
            condition.OS = "Win7";
            return await _dao.Get(condition);
        }

        public async Task<IEnumerable<RobotInfo>> GetList(RobotInfo condition)
        {
            condition.OS = "Win7";
            return await _dao.GetList(condition);
        }

        public async Task<int> InsertListAsync(IEnumerable<RobotInfo> list)
        {
            return await _dao.InsertListAsync(list);
        }

        public async Task<int> MergeListAsync(IEnumerable<RobotInfo> list)
        {
            return await _dao.MergeListAsync(list);
        }

        public async Task<int> DeleteListAsync()
        {
            return await _dao.DeleteListAsync();
        }
    }
}
