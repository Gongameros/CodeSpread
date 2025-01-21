using CodeSpread.Base;
using CodeSpread.Decompiler.Models;
using CodeSpread.Utils;
using CodeSpread.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CodeSpread;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private const double MinWidth = 700;
    private const double MinHeight = 600;
    private bool _isResizing = false;
    private Point _startPoint;
    private ResizeDirection _resizeDirection;

    private object _currentView { get; set; }
    public object CurrentView
    {
        get { return _currentView; }
        set
        {
            if (_currentView != value)
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); // Notify the UI of the change
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public MainWindow()
    {
        DataContext = this;
        //// Set initial view to StartupView
        CurrentView = new StartupView();

        InitializeComponent();

    }

    // Method to handle file selection (triggered on file selection in StartupView)
    public void OnFileSelected(AssemblyInformation assemblyInformation) => CurrentView = new DecompileView(assemblyInformation);

    private void Resize_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Border border)
        {
            _isResizing = true;
            _startPoint = e.GetPosition(this);
            _resizeDirection = GetResizeDirection(border.Name);
            border.CaptureMouse();
        }
    }

    private void Resize_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isResizing)
        {
            var currentPosition = e.GetPosition(this);
            var offsetX = currentPosition.X - _startPoint.X;
            var offsetY = currentPosition.Y - _startPoint.Y;

            switch (_resizeDirection)
            {
                case ResizeDirection.Left:
                    if (this.Width - offsetX >= MinWidth)
                    {
                        this.Width -= offsetX;
                        this.Left += offsetX;
                    }
                    break;
                case ResizeDirection.Right:
                    if (this.Width + offsetX >= MinWidth)
                    {
                        this.Width += offsetX;
                    }
                    break;
                case ResizeDirection.Top:
                    if (this.Height - offsetY >= MinHeight)
                    {
                        this.Height -= offsetY;
                        this.Top += offsetY;
                    }
                    break;
                case ResizeDirection.Bottom:
                    if (this.Height + offsetY >= MinHeight)
                    {
                        this.Height += offsetY;
                    }
                    break;
            }

            _startPoint = currentPosition;
        }
    }


    private void Resize_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is Border border)
        {
            _isResizing = false;
            border.ReleaseMouseCapture();
        }
    }

    private ResizeDirection GetResizeDirection(string borderName)
    {
        return borderName switch
        {
            "ResizeBorderLeft" => ResizeDirection.Left,
            "ResizeBorderRight" => ResizeDirection.Right,
            "ResizeBorderTop" => ResizeDirection.Top,
            "ResizeBorderBottom" => ResizeDirection.Bottom,
            _ => throw new InvalidOperationException("Invalid resize border name."),
        };
    }

}