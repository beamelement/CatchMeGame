using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatchMe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        model model = new model();

        Timer timer = new Timer();

        //开始时间
        DateTime DataTimeStart = DateTime.Now;
        //现在时间
        DateTime DataTimeNow = DateTime.Now;
        //点击时间
        DateTime DataTimeClick = DateTime.Now;


        public MainWindow()
        {
            InitializeComponent();

            //创建分数的binding
            Binding binding = new Binding() { Source = model };
            this.SP.SetBinding(StackPanel.DataContextProperty, binding);

            //创建timer 来更新时间

            timer.Interval = 100;
            timer.Start();
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_ChangePos);
            timer.AutoReset = true;
        }


        //规定时间没有抓到，弹出对话框，清空
        private void Timer_ChangePos(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            DataTimeNow = DateTime.Now;
            

            TimeSpan timeSpan = DataTimeNow - DataTimeClick;

            if (timeSpan.TotalMilliseconds > 3000)
            {
                MessageBox.Show("抓不住我吧");

                if(MessageBox.Show("抓不住我吧，要不要再来一次","嘿嘿嘿",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //分数重置
                    model.Score = 0;
                    //时间重置
                    DataTimeNow = DateTime.Now;
                    DataTimeClick = DateTime.Now;
                    timer.Start();
                }

            }
            else
            {
                timer.Start();
            }

            

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //创建动态动画参数  DoubleAnimation 包含起点、终点、事件间隔
            DoubleAnimation DX = new DoubleAnimation();
            DoubleAnimation DY = new DoubleAnimation();
            //DX.From = 0.0;
            //DY.From = 0.0;

            Random random = new Random();
            DX.To = random.NextDouble() * 300;
            DY.To = random.NextDouble() * 300;

            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
            DX.Duration = duration;
            DY.Duration = duration;

            //给界面中的TranslateTransform赋予要移动动态动画参数
            this.TT.BeginAnimation(TranslateTransform.XProperty, DX);
            this.TT.BeginAnimation(TranslateTransform.YProperty, DY);

            //分数变化
            model.Score += 1;


            //点击按钮时间
            DataTimeClick = DateTime.Now;



        }
    }
}
