using Dapper;
using Microsoft.Data.Sqlite;
using ModelLibrary;
using DataAccess.Generic;
using System.Data;

namespace DataAccess
{
    public class ProductRobot_DAO
    {
        private readonly string _ds = @"Data Source=D:\APPTMSQMSService\DataAccess\TMSQMS.db";

        private void InitTable()
        {
            if (File.Exists(_ds)) return;
            using (var cn = new SqliteConnection(_ds))
            {
                cn.Execute(@"
                        CREATE TABLE ProductRobots (
                            Id INTEGER AUTOINCREMENT,
                            PartNumber VARCHAR(100),
                            Description NVARCHAR(200),
                            Specification VARCHAR(50),
                            ProductLine VARCHAR(50),
                            ModelName VARCHAR(50),
                            OfficialProductName VARCHAR(50),
                            Length INTEGER,
                            IsVision VARCHAR(5),
                            PlugType VARCHAR(5),
                            IsDC VARCHAR(5),
                            OS VARCHAR(10),
                            HardwardVersion VARCHAR(5),
                            HMI VARCHAR(5),
                            HasSemiCertificate VARCHAR(5),
                            ComplexCable VARCHAR(50),
                            HasESD VARCHAR(5),
                            PalletProtect VARCHAR(5),
                            UsbforUserManual VARCHAR(5),
                            HasOptionCommunication VARCHAR(5),
                            Customer VARCHAR(50),
                            HasCncCover VARCHAR(5),
                            Palletizing VARCHAR(5),
                            HasLightModule VARCHAR(5),
                            DDR VARCHAR(5),
                            CONSTRAINT Player_PK PRIMARY KEY (Id)
                        ");
            }
        }

        public async Task<ProductRobot> Get(ProductRobot condition)
        {
            string sql = GenerateQuerySql(condition);
            var parameters = GenerateParameters(condition);

            using (var connection = new SqliteConnection(_ds))
            {
                return await connection.QueryFirstOrDefaultAsync<ProductRobot>(sql, parameters);
            }
        }

        public async Task<IEnumerable<ProductRobot>> GetList() 
        {
            string sql = GenerateQuerySql();
            using (var connection = new SqliteConnection(_ds))
            {
                return await connection.QueryAsync<ProductRobot>(sql);
            }
        }

        public async Task<IEnumerable<ProductRobot>> GetList(ProductRobot condition)
        {
            string sql = GenerateQuerySql(condition);
            DynamicParameters parameters = GenerateParameters(condition);
            using (var connection = new SqliteConnection(_ds))
            {
                return await connection.QueryAsync<ProductRobot>(sql, parameters);
            }
        }

        public async Task<int> InsertListAsync(IEnumerable<ProductRobot> list)
        {
            InitTable();

            var parameters = GenerateParameterList(list);
            var sql =
            @"
                INSERT INTO ProductRobots VALUES
                (@No, @PartNumber, @Description, @ProductName, @ModelName, @Length,
                @Vision, @PlugType, @IsAgv, @OS, @HardwardVersion, @HMI, @IsSemi,
                @ComplexCable, @IsESD, @PalletProtect, @UsbforYes, @HasCommunication, @Customer)
            ";

            return await ExecuteListAsync(sql, parameters);
        }

        public async Task<int> MergeListAsync(IEnumerable<ProductRobot> list)
        {
            var parameters = GenerateParameterList(list);
            var sql =
            @"
                INSERT OR REPLACE INTO ProductRobots (
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
                DELETE FROM ProductRobots
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

        private string GenerateQuerySql()
        {
            return $@"
                SELECT PartNumber, Description, ProductName, ModelName, Length,
                        Vision, PlugType, IsAgv, OS, HardwardVersion, HMI, IsSemi,
                        ComplexCable, IsESD, PalletProtect, UsbforYes, HasCommunication, Customer
                FROM ProductRobots";
        }

        private string GenerateQuerySql(ProductRobot condition)
        {
            return $@"
                SELECT PartNumber, Description, ProductName, ModelName, Length,
                        Vision, PlugType, IsAgv, OS, HardwardVersion, HMI, IsSemi,
                        ComplexCable, IsESD, PalletProtect, UsbforYes, HasCommunication, Customer
                FROM ProductRobots
                WHERE {"OS != @OS".If(!string.IsNullOrEmpty(condition.OS))}
                    {"AND ModelName = @ModelName".If(!string.IsNullOrEmpty(condition.ModelName))}
                    {"AND Vision = @Vision".If(!string.IsNullOrEmpty(condition.IsVision))}
                    {"AND PlugType = @PlugType".If(!string.IsNullOrEmpty(condition.PlugType))}
                    {"AND IsAgv = @IsAgv".If(!string.IsNullOrEmpty(condition.IsDC))}
                    {"AND HardwardVersion = @HardwardVersion".If(!string.IsNullOrEmpty(condition.HardwardVersion))}
                    {"AND HMI = @HMI".If(!string.IsNullOrEmpty(condition.HMI))}
                    {"AND IsSemi = @IsSemi".If(!string.IsNullOrEmpty(condition.HasSemiCertificate))}
                    {"AND ComplexCable = @ComplexCable".If(!string.IsNullOrEmpty(condition.ComplexCable))}
                    {"AND IsESD = @IsESD".If(!string.IsNullOrEmpty(condition.HasESD))}
                    {"AND PalletProtect = @PalletProtect".If(!string.IsNullOrEmpty(condition.PalletProtect))}
                    {"AND UsbforYes = @UsbforYes".If(!string.IsNullOrEmpty(condition.UsbforUserManual))}
                    {"AND HasCommunication = @HasCommunication".If(!string.IsNullOrEmpty(condition.HasOptionCommunication))}
                    {"AND Customer LIKE @Customer".If(!string.IsNullOrEmpty(condition.Customer))}
            ";
        }

        private DynamicParameters GenerateParameters(ProductRobot condition)
        {
            // 一般單傳物件也可以，但是效能不好
            // DynamicParameters可以指定型別使用, 效果接近ADO.NET的Parameters
            var parameters = new DynamicParameters();
            parameters.Add("No", condition.Id, DbType.Int32);
            parameters.Add("PartNumber", condition.PartNumber, DbType.String);
            parameters.Add("Description", condition.Description, DbType.String);
            parameters.Add("ProductName", condition.ProductLine, DbType.String);
            parameters.Add("ModelName", condition.ModelName, DbType.String);
            parameters.Add("Length", condition.Length, DbType.Int32);
            parameters.Add("Vision", condition.IsVision, DbType.String);
            parameters.Add("PlugType", condition.PlugType, DbType.String);
            parameters.Add("IsAgv", condition.IsDC, DbType.String);
            parameters.Add("OS", condition.OS, DbType.String);
            parameters.Add("HardwardVersion", condition.HardwardVersion, DbType.String);
            parameters.Add("HMI", condition.HMI, DbType.String);
            parameters.Add("IsSemi", condition.HasSemiCertificate, DbType.String);
            parameters.Add("ComplexCable", condition.ComplexCable, DbType.String);
            parameters.Add("IsESD", condition.HasESD, DbType.String);
            parameters.Add("PalletProtect", condition.PalletProtect, DbType.String);
            parameters.Add("UsbforYes", condition.UsbforUserManual, DbType.String);
            parameters.Add("HasCommunication", condition.HasOptionCommunication, DbType.String);
            parameters.Add("Customer", condition.Customer, DbType.String);

            return parameters;
        }

        private List<DynamicParameters> GenerateParameterList(IEnumerable<ProductRobot> list)
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
