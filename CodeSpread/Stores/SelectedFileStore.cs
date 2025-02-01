namespace CodeSpread.Stores;

public class SelectedFileStore
{
    private string _selectedFile;
    public string SelectedFile
    {
        get => _selectedFile;
        set
        {
            _selectedFile = value;
            CurrentFileChanged?.Invoke();
        }
    }

    public event Action CurrentFileChanged;

    public void ClearSelectedFile()
    {
        SelectedFile = null;
    }
}
