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
using AMP_SalesOrderManager.sr_InvDiscDetails;
using AMP_SalesOrderManager.sr_SalesLine;

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
            var verkaufsauftragsliste = from so in dataContext.AMP_SalesOrderMng_OrderListDetail
                                        select so;

            SalesOrderGrid.ItemsSource = verkaufsauftragsliste;
        }

        private void btnUpdateSalesOrderPage_Click(object sender, RoutedEventArgs e)
        {
            //Webservice initialisieren
            RechnungsrabattDetails_PortClient rechnungsrabattDetails_PortClient = new RechnungsrabattDetails_PortClient();
            SalesLine_PortClient salesLine_PortClient = new SalesLine_PortClient();


            var verkaufsauftragsliste = from so in dataContext.AMP_SalesOrderMng_OrderListDetail
                                        select so;

            foreach (var item in verkaufsauftragsliste)
            {
                System.Diagnostics.Process proc1 = System.Diagnostics.Process.Start("dynamicsnav://192.168.0.33:7046/BC_ECHT/Fahrzeugbau/runpage?page=50003&$filter='Record ID Text' IS '"+ item.Record_ID_Text +"'");
                proc1.WaitForExit();
            }

            
            
            //System.Diagnostics.Process.Start("dynamicsnav://192.168.0.33:7046/BC_ECHT/Fahrzeugbau/runpage?page=50003&$filter='Record ID Text' IS 'Sales Line: Auftrag,F21-02687,30000'");
            //Objekt vom Typ RechnungsrabattDetails erzeugen mittels Read
            //Liefert immer nur ersten: 
            //RechnungsrabattDetails rechnungsrabattDetails = rechnungsrabattDetails_PortClient.Read(10, "A000");
            //RechnungsrabattDetails rechnungsrabattDetails = rechnungsrabattDetails_PortClient.ReadByRecId("Invoice Disc_ Details,F18-05606,30000");

            //MessageBox.Show(rechnungsrabattDetails.Discount_Code + " - " + rechnungsrabattDetails.Discount_Description + " - " + rechnungsrabattDetails.FORMAT_Record_ID);

            //Objekt vom Typ SalesLine erzeugen mittels Read
            //SalesLine salesLine = salesLine_PortClient.Read("F19-05515", 40000);

            //Objekt verändern
            //SalesLine newSalesLine = UpdateNewSalesLine(salesLine, "FRACHT", 240, 40500);

            //neue Zeile erzeugen
            //salesLine_PortClient.Create(ref newSalesLine);


            //MessageBox.Show(salesLine.No + " zum Preis von " + salesLine.Unit_Price.ToString());

        }

        private SalesLine UpdateNewSalesLine(SalesLine salesLine, string itemno, decimal unitprice, int lineno)
        {
            salesLine.No = itemno;
            salesLine.Unit_Price = unitprice;
            salesLine.Line_No = lineno;
            return salesLine;
        }
    }
}
