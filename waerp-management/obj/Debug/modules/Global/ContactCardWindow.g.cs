﻿#pragma checksum "..\..\..\..\modules\Global\ContactCardWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "624500BB22EE063064389BF1C9F2B9347C63D1E1867FDA0628C6FDAC2B2A25C8"
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
using waerp_management.application.Global;


namespace waerp_management.application.Global {
    
    
    /// <summary>
    /// ContactCardWindow
    /// </summary>
    public partial class ContactCardWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyName;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyAdress;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyCity;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyPostcode;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyCountry;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyWebsite;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyMail;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CompanyPhone;
        
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
            System.Uri resourceLocater = new System.Uri("/Waerp Inventory Management;component/modules/global/contactcardwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
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
            this.CompanyName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.CompanyAdress = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.CompanyCity = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.CompanyPostcode = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.CompanyCountry = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.CompanyWebsite = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.CompanyMail = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.CompanyPhone = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            
            #line 99 "..\..\..\..\modules\Global\ContactCardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

