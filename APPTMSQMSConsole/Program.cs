// See https://aka.ms/new-console-template for more information
using BusinessLogic;
using ModelLibrary;
using System;

namespace APPTMSQMSConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await ExecuteProductRobot();
            //var bll = new ProductSoftWare_BLL();
        }

        private static async Task ExecuteProductRobot()
        {
            var bll = new ProductRobot_BLL();

            // 匯入料號檔案 + CRUD
            var excel = new ExcelFileInfo
            {
                filePath = "FakeData",
                fileName = "TPK83R0102(0) Official Model and Part Number_TRI_210902.xlsx",
                sheetNames = new string[] { "Robots-TM5A PN HW3.2", "Robots-TMAA PN HW3.2" }
            };
            var count = await bll.ImportData(excel); //要觸發 UnhandledExeption 先決條件是呼叫 Task.Wait() 或 Task.Result。

            var item = await bll.GetList(new ProductRobot
            {
                Customer = "QUANTA"
            });
            Console.Write(item.Count());
        }
    }
}

