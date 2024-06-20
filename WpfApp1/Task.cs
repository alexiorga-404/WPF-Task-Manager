using System.ComponentModel;

public class Task : INotifyPropertyChanged
{
    private string text;
    private bool completed;

    public string Text
    {
        get { return text; }
        set
        {
            text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    public bool Completed
    {
        get { return completed; }
        set
        {
            completed = value;
            OnPropertyChanged(nameof(Completed));
        }
    }

    public Task(string text)
    {
        Text = text;
        Completed = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
