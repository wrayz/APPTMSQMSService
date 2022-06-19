using BusinessLogic.ExcelHelper;
using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    public class RobotInfo_BLL
    {
        private RobotInfo_DAO _dao = new RobotInfo_DAO();

        public async Task<int> ImportData(string path, string filename)
        {
            var reader = new TechmanExcelReader();

            var list = reader.GetRobotList(path, filename);

            return await MergeListAsync(list);
        }

        public async Task<RobotInfo> Get()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<RobotInfo>> GetList()
        {
            return await _dao.GetList();
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
