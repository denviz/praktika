using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using praktika.Entities;

namespace praktika;

public partial class EmployeeAdd : Window
{
    private readonly PraktikaContext _context;
    private readonly Employee _employee;

    public event Action? EmployeeAdded;
    
    public EmployeeAdd(PraktikaContext context, Employee employee)
    {
        InitializeComponent();
        _context = context;
        _employee = employee;
    }
    
    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (txtLastName.Text != null || txtFirstName.Text != null || txtPosition.Text != null ||
            txtEmail.Text != null || txtPhone.Text != null)
        {
            _employee.LastName = txtLastName.Text.Trim();
            _employee.FirstName = txtFirstName.Text.Trim();
            _employee.Position = txtPosition.Text.Trim();
            _employee.EmpEmail = txtEmail.Text.Trim();
            _employee.EmpPhone = txtPhone.Text.Trim();

            _context.Employees.Add(_employee);
            _context.SaveChanges();

            EmployeeAdded?.Invoke();
            Close();
        }
        else
        {
            Console.WriteLine("Заполните все значения");
        }
        
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}