using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using praktika.Entities;

namespace praktika;

public partial class ProjectAdd : Window
{
    private readonly PraktikaContext _context;
    private readonly Project _project;

    public event Action? ProjectAdded;
    public ProjectAdd(PraktikaContext context, Project project)
    {
        InitializeComponent();
        _context = context;
        _project = project;

        // Заполнение ComboBox клиентами
        cmbClients.ItemsSource = _context.Clients.ToList();
        cmbClients.DisplayMemberBinding = new Avalonia.Data.Binding("ClientName");

        // Заполнение ComboBox статусами
        cmbStatus.ItemsSource = _context.ProjectsStatuses.ToList();
        cmbStatus.DisplayMemberBinding = new Avalonia.Data.Binding("StatusName");
    }
    
    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtProjectName.Text))
        {
            _project.ProjectName = txtProjectName.Text.Trim();
        }
        else
        {
            Console.WriteLine("Заполните все значения");
        }
        _project.StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate?.DateTime ?? DateTime.Now);
        _project.EndDate = dpEndDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value.DateTime) : null;
        _project.ClientInProject = (cmbClients.SelectedItem as Client)?.ClientId;
        _project.Status = (cmbStatus.SelectedItem as ProjectsStatus)?.ProjectStatusId;

        _context.Projects.Add(_project);
        _context.SaveChanges();

        ProjectAdded?.Invoke();
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}