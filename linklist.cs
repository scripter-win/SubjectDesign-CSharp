using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace 课程设计
{
    public class buyer
    {
       public string [] message = new string[100];
        ///列车数据

       public string [] userinfo = new string[100];
        ///用户数据

       public int [,,] seat=new int[1,4,100];
       ///座位位置

       public int i = 0;
       public double []cost=new double[100];
       public string buyer_info;
       public void del(int io)
        {
            for (; io < i; io++)
            {
                message[io - 1] = message[io];
                seat[0,0,io-1]=seat[0,0,io];
                seat[0, 1, io - 1] = seat[0, 1, io];
                cost[io - 1] = cost[io];
            }
            i--;
        }
    }

    public class train
    {
        public string name;
        public string[,] station = new string[10,2];
        public int time;
        public double[] cost = new double[2];
        public bool [,,]carriage=new bool [10,30,120];
        public int []carriage_sapace=new int [3];
        public int carriage_change;
        public int carriage_end;
        public int []carriage_set1 = new int [10];
        public int []carriage_set2 = new int[10];
        public double []price=new double [2];
        public train front=null;
        public train next=null;
    }

    public class linklist
    {
        station newstation = new station();
        public double cost;
        private string [,]ticket=new string[100,2];
        public train head = new train();
       
        public void create() 
        {
            head.next = head;
            head.front = head;
        }
        
        public  void  data_make()
        {
            int  c = 0, k = 0, i = 0;                int[] checks = new int[1024];   
            int seed = Randseed.randseed;
            train p = head;
            for (int i_ = 0; i_ < 100; i_++)
            {
               
                train target = new train();
                while (p.next != head)
                    p = p.next;
                p.next = target;
                target.next = head;
                target.front = p;
                head.front = target;
                Random rd = new Random();
                i = rd.Next();
                seed = rd.Next()+i;
                    
                    for (int ci = i_; ci> 0; ci--)
                        if (checks[ci] == (i % 900 + 100)) { ci = i_;  i++; }      //防车名重叠算法
                                checks[i_]=(i % 900 + 100);
              
                p.time = 240 + i % 400;
                if ((i+seed) % 2 == 0)
                {
                    target.price[0] = 0.7;
                    target.price[1] = 0.42;
                    target.name = "K" + (i % 900 + 100).ToString();
                    target.carriage_change = i % 10 + 5;          
                    target.carriage_end = 22;
                    target.carriage_sapace[0] = 3 * 20;
                    target.carriage_sapace[1] = 5 * 24;

                    for (c = 0; c < 22; c++)
                        for (k = 0; k < 60 + (c < (i % 15) ? 0 : 60); k++)
                            for (int risk = 0; risk < 10; risk++ )
                                if (((i / 2 + seed) + k * i / 3  + (c * c * k+seed) % 2 + c*k*i%3) % 2 == 0)
                                {
                                    target.carriage[risk, c, k] = true;
                                    if (c < target.carriage_change)
                                        target.carriage_set1[risk]++;
                                    else target.carriage_set2[risk]++;
                                }
                                else target.carriage[risk, c, k] = false;
                }
                else
                {
                    target.name = "G" + (i % 900+100).ToString();
                    target.price[0] = 2.6;
                    target.price[1] = 1.05 +double.Parse((i%2).ToString())/3;
                    target.carriage_change = 4;
                    target.carriage_end = 22;
                    target.carriage_sapace[0] = 4 * 13;
                    target.carriage_sapace[1] = 5 * 13;
             
                         for (c = 0; c < 22; c++)
                            for (k = 0; k < 52 + (c < 4 ? 0 : 13); k++)
                                for (int risk = 0; risk < 10; risk++)
                                if (((i + seed) + k * (k + k * c * i / 200)) % 2 == 0) 
                                {
                                    target.carriage[risk,c, k] = true;
                                    if (c < 4)
                                    target.carriage_set1[risk]++;
                                    else target.carriage_set2[risk]++;
                                }
                                    else target.carriage[risk,c, k] = false;
                }
                switch (i%5)
                {
                    case 0: target.station = newstation.line1; break;
                    case 1: target.station = newstation.line2; break;
                    case 2: target.station = newstation.line3; break;
                    case 3: target.station = newstation.line4; break;
                    case 4: target.station = newstation.line5; break;
                   default: target.station = newstation.line3; break; 
                }
                Thread.Sleep(0);
            }
        }
        
        public  string find(string found)
        {
              int set1,set2;
            string message="";string[]station_found=found.Split(new char[1]{'-'}); int j = 0, i = 0,time1 = 0,time2 = 0;
            train p=head;
            for ( ; p.next != head ;i = 0 ,j = 0 ,time1 = 0,time2 = 0 )
            {
                p = p.next;      int risk1 = 0, risk2 = 0;  

                while (j < 10 && i < 2 && p.station[j, 0] != "")
                {
                    if ((station_found[0] == p.station[j, 0] || station_found[1] == p.station[j, 0]))
                        i++;
                    
                    if (i == 1) 
                    {
                        time2 += int.Parse(p.station[j, 1]);
                        if (risk1 == risk2) { risk1 = j; } else risk2 = j;       //记录站点方位数据
                    }

                    else if (i == 0) time1 += int.Parse(p.station[j, 1]);
                    j++;
                   
                }

                if (p.name[0] == 'K') { time1 *= 2; time2 *= 2; }

                set1 = p.carriage_set1[risk1];
                set2 = p.carriage_set2[risk1];
                for (int risks=risk1;risk1<risk2 ;risk1++ )
                {
                    if (p.carriage_set1[risks + 1] < set1) set1 = p.carriage_set1[risks];
                    if (p.carriage_set1[risks + 1] < set2) set2 = p.carriage_set2[risks];
                }

                    if (i == 2)
                    {
                        message += "<";
                        message += p.name;
                        message += '>';
                        if (p.carriage_change == 4) message += "　一等座：";
                        else message += "　　卧铺：";
                        for (int num = 1000; num >= 10 && num >set1; num /= 10) message += " ";
                        message += set1.ToString("");

                        message += " 价格:";
                        for (int num = 10000; num >= 10 && num > p.price[0] * time2; num /= 10) message += " ";
                        message += (p.price[0] * time2).ToString("F1");
                        p.cost[0] = p.price[0] * time2;

                        if (p.carriage_change == 4) message += "　二等座：";
                        else message += "　　硬座：";
                        for (int num = 10000; num >= 10 && num >set2; num /= 10) message += " ";
                        message += set2.ToString("");

                        message += " 价格:";
                        for (int num = 10000; num >= 10 && num > p.price[1] * time2; num /= 10) message += " ";
                        message += (p.price[1] * time2).ToString("F1");
                        p.cost[1] = p.price[1] * time2;

                        message += "  <出发时间:  ";
                        message += ((time1 + p.time / 60) % 24).ToString("D2"); message += ":";
                        message += ((time1 + p.time) % 60).ToString("D2");

                        message += "  到站时间:";
                        if ((((time1 + p.time / 60) % 24 + time2 / 60 > 24))) message += "次日"; else message += "　　";
                        message += (((time1 + time2 + p.time) / 60) % 24).ToString("D2");
                        message += ":";
                        message += ((time1 + time2 + p.time) % 60).ToString("D2");
                        message += ">-";
                    }
            }
            message+="end-";return message;
        }

        public  string buy(string message)
        {
            string[] messages = message.Split(new char[2] {'<','>'});

            int risk1 = -1, risk2 = -1;         //risk 初始值默认设定为-1，假若设置为0，当出现后续输入值为0时触发bug   
            string info;
            train p = head;
            int start_case, end_case;
            while (p.next != head)
                {
                    p = p.next;
                    if (p.name == messages[3])
                    break;
                }
              for (int risks = 0; risks < 10; risks++)
            {
                if (messages[7] == p.station[risks, 0]||messages[9]==p.station[risks,0]) if (risk1 == risk2) risk1 = risks; else risk2 = risks;
            }

              bool[,] carriage = new bool [30, 120];
            
                int space;
                if (messages[1] == "一等座")
                {
                    start_case = 0; end_case = p.carriage_change;
                    space = p.carriage_sapace[0];
                }
                else
                {
                    start_case = p.carriage_change; end_case = 22;
                    space = p.carriage_sapace[1];
                }
                for (int i = start_case; i < end_case; i++)
                    for (int j = 0; j < space; j++)
                    { 
                        for(int risk=risk1;risk<=risk2;risk++)
                        { 
                            carriage[i,j]=true;
                            if (p.carriage[risk, i, j] == false) { carriage[i, j] = false; break; }
                        }
                            
                            int risks=0;
                            if (carriage[i, j] == true)
                            {
                            if (messages[1] == "一等座")
                            {
                                for (risks = risk1; risks <= risk2; risks++)
                                { p.carriage_set1[risks]--; p.carriage[risks, i, j] = true; }
                                info = p.cost[0].ToString("F0") + "," + (i + 1).ToString() + "," + (j + 1).ToString() + "," + messages[3] + "    " + messages[5];
                            }
                            else
                            {
                                for (risks = risk1; risks <= risk2; risks++)
                                { p.carriage_set2[risks]--; p.carriage[risks, i, j] = true; }
                                info = p.cost[1].ToString("F0") + "," + (i + 1).ToString() + "," + (j + 1).ToString() + "," + messages[3] + "    " + messages[5];
                            }
                            info += ("," + risk1 + "," + risk2);
                            return info;
                            }
                        }
            return "已无余票！";
            }

    }

        public class station
        {
            public string[,] line1 = new string[10, 2] { { "哈尔滨", "77" }, { "长春", "68" }, { "沈阳", "43" }, { "北京", "33" }, { "天津", "25" }, { "济南", "48" }, { "徐州", "29" }, { "南京", "46" }, { "上海", "22" }, { "杭州", "0" } };

            public string[,] line2 = new string[10, 2] { { "哈尔滨", "78" }, { "长春", "69" }, { "沈阳", "39" }, { "北京", "32" }, { "太原", "39" }, { "郑州", "39" }, { "武汉", "70" }, { "长沙", "42" }, { "广州", "15" }, { "深圳", "0" } };

            public string[,] line3 = new string[10, 2] { { "兰州", "74" }, { "西安", "75" }, { "郑州", "90" }, { "武汉", "103" }, { "长沙", "63" }, { "永州", "112" }, { "广州", "15" }, { "深圳", "0" }, { "", "" }, { "", "" } };

            public string[,] line4 = new string[10, 2] { { "兰州", "76" }, { "西安", "78" }, { "成都", "77" }, { "重庆", "31" }, { "昆明", "45" }, { "贵阳", "42" }, { "南宁", "0" }, { "", "0" }, { "", "" }, { "", "" } };

            public string[,] line5 = new string[10, 2] { { "乌鲁木齐", "150" }, { "拉萨", "170" }, { "西宁", "108" }, { "银川", "144" }, { "呼和浩特", "180" }, { "北京", "170" }, { "西安", "0" }, { "", "0" }, { "", "" }, { "", "" } };

            public string[,] line = new string[10, 2];
        }
    }
 #if debug
