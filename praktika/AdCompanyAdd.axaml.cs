using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using praktika.Entities;

namespace praktika;

public partial class AdCompanyAdd : Window
{
    private readonly PraktikaContext _context;
    private readonly AdCompany _adCompany;

    public event Action? AdCompanyAdded;
    public AdCompanyAdd(PraktikaContext context, AdCompany adCompany)
    {
        InitializeComponent();
        _context = context;
        _adCompany = adCompany;

        // Заполнение ComboBox проектами
        cmbProjects.ItemsSource = _context.Projects.ToList();
        cmbProjects.DisplayMemberBinding = new Avalonia.Data.Binding("ProjectName");
    }
    
    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        _adCompany.CompanyName = txtCompanyName.Text;
        _adCompany.Platform = txtPlatform.Text;
        _adCompany.Budget = decimal.Parse(txtBudget.Text);
        _adCompany.StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate?.DateTime ?? DateTime.Now);
        _adCompany.EndDate = dpEndDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value.DateTime) : null;
        _adCompany.ProjectInCompany = (cmbProjects.SelectedItem as Project)?.ProjectId;

        _context.AdCompanies.Add(_adCompany);
        _context.SaveChanges();

        AdCompanyAdded?.Invoke();
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}