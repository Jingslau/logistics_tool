﻿using System.Data;
using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;

namespace waerp_management.modules.Administration.ItemAdministration
{
    /// <summary>
    /// Interaction logic for EditFIlterWindow.xaml
    /// </summary>
    public partial class EditFilterWindow : Window
    {
        public string filterNo;
        public string filterID;
        public string newFilterName;
        public EditFilterWindow()
        {
            InitializeComponent();
            FilterIDSelector.Items.Add("1");
            FilterIDSelector.Items.Add("2");
            FilterIDSelector.Items.Add("3");
            FilterIDSelector.Items.Add("4");
            FilterIDSelector.Items.Add("5");
        }

        private void FilterIDSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FilterIDSelector.SelectedItem != null)
            {
                filterNo = FilterIDSelector.SelectedItem.ToString();
                oldFiltername.Items.Clear();
                DataSet allFilters = AdministrationQueries.GetAllInfo($"filter{filterNo}_names");
                for (int i = 0; i < allFilters.Tables[0].Rows.Count; i++)
                {
                    oldFiltername.Items.Add(allFilters.Tables[0].Rows[i]["name"]);
                }
                oldFiltername.IsEnabled = true;
                newFiltername.IsEnabled = false;
                newFiltername.Text = "";
                SelectedItemsNo.Text = "0";
            }
            else
            {
                oldFiltername.IsEnabled = false;
                newFiltername.IsEnabled = false;
            }
        }

        private void oldFiltername_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {


            if (oldFiltername.SelectedItem != null)
            {
                DataSet ds = AdministrationQueries.RunSql($"SELECT * FROM filter{filterNo}_names WHERE name = '{oldFiltername.SelectedItem}'");
                filterID = ds.Tables[0].Rows[0]["filter_id"].ToString();
                int SelectedItems = AdministrationQueries.RunSql($"SELECT * FROM item_filter_relations WHERE filter{filterNo}_id = {filterID}").Tables[0].Rows.Count;
                SelectedItemsNo.Text = SelectedItems.ToString();
                newFiltername.IsEnabled = true;
                newFiltername.Text = "";
            }
            else
            {
                newFiltername.IsEnabled = false;
                newFiltername.Text = "";
            }
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void EditFilter_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = AdministrationQueries.RunSql($"SELECT * FROM filter{filterNo}_names WHERE name = '{newFiltername.Text}'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ErrorHandlerModel.ErrorText = "Es besteht bereits ein Filter mit diesem Namen!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else if (newFiltername.Text.Length <= 0)
            {
                ErrorHandlerModel.ErrorText = "Bitte geben Sie einen neuen Filternamen ein!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else
            {
                AdministrationQueries.RunSql($"UPDATE filter{filterNo}_names SET name = '{newFiltername.Text}' WHERE filter_id = {filterID}");
                ErrorHandlerModel.ErrorText = "Der Filter wurde erfolgreich bearbeitet!";
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorWindow showSuccess = new ErrorWindow();
                showSuccess.ShowDialog();
                DialogResult = false;
            }
        }
    }
}
