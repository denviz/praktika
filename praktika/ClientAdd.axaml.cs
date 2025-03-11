using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using praktika.Entities;

namespace praktika;

public partial class ClientAdd : Window
{
    private readonly PraktikaContext _context;
    private readonly Client _client;

    public event Action? ClientAdded;
    public ClientAdd(PraktikaContext context, Client client)
    {
        InitializeComponent();
        _context = context;
        _client = client;
    }
    
    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (txtClientName.Text != null || txtClientEmail.Text != null || txtClientPhone.Text != null)
        {
            _client.ClientName = txtClientName.Text;
            _client.ClientEmail = txtClientEmail.Text;
            _client.ClientPhone = txtClientPhone.Text;

            _context.Clients.Add(_client);
            _context.SaveChanges();

            ClientAdded?.Invoke();
            Close();
        }
        else
        {
            Console.WriteLine("Введите все значения");
        }
        
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}