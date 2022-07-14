using Dapper;
using Microsoft.Data.Sqlite;
using ModelLibrary;
using DataAccess.Generic;
using System.Data;

namespace DataAccess
{
    public class RobotInfo_DAO
    {
        private readonly string _ds = "Data Source=TMSQMS.db";

        public async Task<RobotInfo> Get(RobotInfo condition)
        {
            string sql = GenerateQuerySql(condition);
            var parameters = GenerateParameters(condition);

            using (var connection = new SqliteConnection(_ds))
            {
                return await connection.QueryFirstOrDefaultAsync<RobotInfo>(sql, parameters);
            }
        }

        public async Task<IEnumerable<RobotInfo>> GetList(RobotInfo condition)
        {
            string sql = GenerateQuerySql(condition);
            var parameters = GenerateParameters(condition);

            using (var connection = new SqliteConnection(_ds))
            {
                return await connection.QueryAsync<RobotInfo>(sql, parameters);
            }
        }

        public async Task<int> InsertListAsync(IEnumerable<RobotInfo> list)
        {
            var parameters = GenerateParameterList(list);
            var sql =
            @"
                INSERT INTO Products VALUES
                (@No, @PartNumber, @Description, @ProductName, @ModelName, @Length,
                @Vision, @PlugType, @IsAgv, @OS, @HardwardVersion, @HMI, @IsSemi,
                @ComplexCable, @IsESD, @PalletProtect, @UsbforYes, @HasCommunication, @Customer)
            ";

            return await ExecuteListAsync(sql, parameters);
        }

        public async Task<int> MergeListAsync(IEnumerable<RobotInfo> list)
        {
            var parameters = GenerateParameterList(list);
            var sql =
            @"
                INSERT OR REPLACE INTO Products (
                    No, PartNumber, Description, ProductName, ModelName, Length,
                    Vision, PlugType, IsAgv, OS, HardwardVersion, HMI, IsSemi,
                    ComplexCable, IsESD, PalletProtect, UsbforYes, HasCommunication, Customer
                ) VALUES (
                    @No, @PartNumber, @Description, @ProductName, @ModelName, @Length,
                    @Vision, @PlugType, @IsAgv, @OS, @HardwardVersion, @HMI, @IsSemi,
                    @ComplexCable, @IsESD, @PalletProtect, @UsbforYes, @HasCommunication, @Customer
                )
            ";

            return await ExecuteListAsync(sql, parameters);
        }

        public async Task<int> DeleteListAsync()
        {
            string sql =
            @"
                DELETE FROM Products
            ";
            return await ExecuteListAsync(sql);
        }

        private async Task<int> ExecuteListAsync(string sql, List<DynamicParameters>? parameters = null)
        {
            int count = 0;

            using (var connection = new SqliteConnection(_ds))
            {
                connection.Open();

                // Transaction
                using (var tran = connection.BeginTransaction())
                {
                    count += await connection.ExecuteAsync(sql, parameters);
                    tran.Commit();
                }
            }

            return count;
        }

        private string GenerateQuerySql(RobotInfo condition)
        {
            return $@"
                SELECT PartNumber, Description, ProductName, ModelName, Length,
                        Vision, PlugType, IsAgv, OS, HardwardVersion, HMI, IsSemi,
                        ComplexCable, IsESD, PalletProtect, UsbforYes, HasCommunication, Customer
                FROM Products
                WHERE OS != @OS
                    {"AND ModelName = @ModelName".If(!string.IsNullOrEmpty(condition.ModelName))}
                    {"AND Vision = @Vision".If(!string.IsNullOrEmpty(condition.Vision))}
                    {"AND PlugType = @PlugType".If(!string.IsNullOrEmpty(condition.PlugType))}
                    {"AND IsAgv = @IsAgv".If(!string.IsNullOrEmpty(condition.IsAgv))}
                    {"AND HMI = @HMI".If(!string.IsNullOrEmpty(condition.HMI))}
                    {"AND IsSemi = @IsSemi".If(!string.IsNullOrEmpty(condition.IsSemi))}
                    {"AND ComplexCable = @ComplexCable".If(!string.IsNullOrEmpty(condition.ComplexCable))}
                    {"AND IsESD = @IsESD".If(!string.IsNullOrEmpty(condition.IsESD))}
                    {"AND PalletProtect = @PalletProtect".If(!string.IsNullOrEmpty(condition.PalletProtect))}
                    {"AND UsbforYes = @UsbforYes".If(!string.IsNullOrEmpty(condition.UsbforYes))}
                    {"AND HasCommunication = @HasCommunication".If(!string.IsNullOrEmpty(condition.HasCommunication))}
                    {"AND Customer LIKE @Customer".If(!string.IsNullOrEmpty(condition.Customer))}
            ";
        }

        private DynamicParameters GenerateParameters(RobotInfo condition)
        {
            // 一般單傳物件也可以，但是效能不好
            // DynamicParameters可以指定型別使用, 效果接近ADO.NET的Parameters
            var parameters = new DynamicParameters();
            // parameters.Add("No", condition.No, DbType.Int32);
            parameters.Add("PartNumber", condition.PartNumber, DbType.String);
            parameters.Add("Description", condition.Description, DbType.String);
            parameters.Add("ProductName", condition.ProductName, DbType.String);
            // parameters.Add("ModelName", condition.ModelName, DbType.String);
            parameters.Add("Length", condition.Length, DbType.Int32);
            parameters.Add("Vision", condition.Vision, DbType.String);
            parameters.Add("PlugType", condition.PlugType, DbType.String);
            parameters.Add("IsAgv", condition.IsAgv, DbType.String);
            parameters.Add("OS", condition.OS, DbType.String);
            parameters.Add("HardwardVersion", condition.HardwardVersion, DbType.String);
            parameters.Add("HMI", condition.HMI, DbType.String);
            parameters.Add("IsSemi", condition.IsSemi, DbType.String);
            parameters.Add("ComplexCable", condition.ComplexCable, DbType.String);
            parameters.Add("IsESD", condition.IsESD, DbType.String);
            parameters.Add("PalletProtect", condition.PalletProtect, DbType.String);
            parameters.Add("UsbforYes", condition.UsbforYes, DbType.String);
            parameters.Add("HasCommunication", condition.HasCommunication, DbType.String);
            parameters.Add("Customer", condition.Customer, DbType.String);

            return parameters;
        }

        private List<DynamicParameters> GenerateParameterList(IEnumerable<RobotInfo> list)
        {
            var parameters = new List<DynamicParameters>();

            foreach (var item in list)
            {
                var para = GenerateParameters(item);
                parameters.Add(para);
            }

            return parameters;
        }
    }
}
