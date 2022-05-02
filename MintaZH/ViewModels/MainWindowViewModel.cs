using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using MintaZH.Logic;
using MintaZH.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MintaZH.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        IFixtureLogic _logic;

        public ObservableCollection<Fixture> Left { get; set; }
        public ObservableCollection<Fixture> Right { get; set; }

        private Fixture _selectedFromLeftSide;

        public Fixture SelectedFromLeftSide
        {
            get { return _selectedFromLeftSide; }
            set
            {
                SetProperty(ref _selectedFromLeftSide, value);
                (AddCommand as RelayCommand).NotifyCanExecuteChanged();
                (DiscountCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Fixture _selectedFromRightSide;

        public Fixture SelectedFromRightSide
        {
            get { return _selectedFromRightSide; }
            set
            {
                SetProperty(ref _selectedFromRightSide, value);
                (RemoveCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }




        public int AllCost { get { return this._logic.AllCost; } }
        public int AllCount { get { return this._logic.AllCount; } }

        public ICommand LoadCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DiscountCommand { get; set; }
        public ICommand ListingCommand { get; set; }

        public MainWindowViewModel()
            : this(IsInDesignMode ? null : Ioc.Default.GetService<IFixtureLogic>())
        {

        }
        public MainWindowViewModel(IFixtureLogic logic)
        {
            this._logic = logic;

            Left = new ObservableCollection<Fixture>();
            Right = new ObservableCollection<Fixture>();

            this._logic.SetUpCollections(Left, Right);

            LoadCommand = new RelayCommand(
                () => this._logic.LoadFromFile(),
                () => Left.Count == 0
                );

            RemoveCommand = new RelayCommand(
               () => this._logic.RemoveFromRightSide(SelectedFromRightSide),
               () => SelectedFromRightSide != null
               );

            AddCommand = new RelayCommand(
               () => this._logic.AddToRightSide(SelectedFromLeftSide),
               () => SelectedFromLeftSide != null
               );

            DiscountCommand = new RelayCommand(
               () => this._logic.DiscountSelected(SelectedFromLeftSide),
               () => SelectedFromLeftSide != null
               );

            ListingCommand = new RelayCommand(
               () => this._logic.ListAll()
               );

            Messenger.Register<MainWindowViewModel, string, string>(this, "LogicResult", (recipient, msg) =>
            {
                OnPropertyChanged("AllCost");
                OnPropertyChanged("AllCount");
            });
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

    }
}
