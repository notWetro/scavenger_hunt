using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fe_hunt_gui
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class LoadingCircle : UserControl
    {
        public LoadingCircle()
        {
            InitializeComponent();
        }

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(LoadingCircle), new PropertyMetadata(default(Duration)));

        public Brush SpinnerColor
        {
            get { return (Brush)GetValue(SpinnerColorProperty); }
            set { SetValue(SpinnerColorProperty, value); }
        }

        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(LoadingCircle), new PropertyMetadata(Brushes.DodgerBlue));
    }

}