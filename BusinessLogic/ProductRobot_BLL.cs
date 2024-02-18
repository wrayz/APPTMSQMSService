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
        /// �פJExcel���
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
        /// ���o�зǤ��u�M��
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
        /// ���o���u���
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ProductRobot> Get(ProductRobot condition)
        {
            DefaultCondition(ref condition);
            return await _dao.Get(condition);
        }

        /// <summary>
        /// ���o���u�M����
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductRobot>> GetList()
        {
            return await _dao.GetList();
        }

        /// <summary>
        /// ���o���u�M����
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductRobot>> GetList(ProductRobot condition)
        {
            DefaultCondition(ref condition);
            return await _dao.GetList(condition);
        }

        /// <summary>
        /// �s�W�h�����u���
        /// </summary>
        /// <param name="list">���u�M��</param>
        /// <returns></returns>
        public async Task<int> InsertListAsync(IEnumerable<ProductRobot> list)
        {
            return await _dao.InsertListAsync(list);
        }

        /// <summary>
        /// �X�֦h�����u���
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> MergeListAsync(IEnumerable<ProductRobot> list)
        {
            return await _dao.MergeListAsync(list);
        }

        /// <summary>
        /// �R�����u�M��
        /// </summary>
        /// <returns></returns>
        public async Task<int> DeleteListAsync()
        {
            return await _dao.DeleteListAsync();
        }

        /// <summary>
        /// �w�]����
        /// </summary>
        /// <param name="condition"></param>
        private void DefaultCondition(ref ProductRobot condition)
        {
            //�@�w�u�X���R�q
            condition.IsESD = "Yes";
            //���|��Conti�ݨD�Ƹ�
            condition.UsbforYes = "No";
        }
    }
}
