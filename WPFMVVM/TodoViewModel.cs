using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFMVVM
{
    public class TodoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> Items { get; set; }

        private string _newTaskTitle;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged();
            }
        }

        private TodoItem _selectedTask;
        public TodoItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }

        public TodoViewModel()
        {
            Items = new ObservableCollection<TodoItem>();
            AddTaskCommand = new RelayCommand(AddTask);
            RemoveTaskCommand = new RelayCommand(RemoveTask);
        }

        private void AddTask(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                Items.Add(new TodoItem { Title = NewTaskTitle });
                NewTaskTitle = string.Empty;
            }
        }

        private void RemoveTask(object parameter)
        {
            if (parameter is TodoItem task && Items.Contains(task))
            {
                Items.Remove(task);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
