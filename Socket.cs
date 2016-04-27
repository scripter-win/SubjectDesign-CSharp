using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace 课程设计
{
    public class Socket_cs
    {
        public string ip = "169.254.53.224";
        public int port = 23078;
        public bool 朝露;
        Socket socket_bind = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void bind()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
            socket_bind.Bind(point);
            socket_bind.Listen(15);
        }

        public int send(string send_message)
        {
            try
            {
                Socket SEND = socket_bind.Accept();
                byte[] mesbyte = Encoding.Unicode.GetBytes(send_message);
                SEND.Send(mesbyte, mesbyte.Length, 0);
                return 1;
            }
            catch (Exception e) { return 0;}
        }
        public string get()
        {
            try
            {
                Socket GET = socket_bind.Accept();
                byte[] msbyte = new byte[1024];
                GET.Receive(msbyte, msbyte.Length, 0);
                string message = Encoding.Unicode.GetString(msbyte, 0, msbyte.Length);
                return message;
            }
            catch (Exception e) { return e.ToString(); }
        }
    }
    }


