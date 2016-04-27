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
    public partial class seat_choose : Form
    {
        private train p = new train();
        private Button[,] button = new Button[24, 5];
        private Size size = new Size(50,20);
        private int[,] seats = new int[100, 2];
        
        private bool [,]carriage= new bool [30,120];
        private int risk1 = 0, risk2 = 0;

        private string sendmessage;
        
        public main_window mic = new main_window();
        public seat_choose()///窗体构造函数
        {
            InitializeComponent();

           
            mic.Show(); this.Hide();
            ///显示主界面，并且该界面隐藏待机

            
            p = null;
            timer1.Interval = 100;
            timer1.Enabled = true;
            button1.Enabled = false;     
            ///一些起始定义

            for (int i = 0; i < 24; i++)
            for (int j = 0; j < 5; j++)
            {
               button[i, j] = new Button();
               this.Controls.Add(button[i, j]);
               button[i, j].Click += new EventHandler(button_Click);
               button[i, j].Size = size;
               button[i, j].Top = i * 20 + 70;
               button[i, j].Left = j * 80 + 90;
            }
            ///定义一系列按钮对象，并添加进controls里，作用是用来显示座位号，同时可以让用户点击
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mic.吹雪 == true)
            {
                mic.吹雪 = false;
                timer2.Enabled = true;
                timer2.Interval = 1;
            }
            label2.Text = (site + 1).ToString("D2");
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            if (sendmessage != mic.sendmessage)
            {
                sendmessage = mic.sendmessage;
                richTextBox1.Clear();
            } sendmessage = mic.sendmessage;
            
                risk1 = 0; risk2 = 0;
                p=mic.link.head;
                while (p.next != mic.link.head)
                {
                    p = p.next;
                    if (p.name == mic.sendmessage) break;
                }

                  for (int risks = 0; risks < 10; risks++)
            {
                if (mic.textBox1.Text== p.station[risks, 0]||mic.textBox2.Text==p.station[risks,0])
                if (risk1 == risk2) risk1 = risks; else risk2 = risks;
            }
            for(int i=0;i<22;i++)
                for(int j=0;j<120;j++)
                {
                    carriage[i,j]=true;
                    for(int risks=risk1;risks<=risk2;risks++)
                        if (p.carriage[risks, i, j] == false) { carriage[i, j] = false; break; }
                }
               
            for (int i = 0; i < 24; i++)
                    for (int j = 0; j < 5; j++)
                    {
                        button[i, j].Visible = false;
                        button[i, j].Enabled = false;
                    }
                #region buttonfind
                height_length_set();
                for (int i = 0; i < set_het; i++)
                    for (int j = 0; j < set_len; j++)
                    {
                        button[i, j].Text = (i * set_len + j + 1).ToString("D3");
                        button[i, j].Visible = true;
                        if (carriage[site, i * set_len + j] == true)
                            button[i, j].Enabled = true;
                    }
                #endregion
                pictureBox1.Visible = false; pictureBox3.Visible = false;
                车厢显示();
                this.Show();
            timer2.Enabled = false;
        }

        public int site=0;
        public int set_het; public int set_len;
        public int ii= 0;


        void button_Click(object sender ,EventArgs e)
        ///该函数是定义的座位按钮函数，作为用户点击的反馈，被点击时，储存下数据，并使该按钮变为不可用 
        {
            richTextBox1.Text += (label2.Text+"车厢"+(sender as Button).Text+"号\n");
            ///信息栏显示用户选择

            (sender as Button).Enabled = false;
            ///按钮不可点击
            
            carriage[int.Parse(label2.Text)-1,int .Parse((sender as Button).Text)-1]=false;
            seats[ii,0]=int.Parse(label2.Text)-1;
            seats[ii,1]=int .Parse((sender as Button).Text)-1;
            ii++;
            ///数据传递

        }

        ///
        
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i=0;i<5*24;i++)
                {
                    button[i/5, i%5].Visible = false;
                    button[i/5, i%5].Enabled = false;
                }
            ///座位信息全部清空，等待接下来的输入
            
            site--;
            ///标志车厢位置的site减少一

            #region buttonfind
            height_length_set();
            ///处理座位横向数量，与列向数量的函数

            for (int i = 0; i < set_het; i++)
                for (int j = 0; j < set_len; j++)
                {
                    button[i, j].Text = (i * set_len + j + 1).ToString("D3");
                    button[i, j].Visible = true;
                    if (carriage[site, i * set_len + j] == true)
                        button[i, j].Enabled = true;
                }
            ///给按钮分配该车厢的数据，从而生成座位


            #endregion
            if (site < 1)
                button1.Enabled = false;
            if (site <= 21)
                button2.Enabled = true;
            ///以上函数的作用是当车厢到了尽头时，不能前往前一列车厢或后一列车厢



            车厢显示();

            ///该函数是显示一些文本，比如12车厢，然后是硬座车厢

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 24*5; i++)
                {
                    button[i/5, i%5].Visible = false;
                    button[i/5, i%5].Enabled = false;
                }
            site++;

            #region buttonfind
            height_length_set();
            for (int i = 0; i < set_het; i++)
                for (int j = 0; j < set_len; j++)
                {
                    button[i, j].Visible = true;
                    button[i, j].Text = (i * set_len + j + 1).ToString("D3");
                    if (carriage[site, i * set_len + j] == true)
                        button[i, j].Enabled = true;
                }
            #endregion

            if (site >= 0)
                button1.Enabled = true;
            if (site == 21)
                button2.Enabled = false;
                 车厢显示();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ii = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            while (ii > 0)
            {
                if (seats[ii, 0] < p.carriage_change)///判断是卧铺/一等座车厢 还是 坐票/一等座车厢
                {
                    mic.er.cost[mic.er.i] = p.cost[0];
                ///将数据存入用户已购车票的数据中，该数据是票价系数，与行车时间一起决定车票票价

                    for (int risks = risk1; risks <= risk2; risks++)
                    {
                        p.carriage_set1[risks]--;
                        p.carriage[risks, seats[ii, 0], seats[ii, 1]] =false; 
                    }
                ///改变座位的状态，表示已经被购买，不能再买
                }
               
                
                else
                {
                    mic.er.cost[mic.er.i] = p.cost[1];
                    for (int risks = risk1; risks <= risk2; risks++) 
                    { 
                        p.carriage_set2[risks]--; 
                        p.carriage[risks, seats[ii, 0], seats[ii, 1]] = false; 
                    }
                }
                ///与上面if（）条件判断通过时的代码等同



                mic.er.seat[0,0,mic.er.i]=seats[ii,0] + 1;
                mic.er.seat[0,1,mic.er.i]=seats[ii,1] + 1;
		        mic.er.seat[0,2,mic.er.i]=risk1;
		        mic.er.seat[0,3,mic.er.i]=risk2;
                mic.er.message[mic.er.i++] = mic.listBox2.SelectedItem.ToString().Split(new char[2] { '<', '>' })[1] + "    " 
                + mic.listBox2.SelectedItem.ToString().Split(new char[2] { '<', '>' })[3]+ "     " + mic.textBox1.Text + "->" + mic.textBox2.Text;
                ii--;
                ///数据完全写入用户购票数据中

            }
                ii = 0;
                this.Hide();
            ///标志归位，该界面隐藏，返回主界面

        }
  
          private void  height_length_set()
        {
            if (p.carriage_change != 4)
            {
                if (site < p.carriage_change)
                {
                    set_het = 20;
                    set_len = 3;
                }
                else
                {
                    set_het = 24;
                    set_len = 5;
                }
            }
            else
            {
                if (site < p.carriage_change)
                {
                    set_het = 13;
                    set_len = 4;
                }
                else
                {
                    set_het = 13;
                    set_len = 5;
                }
            }
        }

          private void 车厢显示()
          {
              if (mic.sendmessage[0] == 'G')
              {
                  label3.Text = site < p.carriage_change ? "一等座车厢" : "二等座车厢";
                  {
                      pictureBox3.Visible = false;
                      pictureBox1.Visible = true;
                  }
              }
              else
              {
                  label3.Text = site < p.carriage_change ? "卧铺车厢" : "硬座车厢";
                  if (site < p.carriage_change)
                      pictureBox3.Visible = true;
                  else pictureBox3.Visible = false;
              }
          }

    }
}
