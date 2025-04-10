﻿#pragma checksum "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CAED1EF7E059A5E6A36659E54BF707BE52DB9ED1C2E61DE8A3048D2848BE42D2"
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
using waerp_management.application.Administration.TempLocationAdministration;


namespace waerp_management.application.Administration.TempLocationAdministration {
    
    
    /// <summary>
    /// ConfirmDeleteTempLocationWindow
    /// </summary>
    public partial class ConfirmDeleteTempLocationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ErrorWindowBorder;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon ErrorWindowIcon;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTitle;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ConfirmText;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancleBtn;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteTempLocation;
        
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
            System.Uri resourceLocater = new System.Uri("/Waerp Inventory Management;component/application/administration/templocationadmi" +
                    "nistration/confirmdeletetemplocationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
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
            this.ErrorWindowBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.ErrorWindowIcon = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 3:
            this.MessageTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ConfirmText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.CancleBtn = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
            this.CancleBtn.Click += new System.Windows.RoutedEventHandler(this.CancleBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DeleteTempLocation = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\..\..\Application\Administration\TempLocationAdministration\ConfirmDeleteTempLocationWindow.xaml"
            this.DeleteTempLocation.Click += new System.Windows.RoutedEventHandler(this.DeleteTempLocation_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

