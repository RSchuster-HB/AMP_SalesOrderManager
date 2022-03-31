using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;

namespace AMP_SalesOrderManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //2. Klasse erstellen: AMP_SalesOrderManager - Hinzufügen - neues Element - Linq to SQL Klassen
        
        //3. dataContext erstellen mit neuer Klasse
        LinqToSqlDataClassesDataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();

            //1. Connection String anlegen
            string connectionString = ConfigurationManager.ConnectionStrings["AMP_SalesOrderManager.Properties.Settings.Abfragedaten_BCConnectionString"].ConnectionString;

            //4. DataContext initialisieren mit vorher angelegtem ConnectionString
            dataContext = new LinqToSqlDataClassesDataContext(connectionString);

            //SalesOrderGrid befüllen
            GetSalesOrders();

        }

        

        public void GetSalesOrders()
        {
            var verkaufsauftragsliste = from so in dataContext.AMP_SalesOrderMng_InvDiscDetail
                                        select so;

            SalesOrderGrid.ItemsSource = verkaufsauftragsliste;
        }
    }
}
