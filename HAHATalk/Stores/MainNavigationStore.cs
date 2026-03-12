using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WPFLib.Controls;

namespace HAHATalk.Stores
{
    public class MainNavigationStore
    {
        public INotifyPropertyChanged CurrentViewModel
        {
            set => CurrentViewModelChanged?.Invoke(value);
        }

        public SlideType SlideType
        {
            set => SlideTypeChanged?.Invoke(value);
        }

        public event Action<INotifyPropertyChanged>? CurrentViewModelChanged;
        public event Action<SlideType>? SlideTypeChanged;
    }
}
