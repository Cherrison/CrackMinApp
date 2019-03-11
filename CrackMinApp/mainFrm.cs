using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CrackMinApp
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        private void blDir()
        {
            string dir = @"D:\CrackMinApp\wxapkg";
            string fileName = "";
            DirectoryInfo dire = new DirectoryInfo(dir);
            tables.Items.Clear();
            if (dire.Exists)
            {
                foreach (FileInfo file in dire.GetFiles("*.wxapkg"))
                {
                    fileName = file.FullName;
                    int i = fileName.LastIndexOf("\\");
                    fileName = fileName.Substring(++i, fileName.Length - i);
                    tables.Items.Add(fileName);
                }
            }
            else
            { MessageBox.Show("目录不存在\n", "提示"); return; }
        }
        private void btuExc_Click(object sender, EventArgs e)
        {
            if (tables.SelectedItem==null) MessageBox.Show("请先选择文件,或目录选择错误,或该文件目录下没有文件,请加入", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            else
            {
                try
                {
                    //创建一个进程
                    Process p = new Process();
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
                    p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                    p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                    p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                    p.StartInfo.CreateNoWindow = true;//不显示程序窗口

                    p.Start();//启动程序

                    string strCMD0 = "D:";
                    string strCMD1 = @"cd D:\CrackMinApp\nodejs\nodejs";
                    string strCMD2 = @"node .\wuWxapkg.js D:\take\"+tables.SelectedItem.ToString();
                    //向cmd窗口发送输入信息
                    p.StandardInput.WriteLine(strCMD0);
                    for (int i = 0; i < 100; i++)
                    {
                        System.Threading.Thread.Sleep(60);
                        proBar.Value += proBar.Step;
                        if (proBar.Value > 95) break;
                    }
                    p.StandardInput.WriteLine(strCMD1);
                    p.StandardInput.WriteLine(strCMD2 + "&exit");

                    p.StandardInput.AutoFlush = true;

                    //获取cmd窗口的输出信息
                    string output = p.StandardOutput.ReadToEnd();
                    //等待程序执行完退出进程
                    p.WaitForExit();
                    p.Close();
                    proBar.Value = 100;
                    lblSta.Text = "执行完成";
                    rtxInfo.Text = output.ToString();
                    Console.WriteLine(output);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n跟踪;" + ex.StackTrace);
                }
            }
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            this.blDir();
        }
    }
}
