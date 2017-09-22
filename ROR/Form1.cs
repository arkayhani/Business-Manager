using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace ROR
{
    public partial class Form1 : Form
    {

        public List<proj> projs;
        public Form1()
        {
            InitializeComponent();
            projs = new List<proj>();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                groupBox2.Visible = true;
                groupBox1.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showproj();

        }
        public void showproj()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            textBox10.Text = "";
            proj protemp = projs[comboBox1.SelectedIndex];
            if (protemp.onetimes.Count==0)
            {
                foreach (var item in protemp.onetimes)
                {
                    listBox1.Items.Add("In: " + item.time + " amount: " + item.amount);
                }
            }
            if (protemp.continiuses.Count==0)
            {
                foreach (var item in protemp.continiuses)
                {
                    listBox1.Items.Add("From: " + item.ftime + " till: " + item.endtime + " amount: " + item.amount);
                }
            }
            double pwb = 0;
            double pwc = 0;
            if (protemp.onetimes.Count==0)
            {
                foreach (var item in protemp.onetimes)
                {

                    double p = item.amount / Math.Pow(1 + protemp.intrest, item.time);
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

                    double pa = item.amount * ((Math.Pow(1 + protemp.intrest, item.endtime - item.ftime + 1) - 1) / (protemp.intrest * Math.Pow(1 + protemp.intrest, item.endtime - item.ftime + 1)));
                    double p = pa;

                    p = pa / Math.Pow(1 + protemp.intrest, item.ftime - 1);

                    if (p > 0)
                        pwb += p;
                    else
                        pwc -= p;


                }
            }
            protemp.PWB = pwb;
            protemp.PWC = pwc;
            double npw = pwb - pwc;
            double fwb = pwb * Math.Pow(1 + protemp.intrest, protemp.Time);
            double fwc = pwc * Math.Pow(1 + protemp.intrest, protemp.Time);
            double nfw = fwb - fwc;
            double euab = pwb * (protemp.intrest * Math.Pow(1 + protemp.intrest, protemp.Time)) / (Math.Pow(1 + protemp.intrest, protemp.Time) - 1);
            double euac = pwc * (protemp.intrest * Math.Pow(1 + protemp.intrest, protemp.Time)) / (Math.Pow(1 + protemp.intrest, protemp.Time) - 1);
            double neua = euab - euac;
            double flag = 10000;
            double ror = 0;
            textBox7.Text = "";
            textBox7.Text = "pwb: " + pwb.ToString() + Environment.NewLine;
            textBox7.Text += "pwc: " + pwc.ToString() + Environment.NewLine;
            textBox7.Text += "npw: " + npw.ToString() + Environment.NewLine;
            textBox7.Text += "fwb: " + fwb.ToString() + Environment.NewLine;
            textBox7.Text += "fwc: " + fwc.ToString() + Environment.NewLine;
            textBox7.Text += "nfw: " + nfw.ToString() + Environment.NewLine;
            textBox7.Text += "euab: " + euab.ToString() + Environment.NewLine;
            textBox7.Text += "euac: " + euac.ToString() + Environment.NewLine;
            textBox7.Text += "neua: " + neua.ToString() + Environment.NewLine;

            for (double i = 0; i < 1.5050; i += 0.0005)
            {
                pwb = 0;
                pwc = 0;
                if (protemp.onetimes.Count==0)
                {
                    foreach (var item in protemp.onetimes)
                    {
                        if (item.time > 0)
                        {
                            double p = item.amount / Math.Pow(1 + i, item.time);
                            if (p > 0)
                                pwb += p;
                            else
                                pwc -= p;
                        }
                        else
                        {
                            double p = item.amount;
                            if (p > 0)
                                pwb += p;
                            else
                                pwc -= p;
                        }
                    }
                }
                if (protemp.continiuses.Count==0)
                {
                    foreach (var item in protemp.continiuses)
                    {

                        double pa = item.amount * (Math.Pow(1 + i, item.endtime - item.ftime + 1) - 1) / (i * Math.Pow(1 + i, item.endtime - item.ftime + 1));
                        double p = pa;
                        if (item.ftime > 1)
                            p = pa / Math.Pow(1 + i, item.ftime - 1);

                        if (p > 0)
                            pwb += p;
                        else
                            pwc -= p;


                    }
                }
                npw = pwb - pwc;
                if (Math.Abs(npw) < flag)
                {
                    flag = npw;
                    ror = i;
                }
            }
            if (ror < 1.5045)
                textBox7.Text += "ror: " + ror.ToString() + Environment.NewLine;
            else
            {
                textBox7.Text += "ror: " + "We dont have ROR" + Environment.NewLine;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button4.Enabled = true;
            comboBox1.Items.Add(textBox6.Text);
            proj protemp = new proj();
            protemp.intrest = double.Parse(textBox9.Text);
            protemp.Time = double.Parse(textBox13.Text);
            protemp.name = textBox6.Text;
            projs.Add(protemp);
            textBox6.Text = "";
            textBox9.Text = "";
            textBox13.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            proj protemp = projs[comboBox1.SelectedIndex];
            
            if (radioButton1.Checked == true)
            {
                onetime tempone = new onetime();
               
                if (checkBox1.Checked == false)
                {
                    tempone.time = double.Parse(textBox2.Text);
                }
                else
                {
                    tempone.time = protemp.Time;
                    protemp.SV = double.Parse(textBox1.Text);
                }
                tempone.amount = double.Parse(textBox1.Text);
                protemp.onetimes.Add(tempone);
                textBox2.Text = "";
                textBox1.Text = "";
            }
            if (radioButton2.Checked == true)
            {
                continuous tempone = new continuous();
                
                tempone.ftime = double.Parse(textBox4.Text);
                tempone.amount = double.Parse(textBox3.Text);
                tempone.endtime = double.Parse(textBox5.Text);
                protemp.continiuses.Add(tempone);
                textBox4.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
            }
            showproj();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            proj protemp = projs[comboBox1.SelectedIndex];
            protemp.depreciation = new double[(int)(protemp.Time + 1)];
            for (int i = 0; i < protemp.Time + 1; i++)
            {
                protemp.depreciation[i] = 0;
            }
            double p = 0;
            double sv = protemp.SV;
            foreach (var item in protemp.onetimes)
            {
                if (item.time == 0)
                    p = -item.amount;

            }
            if (radioButton7.Checked == true)
            {

                for (int i = 0; i < protemp.Time + 1; i++)
                {
                    protemp.depreciation[i] =0;
                }

            }
            if (radioButton3.Checked == true)
            {

                double D = (p - sv) / protemp.Time;
                protemp.depreciation[0] = 0;
                for (int i = 1; i < protemp.Time + 1; i++)
                {
                    protemp.depreciation[i] = D;
                }

            }
            if (radioButton4.Checked == true)
            {

                double syd = (protemp.Time * (protemp.Time + 1)) / 2;
                protemp.depreciation[0] = 0;
                for (int i = 1; i < protemp.Time + 1; i++)
                {
                    double D = (protemp.Time - i + 1) * (p - sv) / syd;
                    protemp.depreciation[i] = D;
                }

            }
            if (radioButton6.Checked == true)
            {

                double d = 1 - Math.Pow((sv / p), (1 / protemp.Time));
                protemp.depreciation[0] = 0;
                for (int i = 1; i < protemp.Time + 1; i++)
                {
                    double D = d * p * Math.Pow((1 - d), i);
                    protemp.depreciation[i] = D;
                }

            }
            if (radioButton5.Checked == true)
            {

                double d = (2 / protemp.Time);
                protemp.depreciation[0] = 0;
                double bv = p;
                for (int i = 1; i < protemp.Time + 1; i++)
                {

                    double D = d * p * Math.Pow((1 - d), i - 1);
                    bv = bv - D;
                    if (bv >= sv)
                    {
                        protemp.depreciation[i] = D;
                    }
                    else
                    {
                        protemp.depreciation[i] = bv + D - sv;
                        if (bv + D - sv <= 0)
                            protemp.depreciation[i] = 0;
                    }
                }

            }
            if (protemp.depreciation.Length==0)
            {
                for (int i = 0; i < protemp.Time + 1; i++)
                {
                    listBox1.Items.Add("Deprec in " + i.ToString() + " amount: " + protemp.depreciation[i].ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            textBox10.Text = "";
            proj protemp = projs[comboBox1.SelectedIndex];
            double n = protemp.Time;
            double inf = double.Parse(textBox12.Text);
            protemp.CFBT = new double[(int)(n + 1)];
            protemp.TAX = new double[(int)(n + 1)];
            protemp.CFAT = new double[(int)(n + 1)];
            for (int i = 0; i < n + 1; i++)
            {
                protemp.CFBT[i] = 0;
                protemp.TAX[i] = 0;
                protemp.CFAT[i] = 0;

            }

            for (int i = 0; i < n + 1; i++)
            {
                double cfbttemp = 0;
                foreach (var item in protemp.onetimes)
                {
                    if (item.time == i)
                        cfbttemp += item.amount;

                }
                foreach (var item in protemp.continiuses)
                {
                    if (item.ftime <= i && item.endtime >= i)
                        cfbttemp += item.amount;

                }
                if (i == n)
                {
                    cfbttemp = cfbttemp - protemp.SV;
                }
                protemp.CFBT[i] = cfbttemp*Math.Pow((1+inf),i);
            }
            for (int i = 1; i < n + 1; i++)
            {
                double TI = protemp.CFBT[i] - protemp.depreciation[i];
                if (TI > 0)
                {
                    double TAX = TI * double.Parse(textBox8.Text);
                    protemp.TAX[i] = TAX;
                    protemp.CFAT[i] = protemp.CFBT[i] - protemp.TAX[i];
                }
                else
                {
                    protemp.TAX[i] = 0;
                    protemp.CFAT[i] = 0;
                }
            }
            protemp.CFAT[0] = protemp.CFBT[0];
            for (int i = 0; i < n + 1; i++)
            {
                listBox2.Items.Add(protemp.CFBT[i].ToString());
                listBox3.Items.Add(protemp.depreciation[i].ToString());
                listBox4.Items.Add(protemp.TAX[i].ToString());
                listBox5.Items.Add(protemp.CFAT[i].ToString());
            }
            listBox2.Items.Add((protemp.SV*Math.Pow((1+inf),n)).ToString());
            listBox3.Items.Add("0");
            listBox4.Items.Add("0");
            listBox5.Items.Add((protemp.SV*Math.Pow((1+inf),n)).ToString());
            double pwb = 0;
            double pwc = 0;

            for (int i = 0; i < n + 1; i++)
            {
                double p = protemp.CFAT[i] /( Math.Pow(1 + protemp.intrest, i) * Math.Pow((1 + inf), i));
                if (p > 0)
                    pwb += p;
                else
                    pwc -= p;
            }

            pwb += (protemp.SV * Math.Pow((1 + inf), n)) / (Math.Pow(1 + protemp.intrest, n) * Math.Pow((1 + inf), n)); 

            double npw = pwb - pwc;
            double fwb = pwb * Math.Pow(1 + protemp.intrest, protemp.Time);
            double fwc = pwc * Math.Pow(1 + protemp.intrest, protemp.Time);
            double nfw = fwb - fwc;
            double euab = pwb * (protemp.intrest * Math.Pow(1 + protemp.intrest, protemp.Time)) / (Math.Pow(1 + protemp.intrest, protemp.Time) - 1);
            double euac = pwc * (protemp.intrest * Math.Pow(1 + protemp.intrest, protemp.Time)) / (Math.Pow(1 + protemp.intrest, protemp.Time) - 1);
            double neua = euab - euac;
            double flag = 10000;
            double ror = 0;
            textBox10.Text = "";
            textBox10.Text = "pwb: " + pwb.ToString() + Environment.NewLine;
            textBox10.Text += "pwc: " + pwc.ToString() + Environment.NewLine;
            textBox10.Text += "npw: " + npw.ToString() + Environment.NewLine;
            textBox10.Text += "fwb: " + fwb.ToString() + Environment.NewLine;
            textBox10.Text += "fwc: " + fwc.ToString() + Environment.NewLine;
            textBox10.Text += "nfw: " + nfw.ToString() + Environment.NewLine;
            textBox10.Text += "euab: " + euab.ToString() + Environment.NewLine;
            textBox10.Text += "euac: " + euac.ToString() + Environment.NewLine;
            textBox10.Text += "neua: " + neua.ToString() + Environment.NewLine;

            for (double i = 0; i < 1.5050; i += 0.0005)
            {
                pwb = 0;
                pwc = 0;

                for (int j = 0; j < n + 1; j++)
                {

                    double p = protemp.CFAT[j] / (Math.Pow(1 + i, j) );
                    if (p > 0)
                        pwb += p;
                    else
                        pwc -= p;

                }
                pwb += protemp.SV  / Math.Pow(1 + i, n);

                npw = pwb - pwc;
                if (Math.Abs(npw) < flag)
                {
                    flag = npw;
                    ror = i;
                }
            }
            if (ror < 1.5045)
                textBox10.Text += "ror: " + ror.ToString() + Environment.NewLine;
            else
            {
                textBox10.Text += "ror: " + "We dont have ROR" + Environment.NewLine;
            }


        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.Enabled = false;
            }
            else { textBox2.Enabled = true; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(projs,textBox11.Text);
            f2.Show();
        }

        private void radioButton7_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}
