using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace ROR
{
    public class onetime
        {
            public double time;
            public double amount;
        }
public class continuous
        {
            public double ftime;
            public double endtime;
            public double amount;
        }
      public  class proj
        {
          
            public string name;
            public double intrest;
            public double PWB=0;
            public double PWC=0;
            public double Time;
            public double SV=0;
            public List<onetime> onetimes = new List<onetime>();
            public List<continuous> continiuses = new List<continuous>();
            public double[] depreciation;
            public double[] CFBT;
            public double[] TAX;
            public double[] CFAT;
        }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
