using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using BusinessDomain;
using BusinessLogic;

namespace GUI_WPF.Pages.Coordinator
{
    public partial class VinculatedOrganizations : Page
    {
        public VinculatedOrganizations()
        {
            InitializeComponent();

        }
       

        /*private void button1_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Estado");
            tabla.Columns.Add("Correo Electrónico");

            DataRow row = tabla.NewRow();

            row["Nombre"] = "IPE";
            row["Estado"] = "Veracruz";
            row["Correo Electrónico"] = "ipe@pensiones.gob.mx";
            tabla.Rows.Add(row);

            foreach(DataRow Drow in tabla.Rows)
            {
                int num = tabla_Ov.Columns.Add(row);
            }

        }*/
    }
}
