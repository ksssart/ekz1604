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

public partial class registrationForm : Window
{
    private List<clients> _clients;
    private string connStr = "server=localhost;database=ekz1604;port=3306;User Id=root;password=кщще;";
    private MySqlConnection conn;
        
    public registrationForm()
    {
        InitializeComponent();
        _Cli = cccli;
        this.DataContext = _Cli;
        _clients = CCClients;
    }
    
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var zzz = _clients.FirstOrDefault(x => x.id == _Cli.id);
        if (zzz == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO clients (id, surname, name, phone, dolznost_id) VALUES (" + Convert.ToInt32(id.Text) + ", '" + surname.Text + "', '"+ name.Text + "', '"+ lastname.Text + "', " + Convert.ToInt32(dolznost_id.Text) + ");";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE sotrudnik SET surname = '" + surname.Text + "', '"+ name.Text + "', '"+ lastname.Text + "', " + Convert.ToInt32(dolznost_id.Text) + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }
    
    private void back(object? sender, RoutedEventArgs e)
    {
        var main = new MainWindow();
        main.Show();
        this.Hide();
    }
}
