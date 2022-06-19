using Dapper;
using Microsoft.Data.Sqlite;
using ModelLibrary;

namespace DataAccess
{
    public class RobotInfo_DAO
    {
        public async Task<IEnumerable<RobotInfo>> GetList()
        {
            using (var connection = new SqliteConnection("Data Source=TMSQMS.db"))
            {
                string sql =
                @"
                    SELECT PartNumber, Description, ProductName, ModelName, Length,
                            Vision, PlugType, IsAgv, OS, HardwardVersion, HMI, IsSemi,
                            ComplexCable, IsESD, PalletProtect, UsbforYes, HasCommunication, Customer
                    FROM Products
                ";
                var list = await connection.QueryAsync<RobotInfo>(sql);
                return list;
            }
        }

        public async Task<int> InsertListAsync(IEnumerable<RobotInfo> list)
        {
            var sql =
            @"
                INSERT INTO Products VALUES
                (@No, @PartNumber, @Description, @ProductName, @ModelName, @Length,
                @Vision, @PlugType, @IsAgv, @OS, @HardwardVersion, @HMI, @IsSemi,
                @ComplexCable, @IsESD, @PalletProtect, @UsbforYes, @HasCommunication, @Customer)
            ";

            return await ExecuteListAsync(sql, list);
        }

        public async Task<int> MergeListAsync(IEnumerable<RobotInfo> list)
        {
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

            return await ExecuteListAsync(sql, list);
        }

        public async Task<int> DeleteListAsync()
        {
            string sql =
            @"
                DELETE FROM Products
            ";
            return await ExecuteListAsync(sql);
        }

        private async Task<int> ExecuteListAsync(string sql, IEnumerable<RobotInfo>? list = null)
        {
            int count = 0;
            using (var connection = new SqliteConnection(@"Data Source=TMSQMS.db"))
            {
                connection.Open();

                // Transaction
                using (var tran = connection.BeginTransaction())
                {
                    count += await connection.ExecuteAsync(sql, list);
                    tran.Commit();
                }
            }

            return count;
        }
    }
}
