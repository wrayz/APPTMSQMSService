using ModelLibrary;
using OfficeOpenXml;

namespace BusinessLogic.ExcelHelper
{
    public class TechmanExcelReader
    {
        public IEnumerable<ProductSoftware> GetSoftwareList(ExcelFileInfo excel)
        {
            List<ProductSoftware> list = new List<ProductSoftware>();
            foreach (var name in excel.sheetNames)
            {
                var products = ConvertExcelSoftware(excel, name);
                list.AddRange(products);
            }

            return list.GroupBy(x => x.PartNumber).Select(y => y.Last());
        }

        private List<ProductSoftware> ConvertExcelSoftware(ExcelFileInfo excel, string name)
        {
            var list = new List<ProductSoftware>();
            var filePath = FileUtil.GetFileInfo(excel.filePath, excel.fileName).FullName;
            FileInfo existingFile = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //Get the worksheet in the workbook
                ExcelWorksheet sheet = package.Workbook.Worksheets[name];
                int endRowIndex = sheet.Dimension.End.Row;

                //轉成物件
                for (int i = 3; i <= endRowIndex; i++)
                {
                    var item = new ProductSoftware
                    {
                        ProductName = (string)sheet.Cells[i, 2].Value,
                        PartNumber = (string)sheet.Cells[i, 3].Value,
                        Description = (string)sheet.Cells[i, 4].Value,
                        Software = (string)sheet.Cells[i, 5].Value,
                        Note = (string)sheet.Cells[i, 6].Value,
                        DongleKey = (string)sheet.Cells[i, 7].Value,
                    };

                    list.Add(item);
                }
            } // the using statement automatically calls Dispose() which closes the package.

            return list;
        }

        public IEnumerable<RobotInfo> GetRobotList(ExcelFileInfo excel)
        {
            List<RobotInfo> list = new List<RobotInfo>();
            foreach (var name in excel.sheetNames)
            {
                var products = ConvertExcelRobots(excel, name);
                list.AddRange(products);
            }

            return list.GroupBy(x => x.PartNumber).Select(y => y.Last());

            // 找重複資料的問題，找到了，但很詭異
            // var test = robots.GroupBy(x => x.PartNumber).Where(y => y.Count() > 1).Select(z => z.First()).ToList();
            // foreach (var item in test)
            // {
            //     System.Console.WriteLine(item.No);
            //     System.Console.WriteLine(item.PartNumber);
            // }
            // var test2 = robots.GroupBy(x => x.PartNumber).Select(y => y.Last()).ToList();
            // System.Console.WriteLine("----------");

            // foreach (var item in test2)
            // {
            //     foreach (var item2 in test)
            //     {
            //         if (item.PartNumber == item2.PartNumber)
            //         {
            //             System.Console.WriteLine(item.No);
            //             System.Console.WriteLine(item.PartNumber);
            //         }
            //     }
            // }
        }

        private List<RobotInfo> ConvertExcelRobots(ExcelFileInfo excel, string sheetName)
        {
            var robots = new List<RobotInfo>();
            var filePath = FileUtil.GetFileInfo(excel.filePath, excel.fileName).FullName;
            FileInfo existingFile = new FileInfo(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //Get the worksheet in the workbook
                ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];
                int endRowIndex = sheet.Dimension.End.Row;

                //轉成物件
                for (int i = 3; i <= endRowIndex; i++)
                {
                    var item = new RobotInfo
                    {
                        No = Convert.ToInt32(sheet.Cells[i, 1].Value),
                        PartNumber = (string)sheet.Cells[i, 2].Value,
                        Description = (string)sheet.Cells[i, 3].Value,
                        ProductName = (string)sheet.Cells[i, 4].Value,
                        ModelName = (string)sheet.Cells[i, 5].Value,
                        Length = Convert.ToInt32(sheet.Cells[i, 6].Value),
                        Vision = (string)sheet.Cells[i, 7].Value,
                        PlugType = (string)sheet.Cells[i, 8].Value,
                        IsAgv = (string)sheet.Cells[i, 9].Value,
                        OS = (string)sheet.Cells[i, 10].Value,
                        HardwardVersion = sheet.Cells[i, 11].Value.ToString(), //double to string
                        HMI = sheet.Cells[i, 12].Value.ToString(), //double to string
                        IsSemi = (string)sheet.Cells[i, 13].Value,
                        ComplexCable = (string)sheet.Cells[i, 14].Value,
                        IsESD = (string)sheet.Cells[i, 15].Value,
                        PalletProtect = (string)sheet.Cells[i, 16].Value,
                        UsbforYes = (string)sheet.Cells[i, 17].Value,
                        HasCommunication = (string)sheet.Cells[i, 18].Value,
                        Customer = (string)sheet.Cells[i, 19].Value,
                    };

                    robots.Add(item);
                }
            } // the using statement automatically calls Dispose() which closes the package.

            return robots;
        }
    }
}
