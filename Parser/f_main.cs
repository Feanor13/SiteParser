using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using System.Threading;
using HtmlAgilityPack;


namespace Parser
{
    public partial class f_main : Form
    {
        //Домен
        private const string main_url = "http://www.003.ru/krupnaja_i_vstraivaemaja_bytovaja_tehnika-1/gazovye_i_elektricheskie_plity-3/gazovye_plity/_gefest";
        // Потокобезопасная запись данных в TXT_Output
        public delegate void add_text(string str);
        public add_text my_delegate;
        public void add_text_method(string str)
        {
            txt_output.Text += str;
        }
        public delegate void set_text(string[] ar);
        public set_text a_delegate;
        public void set_text_method(string[] ar)
        {
            txt_output.Lines = ar;
        }


        public f_main()
        {
            InitializeComponent();
        }
        public string getRequest(string url)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.AllowAutoRedirect = false;//Запрещаем автоматический реддирект 
                httpWebRequest.Method = "GET"; //Можно не указывать, по умолчанию используется GET. 
                httpWebRequest.Referer = "http://rkn.gov.ru/"; // Реферер. Тут можно указать любой URL
                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (var stream = httpWebResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding(httpWebResponse.CharacterSet)))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                return String.Empty;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string content = getRequest(main_url);
            string urll; 
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            for (int i=2;i<13;i++)
            {

                urll = main_url + "/";
                urll    += i.ToString();
                content += getRequest(urll);
            }
                textBox1.Text = "!!!!!!!";
                textBox1.Text = content;
                

            /* HtmlNodeCollection c = doc.DocumentNode.SelectNodes(".//*[@id='priceForm']/div/div/div");
            foreach (HtmlNode n in c)
            {
               
                textBox1.Text = n.ToString();
            
                
            }*/
        }
    }
}
