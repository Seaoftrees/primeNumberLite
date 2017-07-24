using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace primeNumberLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //テキストボックスの中身をコピー
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        //リスト化モードになったとき
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        //判別モードになったとき
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //計算処理(メインプロセス)
        public void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            textBox1.Text = null;
            progressBar1.Value = 0;
            try
            {
                sw.Start();
                ulong n = (ulong)numericUpDown1.Value;

                //素数判別モード
                if (radioButton1.Checked)
                {
                    if (n == 2)
                    {
                        sw.Stop();
                        label9.Text = "素数です。(" + (int)sw.Elapsed.TotalMilliseconds + "ms)";
                        progressBar1.Value = 100;
                    }
                    else if (n % 2 == 0)
                    {
                        sw.Stop();
                        label9.Text = "素数ではありません。(" + (int)sw.Elapsed.TotalMilliseconds + "ms)";
                        textBox1.Text = "2";
                        progressBar1.Value = 100;
                    }
                    else
                    {
                        double sqrt = Math.Sqrt(n);
                        long result = 0;
                        int forProgress = (int)(sqrt - 3) / 100;
                        for (long i = 3; i <= sqrt; i += 2)
                        {
                            if (n % (ulong)i == 0)
                            {
                                result = i;
                                break;
                            }

                            if (i > forProgress * (progressBar1.Value + 1) && progressBar1.Value < 100)
                            {
                                progressBar1.Value += 1;
                            }
                        }

                        if (result == 0)
                        {
                            sw.Stop();
                            label9.Text = "素数です。(" + (int)sw.Elapsed.TotalMilliseconds + "ms)";
                            progressBar1.Value = 100;
                        }
                        else
                        {
                            sw.Stop();
                            label9.Text = "素数ではありません。(" + (int)sw.Elapsed.TotalMilliseconds + "ms)";
                            textBox1.Text = $"{result}";
                            progressBar1.Value = 100;
                        }
                    }

                }//リストモード
                else
                {
                    textBox1.Text = "2\r\n";
                    int forProgress = (int)(n - 3) / 100;
                    int LiteUse = 0;
                    int[] primeLiteList = new int[21];
                    for (ulong k=3; k<=n; k += 2)
                    {

                        double sqrt = Math.Sqrt(k);
                        long result = 0;
                        for (long i = 3; i <= sqrt; i += 2)
                        {
                            if (k % (ulong)i == 0)
                            {
                                result = i;
                                break;
                            }
                        }

                        if (result == 0)
                        {
                            textBox1.Text = textBox1.Text + k + "\r\n";
                        }


                        if (k > (ulong)(forProgress * (progressBar1.Value + 1)) && progressBar1.Value < 100)
                        {
                            progressBar1.Value += 1;
                        }
                    }
                    sw.Stop();
                    int time = (int)sw.Elapsed.TotalMilliseconds;
                    if (sw.Elapsed.TotalHours>1)
                    {
                        label9.Text = "リスト化が終了しました。(" + sw.Elapsed.Hours + "h"
                            + sw.Elapsed.Minutes + "m"
                            +sw.Elapsed.Seconds + "s)";
                    }
                    else if (time > 60000)
                    {
                        label9.Text = "リスト化が終了しました。(" + sw.Elapsed.Minutes + "m" +
                            sw.Elapsed.Seconds + "s)";
                    }
                    else if (time > 10000)
                    {
                        label9.Text = "リスト化が終了しました。(" + (int)sw.Elapsed.TotalSeconds + "s)";
                    }
                    else
                    {
                        label9.Text = "リスト化が終了しました。(" + (int)sw.Elapsed.TotalMilliseconds + "ms)";
                    }
                    progressBar1.Value = 100;
                }

            }
            catch(Exception ex)
            {
                label9.Text = "エラー(ErrorMessageは右)";
                textBox1.Text = ex.Message;
            }
        }

    }
}
