using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 课程设计
{
    public partial class info_input : Form
    {
        public info_input()
        {
            InitializeComponent();
            textBox3.Text = User.username;
            textBox4.Text = User.usertel;
            textBox5.Text = User.userid;
            textBox6.Text = User.userstuid;
            textBox7.Text = User.usermail;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            info();
            SqlConnection sq;
        }

        

        private void info()
        {
            Random id = new Random();
            int i = id.Next();
            SqlConnection link = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\inspiron 7000\Documents\vstest.mdf;Integrated Security=True;Connect Timeout=30");
           // SqlConnection link = Sql.sqlcon();
            //Sql.sqlupdate(link,label3.Text,textBox4.Text);

           link.Open();
            string cmeds = string.Format("insert into vis  values(" + i.ToString() + ",'" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text  + "','" + textBox5.Text + "','"+textBox7.Text + "','" + textBox6.Text + "')");
            SqlCommand cmed = new SqlCommand(cmeds, link);
            try
            {
                cmed.ExecuteNonQuery();
                MessageBox.Show("录入成功！");
            }
            catch (Exception es) { MessageBox.Show("录入失败！请检查数据连接！");}
            link.Close();
            
            //Sql.close(link);
        }
    }
}
