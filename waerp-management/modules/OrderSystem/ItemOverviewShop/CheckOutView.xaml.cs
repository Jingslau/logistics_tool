using System;
using System.Data;
using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.application.OrderSystem.ItemOverviewShop
{
    /// <summary>
    /// Interaction logic for CheckOutView.xaml
    /// </summary>
    public partial class CheckOutView : Window
    {
        public CheckOutView()
        {
            InitializeComponent();
            GridItems.DataContext = ShoppingCartModel.ShoppingCartInput;
            GridItems.ItemsSource = new DataView(ShoppingCartModel.ShoppingCartInput.Tables[0]);
        }

        private void GridItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void CancleCeckOut(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FinishOrder(object sender, RoutedEventArgs e)
        {
            if (ShoppingCartModel.ShoppingCartInput.Tables[0].Rows.Count > 0)
            {
                bool check1 = OrderItemOverviewQueries.CreateOrder();

                ShoppingCartModel.check = true;
                ErrorHandlerModel.ErrorText = "Die Bestellung wurde erfolgreich aufgegeben!";
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorWindow showResult = new ErrorWindow();
                Nullable<bool> dialogResult = showResult.ShowDialog();
                DialogResult = false;

            }
        }
    }
}
