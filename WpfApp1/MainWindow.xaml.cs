using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> activeTasks { get; set; }
        public ObservableCollection<Task> completedTasks { get; set; }
        public MainWindow()
        {
            InitializeComponent();
           
            activeTasks=new ObservableCollection<Task>();
            completedTasks=new ObservableCollection<Task>();
            DataContext = this;

            SetPlaceholder();
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTask.Text) && tbTask.Text != "Enter a task:")
            {
                if(activeTasks.Any(s => s.Text == tbTask.Text))
                {
                    string message = "Task is already in the list\n" +
                        "Would you still like to add the task?";
                    MessageBoxResult result= MessageBox.Show(message,"",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                    if(result==MessageBoxResult.No)
                        return;


                }
                string data = tbTask.Text;
                Task task = new Task(data);
                activeTasks.Add(task);

                //CheckBox box = new CheckBox();
               // box.Content = task.Text;
               // box.Tag = task;
               // CheckBoxListBox.Items.Add(box);
                tbTask.Clear();
                SetPlaceholder();
            }
        }

        private void tbTask_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbTask.Text == "Enter a task:")
            {
                tbTask.Text = "";
                tbTask.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void tbTask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTask.Text))
            {
                SetPlaceholder();
            }
           
        }
        private void SetPlaceholder()
        {
            tbTask.Text = "Enter a task:";
            tbTask.Foreground=new SolidColorBrush(Colors.DimGray);
        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var taskToRemove = activeTasks.Where(task => task.Completed).ToList();

            foreach(var t in taskToRemove)
            {
                activeTasks.Remove(t);
            }
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            for(int i=activeTasks.Count-1; i>=0; i--)
            {
                if (activeTasks[i].Completed)
                {
                    completedTasks.Add(activeTasks[i]);
                    activeTasks.RemoveAt(i);
                }
            }
        }

        private void tbTask_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
               
                Button_Click(sender, e);
                tbTask.Clear();
            }
        }

        private void CheckBoxListBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btnFinish_Click(sender, e);
                    return;
                case Key.Delete:
                    btnRemove_Click(sender, e);
                    return;
                default : break;
            }
        }
    }
}
