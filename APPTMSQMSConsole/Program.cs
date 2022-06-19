// See https://aka.ms/new-console-template for more information
using BusinessLogic;
using System;

namespace APPTMSQMSConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var bll = new RobotInfo_BLL();

            var path = "FakeData";
            var filename = "TPK83R0102(0) Official Model and Part Number_TRI_210902.xlsx";
            // var count = await bll.ImportData(path, filename); //要觸發 UnhandledExeption 先決條件是呼叫 Task.Wait() 或 Task.Result。

            var item = await bll.GetList();
            Console.Write(item.First().PartNumber);
        }
    }
}

