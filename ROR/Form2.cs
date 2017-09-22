using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace ROR
{
    public partial class Form2 : Form
    {
        List<proj> projects;
        double marr = 0;
        public Form2(List<proj> projs,string st)
        {
            projects = projs;
            marr = double.Parse(st);
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {  //proj protemp = projs[comboBox1.SelectedIndex];
            Graphics g;
            g = this.CreateGraphics();
            Pen pen=new Pen(Color.Blue,5);
            g.FillRectangle(Brushes.Black, 20, 630, 700, 5);
            g.FillRectangle(Brushes.Black, 20, 20, 5, 620);
            g.DrawLine(pen,20,630,700,20);
            List<proj> projectsorderd = new List<proj>();
            foreach (var item in projects)
            {
                
            }
            foreach (var protemp in projects)
            {




                double pwb = 0;
                double pwc = 0;
                if (protemp.onetimes.Count==0)
                {
                    foreach (var item in protemp.onetimes)
                    {

                        double p = item.amount / Math.Pow(1 + marr, item.time);
                        if (p > 0)
                            pwb += p;
                        else
                            pwc -= p;

                    }
                }
                if (protemp.continiuses.Count==0)
                {
                    foreach (var item in protemp.continiuses)
                    {

                        double pa = item.amount * ((Math.Pow(1 + marr, item.endtime - item.ftime + 1) - 1) / (marr * Math.Pow(1 + marr, item.endtime - item.ftime + 1)));
                        double p = pa;

                        p = pa / Math.Pow(1 + marr, item.ftime - 1);

                        if (p > 0)
                            pwb += p;
                        else
                            pwc -= p;


                    }

                } protemp.PWB = pwb;
                protemp.PWC = pwc;
                
                double x = 20 + pwc / double.Parse(comboBox1.SelectedItem.ToString());
                double y = 630 - pwb / double.Parse(comboBox1.SelectedItem.ToString());
                g.FillEllipse(Brushes.Red, (int)x, (int)y, 15, 15);
            }
           // projectsorderd=projects.OrderBy<
            foreach (var item in projects)
            {

            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
