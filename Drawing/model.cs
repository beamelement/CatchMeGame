using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchMe
{

    //创建数据源，在数据发生改变的时候给Binding一个通知
    class model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int score;


        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                if(this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Score"));
                }
            }
        }
    }
}
