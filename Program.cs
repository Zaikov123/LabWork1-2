using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKozl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataController dataController = DataController.GetInstance();
            dataController.LoadData();
            while (true)
            {
                dataController.ShowMainMenu();
            }
        }
    }
}
