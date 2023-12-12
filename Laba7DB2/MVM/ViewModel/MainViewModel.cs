using Laba7DB2.Core;
using Laba7DB2.MVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Laba7DB2.MVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public ViewMModal ViewMModal { get; set; }
        public Order order { get; set; }
        public ControlsWorker controlsWorker { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            /*ViewMModal = new ViewMModal();*/
            /*order = new Order();*/
            controlsWorker = new ControlsWorker();
        }
    }
}
