﻿#pragma checksum "..\..\MainW.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "46C5B7B4D2C60A78135BDA6C533BA6528011555C462B7A4C73142F63B5BFA322"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Laba7DB2.MVM.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Laba7DB2 {
    
    
    /// <summary>
    /// MainW
    /// </summary>
    public partial class MainW : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 54 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CentrText;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ViewBtn;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OrderBtn;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RepairBtn;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ControlBtn;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClientBtn;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReportBtn;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ReportComboBox;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\MainW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReportStart;
        
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
            System.Uri resourceLocater = new System.Uri("/Laba7DB2;component/mainw.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainW.xaml"
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
            
            #line 16 "..\..\MainW.xaml"
            ((Laba7DB2.MainW)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 27 "..\..\MainW.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.ESC_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CentrText = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            
            #line 60 "..\..\MainW.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.X_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 63 "..\..\MainW.xaml"
            ((System.Windows.Controls.StackPanel)(target)).Loaded += new System.Windows.RoutedEventHandler(this.StackPanelka_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ViewBtn = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\MainW.xaml"
            this.ViewBtn.Click += new System.Windows.RoutedEventHandler(this.View_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.OrderBtn = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\MainW.xaml"
            this.OrderBtn.Click += new System.Windows.RoutedEventHandler(this.Order_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RepairBtn = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\MainW.xaml"
            this.RepairBtn.Click += new System.Windows.RoutedEventHandler(this.RepairBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ControlBtn = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\MainW.xaml"
            this.ControlBtn.Click += new System.Windows.RoutedEventHandler(this.ControlWorker_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ClientBtn = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\MainW.xaml"
            this.ClientBtn.Click += new System.Windows.RoutedEventHandler(this.Client_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ReportBtn = ((System.Windows.Controls.Button)(target));
            
            #line 111 "..\..\MainW.xaml"
            this.ReportBtn.Click += new System.Windows.RoutedEventHandler(this.Report_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 119 "..\..\MainW.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ReportComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 14:
            this.ReportStart = ((System.Windows.Controls.Button)(target));
            
            #line 152 "..\..\MainW.xaml"
            this.ReportStart.Click += new System.Windows.RoutedEventHandler(this.ReportStart_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
