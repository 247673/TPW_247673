using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prezentacja
{

    public class Model : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _radius;
        private double _velocityX;
        private double _velocityY;

        public double X
        {
            get { return _x; }
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        public double Radius
        {
            get { return _radius; }
            set { _radius = value; OnPropertyChanged(nameof(Radius)); }
        }

        public double VelocityX
        {
            get { return _velocityX; }
            set { _velocityX = value; OnPropertyChanged(nameof(VelocityX)); }
        }

        public double VelocityY
        {
            get { return _velocityY; }
            set { _velocityY = value; OnPropertyChanged(nameof(VelocityY)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
