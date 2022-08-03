using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;
using ModelLibrary;

namespace APPTMSQMSWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductRobot_BLL _robotBLL = new();
        private readonly ProductSoftware_BLL _softwaretBLL = new();
        public MainWindow()
        {
            /*
            var list = _robotBLL.GetList(new ProductRobot() { }).Result;
            var hmiList = new List<string>();
            foreach (var item in list)
                hmiList.Add(item.HMI);
            */
            InitializeComponent();

            //listBox.ItemsSource = hmiList.Distinct();
        }

        private async void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            await _robotBLL.DeleteListAsync();
            ExcelFileInfo? excel = new()
            {
                filePath = "FakeData",
                fileName = "TPK83R0102(0) Official Model and Part Number_TRI_211118.xlsx",
                sheetNames = new string[] { "Robots-TM5A PN HW3.2", "Robots-TMAA PN HW3.2" }
            };
            var count = await _robotBLL.ImportData(excel);
            /*
            excel.sheetNames = new string[] { "Software" };
            count += await _softwaretBLL.ImportData(excel);
            */
            Console.WriteLine(count);
        }

        private async void BtnGetStandardRobot_Click(object sender, RoutedEventArgs e)
        {
            var list = await _robotBLL.GetStandardRobotList(new ProductRobot()
            {
                HMI = "1.84",
            });

            this.datagrid.ItemsSource = null;
            this.datagrid.ItemsSource = list;
        }

        private async void BtnGetTM5_700_Click(object sender, RoutedEventArgs e)
        {
            var list = await _robotBLL.GetStandardRobotList(new ProductRobot()
            {
                ModelName = "TM5-700",
                HMI = "1.84",
            });

            this.datagrid.ItemsSource = null;
            this.datagrid.ItemsSource = list;
        }

        private async void BtnGetTM5_900_Click(object sender, RoutedEventArgs e)
        {

            var list = await _robotBLL.GetStandardRobotList(new ProductRobot()
            {
                ModelName = "TM5-900",
                HMI = "1.84",
            });

            this.datagrid.ItemsSource = null;
            this.datagrid.ItemsSource = list;
        }

        private async void BtnGetTM14_Click(object sender, RoutedEventArgs e)
        {
            var list = await _robotBLL.GetStandardRobotList(new ProductRobot()
            {
                ModelName = "TM14",
                HMI = "1.84",
            });

            this.datagrid.ItemsSource = null;
            this.datagrid.ItemsSource = list;
        }

        private async void BtnGetTM12_Click(object sender, RoutedEventArgs e)
        {
            var list = await _robotBLL.GetStandardRobotList(new ProductRobot()
            {
                ModelName = "TM12",
                HMI = "1.84",
            });

            this.datagrid.ItemsSource = null;
            this.datagrid.ItemsSource = list;
        }
    }
}
