/*
 * 2018年6月20日 16:42:34
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zsbApp.SplitSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Shown += Form1_Shown;
        }

        private SplitSql _splitSql;

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this._splitSql = new SplitSql();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = this._splitSql.Split(this.comboBox1.Text);
            if (x == null)
            {
                this.appendText("未能解析");
                return;
            }
            this.appendText($"表名：{x.Table}");
            if (x.Data == null)
            {
                this.appendText("未能解析字段与值，可能是值当中含有逗号");
            }
            else
            {
                foreach (var item in x.Data)
                {
                    this.appendText($"{item.Key} = {item.Value}");
                }
            }
            this.appendText("-----------------------------------------");
        }

        private void appendText(string text)
        {
            this.textBox1.AppendText($"{text}{System.Environment.NewLine}");
        }
    }
}
