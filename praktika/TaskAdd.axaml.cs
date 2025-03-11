using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using praktika.Entities;

namespace praktika;

public partial class TaskAdd : Window
{
    private readonly PraktikaContext _context;
    private readonly Task _task;

    public event Action? TaskAdded;
    public TaskAdd(PraktikaContext context, Task task)
    {
        InitializeComponent();
        _context = context;
        _task = task;

        // Заполнение ComboBox проектами
        cmbProjects.ItemsSource = _context.Projects.ToList();
        cmbProjects.DisplayMemberBinding = new Avalonia.Data.Binding("ProjectName");

        // Заполнение ComboBox сотрудниками
        cmbEmployees.ItemsSource = _context.Employees.ToList();
        cmbEmployees.DisplayMemberBinding = new Avalonia.Data.Binding("LastName");
    }
    
    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        _task.TaskName = txtTaskName.Text;
        _task.Description = txtDescription.Text;
        _task.Deadline = DateOnly.FromDateTime(dpDeadline.SelectedDate?.DateTime ?? DateTime.Now);
        _task.ProjectInTask = (cmbProjects.SelectedItem as Project)?.ProjectId;
        _task.AssignedTo = (cmbEmployees.SelectedItem as Employee)?.EmployeeId;

        _context.Tasks.Add(_task);
        _context.SaveChanges();
        
        TaskAdded?.Invoke();
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}