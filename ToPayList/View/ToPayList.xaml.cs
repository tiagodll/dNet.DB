using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToPayList.View
{
	/// <summary>
	/// Interaction logic for lista.xaml
	/// </summary>
	public partial class lista : Page
	{
        //ToPayList.Resources.Data data;
        dNet.DB.IDbObj dbobj;

		public lista()
		{
			InitializeComponent();

            dbobj = dNet.DB.Factory.GetDbObj();
            lstToPay.DataContext = dbobj.LoadList<Model.TransacaoM>("");
		}

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.ShowDialogPage(new EditTransaction(new Model.TransacaoM()));
            dialog = null;
        }

        private void lstToPay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.ShowDialogPage(new EditTransaction(lstToPay.SelectedItem as Model.TransacaoM));
            dialog = null;
        }
	}
}
