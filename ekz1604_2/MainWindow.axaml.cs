using Avalonia.Controls;
using Avalonia;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using MySqlX.XDevAPI.Common;


namespace ekz1604_2;

public partial class MainWindow : Window
{
    private List<clients> _clients;
    private string connStr = "server=localhost;database=ekz1604;port=3306;User Id=root;password=кщще;";
    private MySqlConnection conn;

    public MainWindow()
    {
        InitializeComponent();
        string table = "select*from clients";
        ShowData(table);
        Filter();
    }

    public void ShowData(string sql)
    {
        _clients = new List<clients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CCClients = new clients
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                phone = reader.GetString("phone"),
                login = reader.GetString("login"),
                password = reader.GetString("password")
            };
            _clients.Add(CCClients);
        }
        conn.Close();
        ClientsGrid.ItemsSource = _clients;
    }

    public void Search(object? sender, TextChangedEventArgs textChangedEventArgs)
    {
        var client = _clients;
        client = client.Where(x => x.surname.Contains(SearchBox.Text)).ToList();
        ClientsGrid.ItemsSource = client;
    }
    
    private void Cmb_Phone(object? sender, SelectionChangedEventArgs e)
    {
        var phone = (ComboBox)sender;
        var CCClients = phone.SelectedItem as clients;
        var filtrPhone = _clients.Where(x => x.phone == CCClients.phone).ToList();
        ClientsGrid.ItemsSource = filtrPhone;
    }
    public void Filter()
    {
        _clients = new List<clients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from clients", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CCClients = new clients()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                phone = reader.GetString("phone"),
                login = reader.GetString("login"),
                password = reader.GetString("password")
            };
            _clients.Add(CCClients);
        }
        conn.Close();
        var cliCmb = this.Find<ComboBox>(name: "CmbPhone");
        cliCmb.ItemsSource = _clients;
    }
    
    
    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            clients clientsDel = ClientsGrid.SelectedItem as clients;
            if (_clients == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM clients WHERE id = " + clientsDel.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            _clients.Remove(clientsDel);
            ShowTable("SELECT*FROM clients;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowTable(string selectFromClients)
    {
        throw new NotImplementedException();
    }
    
    private void SortClients(string sortOrder, object? sender, SelectionChangedEventArgs e)
    {
        if (sortOrder == "По возрастанию")
        {
            var surASC = (ComboBox)sender;
            var surASCCC = surASC.SelectedItem as clients;
            var sortedItems = _clients.OrderBy(s => s.surname).ToList();
            ClientsGrid.ItemsSource = sortedItems;
          
        }
        else if (sortOrder == "По убыванию")
        {
            var surDESC = (ComboBox)sender;
            var surDESCCC = surDESC.SelectedItem as clients;
            var sortedItems = _clients.OrderByDescending(s => s.surname).ToList();
            ClientsGrid.ItemsSource = sortedItems;
            
        }
    }
    
    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        string selectedSortOrder = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
        //SortClients(selectedSortOrder);
    }
    
    private void back(object? sender, RoutedEventArgs e)
    {
        clients newCli = new clients();
        ekz1604_2.registrationForm registr = new ekz1604_2.registrationForm(newCli, _clients);
        registr.Show();
        this.Hide();
    }
   
}