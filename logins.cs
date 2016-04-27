#define sql
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace 课程设计
{
    public partial class logins : Form
    {
        Socket_cs sg = new Socket_cs();
        public logins()
        {
            InitializeComponent();
            Randseed.randseed = new Random().Next(); //后续数据生成初始种子
            //new Thread(() => { sg.bind(); }).Start();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
#if sql   
            try
            {
                SqlConnection link = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\inspiron 7000\Documents\vstest.mdf;Integrated Security=True;Connect Timeout=30");
                link.Open();
                SqlDataAdapter data_sql = new SqlDataAdapter
               ("SELECT * FROM vis where account='" + textBox1.Text + "'", link);
                DataSet dse = new DataSet();
                data_sql.Fill(dse);
                string val = dse.Tables[0].Rows[0]["password"].ToString();
                string[] vals = val.Split(new char[1] {' '});                    //bug备注：当用户密码存在空格时密码验证会发生错误
                if (textBox2.Text == vals[0])                           
                {
                    User.id = dse.Tables[0].Rows[0]["id"];
                    User.username = dse.Tables[0].Rows[0]["name"].ToString();
                    User.userid = dse.Tables[0].Rows[0]["id_number"].ToString();
                    User.userstuid = dse.Tables[0].Rows[0]["student_id"].ToString();
                    User.usermail = dse.Tables[0].Rows[0]["email"].ToString();
                    User.usertel = dse.Tables[0].Rows[0]["tel"].ToString();
                    seat_choose tics = new seat_choose();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("密码错误！"); textBox2.Text=""; 
                }
                 link.Close();
            }
            catch(Exception es)
            {
                MessageBox.Show("账户不存在！");
                textBox2.Text = "";
                seat_choose tics = new seat_choose();
                this.Hide();
            }
#endif
#if forbidden_sql
            if (textBox1.Text == "") { MessageBox.Show("请输入帐户名"); return; }
            if (textBox2.Text == "") { MessageBox.Show("请输入密码"); return; }
            main_window start = new main_window();
            int i1 = 0, pass = 0;
            while (i1 < 10)
            {
                if (textBox1.Text == login[i1, 0] && textBox2.Text == login[i1, 1])
                { this.Hide(); start.Show(); pass = 1; } i1++;
            }
            if (pass!=1)
            {
                MessageBox.Show("密码错误！"); textBox1.Text = ""; textBox2.Text = "";
            }
#endif
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // sg.send(textBox1.Text);
            info_input ipt = new info_input();
            ipt.Show();

            Form1 f1 = new Form1();
            f1.Owner = this;
            f1.Show();
#if sql_version_secd
            Random id = new Random();
            int i = id.Next();
            SqlConnection link = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\inspiron 7000\Documents\vstest.mdf;Integrated Security=True;Connect Timeout=30");
            link.Open();
            string cmeds=string.Format("insert into vis  values("+ i +",'"+textBox1.Text+"','"+textBox2.Text+"')");
            SqlCommand cmed = new SqlCommand(cmeds,link);
            try
            {
                cmed.ExecuteNonQuery();
                MessageBox.Show("注册成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show("用户名已存在!");
            }
            /*{
                string strs="visual";
                SqlCommand cmd = new SqlCommand("SearchContact",link);  //存储过程名称为SearchContact
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@account", SqlDbType.NChar, 10);   //传入参数
                cmd.Parameters["@account"].Value = strs.Trim();
            }*/
             link.Close();
#endif
#if forbidden_sql
            if (i > 99) { MessageBox.Show("注册已满"); return; }
            if (textBox1.Text == "") { MessageBox.Show("请输入帐户名"); return; }
            if (textBox2.Text == "") { MessageBox.Show("请输入密码"); return; }
            for (int i_ = 0; i_ < i; i_++)
            {
                if (login[i_, 0] == textBox1.Text)
                {
                    MessageBox.Show("用户名已存在");
                    return;
                }
            }
            login[i,0] = textBox1.Text;
            login[i,1] = textBox2.Text;
            i++;
            MessageBox.Show("注册成功！");
            textBox2.Text = "";
#endif
         }
    }
}
