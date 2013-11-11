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
    /// Interaction logic for EditTransaction.xaml
    /// </summary>
    public partial class EditTransaction : Page
    {
        dNet.DB.IDbObj dbobj;

        public EditTransaction()
        {
            InitializeComponent();
            dbobj = dNet.DB.Factory.GetDbObj();
        }

        public EditTransaction(Model.TransacaoM transacaoM)
        {
            InitializeComponent();
            dbobj = dNet.DB.Factory.GetDbObj();
            this.DataContext = transacaoM;
        }

        private void btnAddComprovante_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete");
        }

        private void btnDelComprovante_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalvarTransacao_Click(object sender, RoutedEventArgs e)
        {
            dbobj.Save(this.DataContext as ToPayList.Model.TransacaoM);
        }

        private void btnCancelarTransacao_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnExcluirTransacao_Click(object sender, RoutedEventArgs e)
        {
            dbobj.Delete(this.DataContext as ToPayList.Model.TransacaoM);
        }
    }
}
