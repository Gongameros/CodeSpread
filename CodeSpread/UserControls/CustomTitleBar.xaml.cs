using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeSpread.UserControls;

/// <summary>
/// Interaction logic for CustomTitleBar.xaml
/// </summary>
public partial class CustomTitleBar : UserControl
{
    private readonly Window MainWindowAlias;

    public CustomTitleBar()
    {
        InitializeComponent();
        MainWindowAlias = Application.Current.MainWindow;
    }

    private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
    {
        MainWindowAlias.WindowState = WindowState.Minimized;
    }

    private void MaximizeWindow_Click(object sender, RoutedEventArgs e)
    {
        MainWindowAlias.WindowState = MainWindowAlias.WindowState == WindowState.Maximized
            ? WindowState.Normal : WindowState.Maximized;
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        MainWindowAlias.Close();        
    }

    private void ToolBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            double maxHeight = ToolBarMainGrid.MaxHeight;

            // Restore if maximized
            if (MainWindowAlias.WindowState == WindowState.Maximized)
            {
                MainWindowAlias.WindowState = WindowState.Normal;

                // Adjust the position of form for the mouse offset
                var mousePosition = e.GetPosition(MainWindowAlias);
                MainWindowAlias.Left = mousePosition.X - (MainWindowAlias.ActualWidth / 2);
                MainWindowAlias.Top = mousePosition.Y - (maxHeight / 2);
            }

            // Drag the window
            MainWindowAlias.DragMove();
        }
    }
}
