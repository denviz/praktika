using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using praktika.Entities;

namespace praktika;

public partial class TablesMain : Window
{
    private PraktikaContext _context;
    public List<Project> Projects { get; set; }
    public List<Client> Clients { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Task> Tasks { get; set; }
    public List<AdCompany> AdCompanies { get; set; }
    public List<Report> Reports { get; set; }
    public List<Specification> Specifications { get; set; }
    public List<ProjectsStatus> ProjectsStatuses { get; set; }
    public TablesMain()
    {
        InitializeComponent();
        _context = new PraktikaContext();
        LoadTableNames();
        cmbTables.SelectedIndex = 0; // Загрузить первую таблицу по умолчанию
        LoadDefaultTable();
    }
    // загрузка названий таблиц в cmb
    private void LoadTableNames()
    {
        var tableNames = new List<string>
        {
            "Projects", "Clients", "Employees", "Tasks", "AdCompanies", "Reports", "Specifications", "ProjectsStatus"
        };
        var cmbTablesList = this.FindControl<ComboBox>("cmbTables");
        cmbTablesList.ItemsSource = tableNames;
    }
    
    // при изменении значения cmb загружается соответственные таблица и фильтры
    private void cmbTables_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        LoadDefaultTable();
    }
    
    // для уменьшения дублирования кода
    private void VisibleFilterAndButton(bool visibleFilter=true, bool visibleButtonCreate=true, bool visibleButtonEdit = false)
    {
        PanelFilter.IsVisible = true;
        if (visibleFilter)
        {
            txtFilter.IsVisible = true;
            cmbFilterProjects.IsVisible = false;
            cmbFilterEmployees.IsVisible = false;
        }
        else
        {
            txtFilter.IsVisible = false;
            cmbFilterProjects.IsVisible = true;
            cmbFilterEmployees.IsVisible = false;
        }
        
        if (visibleButtonCreate)
        {
            btnCreate.IsVisible = true;
            btnEdit.IsVisible = true;
        }
        else
        {
            btnCreate.IsVisible = false;
        }

        if (visibleButtonEdit)
        {
            btnEdit.IsVisible = true;
        }
        else
        {
            btnEdit.IsVisible = false;
        }
    }
    
