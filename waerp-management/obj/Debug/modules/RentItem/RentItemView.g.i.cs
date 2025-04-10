﻿#pragma checksum "..\..\..\..\modules\RentItem\RentItemView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FBA4C8A363431B80EA927628A441560D6B12FD20EA7C5813B8BA30538C4757C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using waerp_management.application.rentItem;


namespace waerp_management {
    
    
    /// <summary>
    /// rentItem
    /// </summary>
    public partial class rentItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 70 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock instructionText;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock breadcrumbData;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid machineData;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridFilter;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backBtn;
        
        #line default
        #line hidden
        
        
        #line 205 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextBtn;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchBox;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OpenRentBtn;
        
        #line default
        #line hidden
        
        
        #line 254 "..\..\..\..\modules\RentItem\RentItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridItems;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Waerp Inventory Management;component/modules/rentitem/rentitemview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.instructionText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            
            #line 81 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.resetSearch);
            
            #line default
            #line hidden
            return;
            case 3:
            this.breadcrumbData = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.machineData = ((System.Windows.Controls.DataGrid)(target));
            
            #line 120 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.machineData.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.machineData_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dataGridFilter = ((System.Windows.Controls.DataGrid)(target));
            
            #line 166 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.dataGridFilter.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filterData_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.backBtn = ((System.Windows.Controls.Button)(target));
            
            #line 199 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.backBtn.Click += new System.Windows.RoutedEventHandler(this.getLastStage);
            
            #line default
            #line hidden
            return;
            case 7:
            this.nextBtn = ((System.Windows.Controls.Button)(target));
            
            #line 207 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.nextBtn.Click += new System.Windows.RoutedEventHandler(this.getNextStage);
            
            #line default
            #line hidden
            return;
            case 8:
            this.searchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 239 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.searchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.OpenRentBtn = ((System.Windows.Controls.Button)(target));
            
            #line 246 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.OpenRentBtn.Click += new System.Windows.RoutedEventHandler(this.openRentWindow);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dataGridItems = ((System.Windows.Controls.DataGrid)(target));
            
            #line 258 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            this.dataGridItems.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ItemData_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 262 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CopyItemIdent);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 264 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CopyDescription);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 266 "..\..\..\..\modules\RentItem\RentItemView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CopyAll);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