//-显示所有信息  code for check 》》》
        public string show_all()
        {
            string str="";
            train p = head;
            while (p.next != head)
            {
                p = p.next;
                str += p.name;
                if (p.carriage_change == 4) str += "  一等座：";else str += "    卧铺：";
                str += p.carriage_set1;
                if (p.carriage_change == 4) str += "  二等座：";else str += "    硬座：";
                str += p.carriage_set2;
                for (int i = 0; i < 10 && p.station[i, 0] != "";str+=" " ,i++)
                    str += p.station[i, 0];
                str += "\n";      
            }
            str += "e";
            return str;
        }           
    
        
        
        //字符串统计座位数算法  -  》》
        
            if (messages[1] != "三")
            {            
        else
            {
                string seatinfo = ""; int c = 0, sapace;
                while (p.next != head)
                {
                    p = p.next;
                    if (p.name == messages[3])
                    {
                        for (int i = 0; i < 22; i++)
                        {
                            if (i < p.carriage_change) sapace = p.carriage_sapace[0]; else sapace = p.carriage_sapace[1];
                            for (int j = 0; j < sapace; j++)
                            {
                                if (p.carriage[i, j] == 1)
                                {
                                    while (p.carriage[i, j] == 1 && j < sapace) { c++; j++; }
                                    seatinfo += c;
                                    c = 0;
                                }
                                else
                                {
                                        while (p.carriage[i, j] == 0 && j < sapace) { c--; j++; }
                                        seatinfo += c;
                                        c = 0;
                                }
                            } 
                        }
                        if (seatinfo != "") return seatinfo; else return "system error！";
                    } 
                }
            }
    
#endif