    // вспомогательная загрузка таблиц
     private void ConfigureDataGrid(string tableName)
    {
        dataGridContent.Columns.Clear(); // Очистить старые колонки
        cmbFilterProjects.SelectedItem = null;
        cmbFilterEmployees.SelectedItem = null;
        txtFilter.Clear();
        switch (tableName)
        {
            case "Projects":
                VisibleFilterAndButton(visibleButtonEdit:true);
                txtFilter.Watermark = "Введите название проекта";
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("ProjectId"), Width = DataGridLength.Auto});
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Avalonia.Data.Binding("ProjectName") });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Клиент", Binding = new Avalonia.Data.Binding("ClientInProjectNavigation.ClientName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Дата начала", Binding = new Avalonia.Data.Binding("StartDate"), Width = DataGridLength.Auto});
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Дата завершения", Binding = new Avalonia.Data.Binding("EndDate"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Статус", Binding = new Avalonia.Data.Binding("StatusNavigation.StatusName"), Width = DataGridLength.Auto });
        
                Projects = _context.Projects
                    .Include(p => p.ClientInProjectNavigation)
                    .Include(p => p.StatusNavigation)
                    .OrderBy(p=> p.ProjectId)
                    .ToList();
                dataGridContent.ItemsSource = Projects;
                break;

            case "Clients":
                VisibleFilterAndButton();
                txtFilter.Watermark = "Введите название организации";
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("ClientId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Имя", Binding = new Avalonia.Data.Binding("ClientName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Email", Binding = new Avalonia.Data.Binding("ClientEmail"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Телефон", Binding = new Avalonia.Data.Binding("ClientPhone"), Width = DataGridLength.Auto });

                Clients = _context.Clients
                    .OrderBy(c=> c.ClientId)
                    .ToList();
                dataGridContent.ItemsSource = Clients;
                break;

            case "Employees":
                VisibleFilterAndButton();
                txtFilter.Watermark = "Введите фамилию сотрудника";
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("EmployeeId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Фамилия", Binding = new Avalonia.Data.Binding("LastName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Имя", Binding = new Avalonia.Data.Binding("FirstName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Отчество", Binding = new Avalonia.Data.Binding("MiddleName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Должность", Binding = new Avalonia.Data.Binding("Position") });

                Employees = _context.Employees
                    .OrderBy(e=> e.EmployeeId)
                    .ToList();
                dataGridContent.ItemsSource = Employees;
                break;

            case "Tasks":
                VisibleFilterAndButton(false);
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("TaskId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Название задачи", Binding = new Avalonia.Data.Binding("TaskName") });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Описание", Binding = new Avalonia.Data.Binding("Description"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Проект", Binding = new Avalonia.Data.Binding("ProjectInTaskNavigation.ProjectName"), Width = DataGridLength.Auto});
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Ответственный", Binding = new Avalonia.Data.Binding("AssignedToNavigation.Fullname") });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Срок выполнения", Binding = new Avalonia.Data.Binding("Deadline"), Width = DataGridLength.Auto });

                Tasks = _context.Tasks
                    .Include(t => t.ProjectInTaskNavigation)
                    .Include(t => t.AssignedToNavigation)
                    .OrderBy(t=> t.TaskId)
                    .ToList();
                dataGridContent.ItemsSource = Tasks;

                // Заполнение ComboBox для фильтрации по проектам
                var cmbProjects = this.FindControl<ComboBox>("cmbFilterProjects");
                cmbProjects.ItemsSource = _context.Projects.ToList();
                cmbProjects.DisplayMemberBinding = new Avalonia.Data.Binding("ProjectName");
                break;
            
            case "AdCompanies":
                VisibleFilterAndButton(false);
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("CompanyId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Название кампании", Binding = new Avalonia.Data.Binding("CompanyName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Платформа", Binding = new Avalonia.Data.Binding("Platform"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Бюджет", Binding = new Avalonia.Data.Binding("Budget"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Проект", Binding = new Avalonia.Data.Binding("ProjectInCompanyNavigation.ProjectName") });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Дата начала", Binding = new Avalonia.Data.Binding("StartDate"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Дата завершения", Binding = new Avalonia.Data.Binding("EndDate"), Width = DataGridLength.Auto });

                AdCompanies = _context.AdCompanies
                    .Include(a => a.ProjectInCompanyNavigation)
                    .OrderBy(a=> a.CompanyId)
                    .ToList();
                dataGridContent.ItemsSource = AdCompanies;

                // Используем тот же ComboBox, что и для Tasks
                var cmbProjectsAd = this.FindControl<ComboBox>("cmbFilterProjects");
                cmbProjectsAd.ItemsSource = _context.Projects.ToList();
                cmbProjectsAd.DisplayMemberBinding = new Avalonia.Data.Binding("ProjectName");
                break;
            
            case "Reports":
                /*VisibleFilterAndButton(false, false);*/
                VisibleFilterAndButton(false,false);
                cmbFilterEmployees.IsVisible = true;
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("ReportId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Название отчёта", Binding = new Avalonia.Data.Binding("ReportName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Проект", Binding = new Avalonia.Data.Binding("ProjectInReportNavigation.ProjectName") });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Автор отчёта", Binding = new Avalonia.Data.Binding("GeneratedByNavigation.Fullname"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Дата создания", Binding = new Avalonia.Data.Binding("GenerationDate"), Width = DataGridLength.Auto });

                Reports = _context.Reports
                    .Include(r => r.ProjectInReportNavigation)
                    .Include(r => r.GeneratedByNavigation)
                    .OrderBy(r=>r.ReportId)
                    .ToList();
                dataGridContent.ItemsSource = Reports;

                // Заполнение ComboBox для фильтрации по проектам
                var cmbProjectsReports = this.FindControl<ComboBox>("cmbFilterProjects");
                cmbProjectsReports.ItemsSource = _context.Projects.ToList();
                cmbProjectsReports.DisplayMemberBinding = new Avalonia.Data.Binding("ProjectName");
                
                // Заполнение ComboBox для фильтрации по сотрудникам
                var cmbEmployeesReports = this.FindControl<ComboBox>("cmbFilterEmployees");
                cmbEmployeesReports.ItemsSource = _context.Employees.ToList();
                cmbEmployeesReports.DisplayMemberBinding = new Avalonia.Data.Binding("Fullname");
                break;
            
            case "Specifications":
                VisibleFilterAndButton(false,false);
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("SpecId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Название ТЗ", Binding = new Avalonia.Data.Binding("SpecName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Проект", Binding = new Avalonia.Data.Binding("ProjectInSpecificationNavigation.ProjectName"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Содержание", Binding = new Avalonia.Data.Binding("Content"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Автор ТЗ", Binding = new Avalonia.Data.Binding("CreatedByNavigation.Fullname") });

                Specifications = _context.Specifications
                    .Include(s => s.ProjectInSpecificationNavigation)
                    .Include(s => s.CreatedByNavigation)
                    .OrderBy(s=>s.SpecId)
                    .ToList();
                dataGridContent.ItemsSource = Specifications;

                // Используем тот же ComboBox, что и для Tasks
                var cmbProjectsSpec = this.FindControl<ComboBox>("cmbFilterProjects");
                cmbProjectsSpec.ItemsSource = _context.Projects.ToList();
                cmbProjectsSpec.DisplayMemberBinding = new Avalonia.Data.Binding("ProjectName");
                break;
            
            case "ProjectsStatus":
                VisibleFilterAndButton(false,false);
                PanelFilter.IsVisible = false;
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Avalonia.Data.Binding("ProjectStatusId"), Width = DataGridLength.Auto });
                dataGridContent.Columns.Add(new DataGridTextColumn { Header = "Статус", Binding = new Avalonia.Data.Binding("StatusName"), Width = DataGridLength.Auto });

                ProjectsStatuses = _context.ProjectsStatuses
                    .OrderBy(p=>p.ProjectStatusId)
                    .ToList();
                dataGridContent.ItemsSource = ProjectsStatuses;
                break;
        }
    }
     // загрузка таблиц
    private void LoadDefaultTable()
    {
        var selectedTable = cmbTables.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedTable))
            return;

        ConfigureDataGrid(selectedTable);
    }
    
    // кнопка фильтрации
    private void btnFilter_Click(object? sender, RoutedEventArgs e)
    {
        var selectedTable = cmbTables.SelectedItem?.ToString();
        switch (selectedTable)
        {
            // по txtbox
            case "Projects": // по имени проекта
                var projectNameFilter = txtFilter.Text?.Trim().ToLower();
                var filteredProjects = _context.Projects
                    .Where(p => string.IsNullOrEmpty(projectNameFilter) || p.ProjectName.ToLower().Contains(projectNameFilter))
                    .Include(p => p.ClientInProjectNavigation)
                    .Include(p => p.StatusNavigation)
                    .ToList();
                dataGridContent.ItemsSource = filteredProjects;
                break;
            
            case "Clients": // по имени организации
                var clientNameFilter = txtFilter.Text?.Trim().ToLower();
                var filteredClients = _context.Clients
                    .Where(c => string.IsNullOrEmpty(clientNameFilter) || c.ClientName.ToLower().Contains(clientNameFilter))
                    .ToList();
                dataGridContent.ItemsSource = filteredClients;
                break;
            
            case "Employees": // по фамилии сотрудника
                var lastNameFilter = txtFilter.Text?.Trim().ToLower();
                var filteredEmployees = _context.Employees
                    .Where(e => string.IsNullOrEmpty(lastNameFilter) || e.LastName.ToLower().Contains(lastNameFilter))
                    .ToList();
                dataGridContent.ItemsSource = filteredEmployees;
                break;
            
            // по cmb
            case "Tasks":
                var selectedProjectTask = this.FindControl<ComboBox>("cmbFilterProjects").SelectedItem as Project;
                var filteredTasks = _context.Tasks
                    .Where(t => selectedProjectTask == null || t.ProjectInTask == selectedProjectTask.ProjectId)
                    .Include(t => t.ProjectInTaskNavigation)
                    .Include(t => t.AssignedToNavigation)
                    .ToList();
                dataGridContent.ItemsSource = filteredTasks;
                break;
            
            case "AdCompanies":
                var selectedProjectAd = this.FindControl<ComboBox>("cmbFilterProjects").SelectedItem as Project;
                var filteredAdCompanies = _context.AdCompanies
                    .Where(a => selectedProjectAd == null || a.ProjectInCompany == selectedProjectAd.ProjectId)
                    .Include(a => a.ProjectInCompanyNavigation)
                    .ToList();
                dataGridContent.ItemsSource = filteredAdCompanies;
                break;
            
            case "Reports":
                var selectedProjectReport = this.FindControl<ComboBox>("cmbFilterProjects").SelectedItem as Project;
                var selectedEmployeeReport = this.FindControl<ComboBox>("cmbFilterEmployees").SelectedItem as Employee;
                var filteredReports = _context.Reports
                    .Where(r => (selectedProjectReport == null || r.ProjectInReport == selectedProjectReport.ProjectId) &&
                                (selectedEmployeeReport == null || r.GeneratedBy == selectedEmployeeReport.EmployeeId))
                    .Include(r => r.ProjectInReportNavigation)
                    .Include(r => r.GeneratedByNavigation)
                    .ToList();
                dataGridContent.ItemsSource = filteredReports;
                break;
            
            case "Specifications":
                var selectedProjectSpec = this.FindControl<ComboBox>("cmbFilterProjects").SelectedItem as Project;
                var filteredSpecifications = _context.Specifications
                    .Where(s => selectedProjectSpec == null || s.ProjectInSpecification == selectedProjectSpec.ProjectId)
                    .Include(s => s.ProjectInSpecificationNavigation)
                    .Include(s => s.CreatedByNavigation)
                    .ToList();
                dataGridContent.ItemsSource = filteredSpecifications;
                break;
        }
    }
    // показать все записи
    private void btnAll_Click(object? sender, RoutedEventArgs e)
    {
        LoadDefaultTable();
        txtFilter.Clear();
        cmbFilterEmployees.SelectedItem = null;
        cmbFilterProjects.SelectedItem = null;
    }
    
    // кнопка для добавления данных в определённую таблицу
    private void BtnCreate_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedTable = cmbTables.SelectedItem?.ToString();
        switch (selectedTable)
        {
            case "Projects":
                var newProject = new Project();
                _context.Projects.Add(newProject);
                ProjectAdd projectAdd = new ProjectAdd(_context, newProject);
                projectAdd.ProjectAdded += () => LoadDefaultTable();
                projectAdd.ShowDialog(this);
                break;

            case "Clients":
                var newClient = new Client();
                _context.Clients.Add(newClient);
                ClientAdd clientAdd = new ClientAdd(_context, newClient);
                clientAdd.ClientAdded += () => LoadDefaultTable();
                clientAdd.ShowDialog(this);
                break;
            
            case "Tasks":
                var newTask = new Task();
                _context.Tasks.Add(newTask);
                TaskAdd taskAdd = new TaskAdd(_context, newTask);
                taskAdd.TaskAdded += () => LoadDefaultTable();
                taskAdd.ShowDialog(this);
                break;
            
            case "AdCompanies":
                var newAdCompany = new AdCompany();
                _context.AdCompanies.Add(newAdCompany);
                AdCompanyAdd adCompanyAdd = new AdCompanyAdd(_context, newAdCompany);
                adCompanyAdd.AdCompanyAdded += () => LoadDefaultTable();
                adCompanyAdd.ShowDialog(this);
                break;
            
            case "Employees":
                var newEmployee = new Employee();
                _context.Employees.Add(newEmployee);
                EmployeeAdd employeeAdd = new EmployeeAdd(_context, newEmployee);
                employeeAdd.EmployeeAdded += () => LoadDefaultTable();
                employeeAdd.ShowDialog(this);
                break;

            // Аналогично для остальных таблиц...
        }
    }
    
    // переход на форму редактирования выбранной записи
    private void btnEdit_Click(object? sender, RoutedEventArgs e)
    {
        var selectedTable = cmbTables.SelectedItem?.ToString();
        switch (selectedTable)
        {
            case "Projects":
                var selectedProject = dataGridContent.SelectedItem as Project;
                if (selectedProject == null) return;
                var projectEdit = new ProjectEdit(_context, selectedProject);
                projectEdit.ProjectUpdated += () => LoadDefaultTable();
                projectEdit.ShowDialog(this);
                break;
            
            /*case "AdCompanies":
                var selectedAdCompany = dataGridContent.SelectedItem as AdCompany;
                if (selectedAdCompany == null) return;
                var adCompanyEdit = new AdCompanyEdit(_context, selectedAdCompany);
                adCompanyEdit.AdCompanyUpdated += () => LoadDefaultTable();
                adCompanyEdit.ShowDialog(this);
                break;

            case "Clients":
                var selectedClient = dataGridContent.SelectedItem as Client;
                if (selectedClient == null) return;
                var clientEdit = new ClientEdit(_context, selectedClient);
                clientEdit.ClientUpdated += () => LoadDefaultTable();
                clientEdit.ShowDialog(this);
                break;

            case "Employees":
                var selectedEmployee = dataGridContent.SelectedItem as Employee;
                if (selectedEmployee == null) return;
                var employeeEdit = new EmployeeEdit(_context, selectedEmployee);
                employeeEdit.EmployeeUpdated += () => LoadDefaultTable();
                employeeEdit.ShowDialog(this);
                break;

            case "Specifications":
                var selectedSpecification = dataGridContent.SelectedItem as Specification;
                if (selectedSpecification == null) return;
                var specificationEdit = new SpecificationEdit(_context, selectedSpecification);
                specificationEdit.SpecificationUpdated += () => LoadDefaultTable();
                specificationEdit.ShowDialog(this);
                break;

            case "Tasks":
                var selectedTask = dataGridContent.SelectedItem as Task;
                if (selectedTask == null) return;
                var taskEdit = new TaskEdit(_context, selectedTask);
                taskEdit.TaskUpdated += () => LoadDefaultTable();
                taskEdit.ShowDialog(this);
                break;*/
        }
    }
    
    
    // удаление выделенной записи в определённой таблице
    private void btnDelete_click(object? sender, RoutedEventArgs e)
    {
        var selectedTable = cmbTables.SelectedItem?.ToString();
        switch (selectedTable)
        {
            case "Projects":
                var project = dataGridContent.SelectedItem as Project;
                if (project == null) return;

                // Удаляем все связанные задачи
                var relatedTasks = _context.Tasks.Where(t => t.ProjectInTask == project.ProjectId).ToList();
                _context.Tasks.RemoveRange(relatedTasks);

                // Удаляем проект
                _context.Projects.Remove(project);
                _context.SaveChanges();
                LoadDefaultTable();
                break;
            
            case "Clients":
                var client = dataGridContent.SelectedItem as Client;
                if (client == null) return;
                _context.Clients.Remove(client);
                _context.SaveChanges();
                LoadDefaultTable();
                break;

            // Аналогично для остальных таблиц...
        }
    }
}