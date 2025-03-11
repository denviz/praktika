using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using praktika.Entities;

namespace praktika;

public partial class ProjectEdit : Window
{
    private readonly PraktikaContext _context;
    private readonly Project _project;

    public event Action? ProjectUpdated;
    public ProjectEdit(PraktikaContext context, Project project)
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

        // Заполнение полей данными
        txtProjectName.Text = _project.ProjectName;
        dpStartDate.SelectedDate = _project.StartDate.ToDateTime(TimeOnly.MinValue);
        dpEndDate.SelectedDate = _project.EndDate?.ToDateTime(TimeOnly.MinValue);
        cmbClients.SelectedItem = _context.Clients.FirstOrDefault(c => c.ClientId == _project.ClientInProject);
        cmbStatus.SelectedItem = _context.ProjectsStatuses.FirstOrDefault(s => s.ProjectStatusId == _project.Status);
    }
    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        _project.ProjectName = txtProjectName.Text;
        _project.StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate?.DateTime ?? DateTime.Now);
        _project.EndDate = dpEndDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value.DateTime) : null;
        _project.ClientInProject = (cmbClients.SelectedItem as Client)?.ClientId;
        _project.Status = (cmbStatus.SelectedItem as ProjectsStatus)?.ProjectStatusId;

        _context.SaveChanges();
        ProjectUpdated?.Invoke();
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
    
}