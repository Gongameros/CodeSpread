using System.ComponentModel;
using System.Windows.Controls;

namespace CodeSpread.Views
{
    /// <summary>
    /// Interaction logic for LayoutView.xaml
    /// </summary>
    public partial class LayoutView : UserControl
    {
        private object _navbarMenuComponent;
        public object NavbarMenuComponent
        {
            get { return _navbarMenuComponent; }
            set
            {
                if (_navbarMenuComponent != value)
                {
                    _navbarMenuComponent = value;
                    OnPropertyChanged(nameof(NavbarMenuComponent)); // Notify the UI of the change
                }
            }
        }

        public LayoutView()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
