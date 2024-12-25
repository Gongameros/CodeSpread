using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeSpread.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenuButton.xaml
    /// </summary>
    public partial class MainMenuButton : UserControl
    {
        public MainMenuButton()
        {
            InitializeComponent();
        }

        // DependencyProperty for the button text
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                nameof(ButtonText),
                typeof(string),
                typeof(MainMenuButton),
                new PropertyMetadata("Default Text"));

        // DependencyProperty for the command
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(MainMenuButton),
                new PropertyMetadata(null));

        // DependencyProperty for command parameter
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                nameof(CommandParameter),
                typeof(object),
                typeof(MainMenuButton),
                new PropertyMetadata(null));
    }
}
