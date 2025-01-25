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

        public string IconSource 
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register(
                nameof(IconSource),
                typeof(string),
                typeof(MainMenuButton),
                new PropertyMetadata("Default Text"));

        public string SubText 
        {
            get { return (string)GetValue(SubTextProperty); }
            set { SetValue(SubTextProperty, value); }
        }

        public static readonly DependencyProperty SubTextProperty =
            DependencyProperty.Register(
                nameof(SubText),
                typeof(string),
                typeof(MainMenuButton),
                new PropertyMetadata("Default Text"));

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
