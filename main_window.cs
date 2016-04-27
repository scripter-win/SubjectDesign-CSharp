using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 课程设计
{
    public partial class main_window : Form
    {
        public string buy_message="";
        public linklist link = new linklist();
        public buyer er = new buyer();

        public main_window()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            
            string str = link.find(textBox1.Text + "-" + textBox2.Text);
            
            string[] strs = str.Split(new char[1] { '-' });
            button5.Visible=false;
            for (int i = 0; strs[i] != "end"; i++) listBox2.Items.Add(strs[i]);
        }

        private void main_window_Load(object sender, EventArgs e)
        {
            link.create();
            link.data_make();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex <= -1)
            {
                MessageBox.Show("请选择车次！");
                return ;
            }

            if  (radioButton1.Checked == true && radioButton2.Checked == true || radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("请选择座位类型！");
                return;
            }

                buy_message = "";
            if (radioButton1.Checked == true)
                buy_message +="<一等座>";
            else buy_message +="<二等座>";
            buy_message += listBox2.SelectedItem.ToString();

            buy_message += ("<"+ textBox1.Text +">");
            buy_message += ("<"+ textBox2.Text +">");

            //for (int iis = int.Parse(listBox1.DisplayMember.ToString().Substring(0, 1)); iis > 0; iis--)
            {
                string[] info = link.buy(buy_message).Split(new char[1] { ',' });
                if (info[0] != "已无余票！")
                {
                    er.cost[er.i] += double.Parse(info[0]);
                    er.seat[0, 0, er.i] = int.Parse(info[1]);
                    er.seat[0, 1, er.i] = int.Parse(info[2]);
                    er.seat[0, 2, er.i] = int.Parse(info[4]);
                    er.seat[0, 3, er.i] = int.Parse(info[5]);
                    er.message[er.i++] = info[3] + "     " + textBox1.Text + "->" + textBox2.Text+User.username;
                }
                else
                {
                    MessageBox.Show("错误~poi，请重新选择");
                    return;
                }
            }

            MessageBox.Show("购买成功！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            
            ///listbox显示的内容清空（listbox为显示车辆信息的白框）
            
            if (er.i > 0)
            {
                for (int i_ = 0; i_ < er.i; i_++)
                    listBox2.Items.Add(er.message[i_] + "  价格：" + er.cost[i_] + "   " + er.seat[0, 0, i_].ToString("D0") + "车厢 " + er.seat[0, 1, i_].ToString("D3") + "号" + "  单号：" + (i_ + 1).ToString("D2"));
           
            ///将用户已购车票全部显示
            
            }
            button5.Visible = true;
            ///使退订按钮显示出来          

        }


        private void button5_Click(object sender, EventArgs e)
        {
           int io = listBox2.SelectedIndex ;
           string unbscribemes = listBox2.SelectedItem.ToString().Split(new char[1] { ' ' })[0];
           train p = link.head;
           while (p.next != link.head)
           {
                   p = p.next;
                  if (p.name == unbscribemes)
                   for (int risks = er.seat[0, 2, io]; risks < er.seat[0, 3, io]; risks++) 
                  {
                       p.carriage[risks,er.seat[0, 0, io], er.seat[0, 1, io]] = true;
                       if (er.seat[0, 0, io] < p.carriage_change)
                           p.carriage_set1[risks]++;
                       else p.carriage_set2[risks]++;
                       er.del(io + 1);
                   }
           }
#region fresh
           listBox2.Items.Clear();
           if (er.i > 0)
           {
               for (int i_ = 0; i_ < er.i; i_++)
                   listBox2.Items.Add(er.message[i_] + "    价格：" + er.cost[i_] + "   " + er.seat[0, 0, i_].ToString("D2") + "车厢 " + er.seat[0, 1, i_].ToString("D3") + "号" + "  单号：" + (i_ + 1).ToString("D2"));
           }
#endregion
        }
        
        
        
        
        public bool 吹雪;///这是一个标识，选票窗口检测到该值为真时即会打开
        public string sendmessage;
        private void button4_Click(object sender, EventArgs e)                            
        {
            if (listBox2.SelectedIndex!=-1)
            {
                sendmessage = listBox2.SelectedItem.ToString().Split(new char[2] {'<','>'})[1];
                ///该字符串为选中车辆的名字，选票窗口会监测这个字符串从而打开特定的车辆的数据
                
                MessageBox.Show(sendmessage);
                吹雪 = true;
                
             }
        }

        private void button6_Click(object sender, EventArgs e)                         
        { 
            info_input foput = new info_input();
            foput.textBox1.Visible = false;
            foput.textBox2.Visible = false;
            foput.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
       
    }
}
