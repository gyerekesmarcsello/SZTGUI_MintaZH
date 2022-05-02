using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH.Models
{
    public enum TypeOfFixture
    {
        Motherboard, CPU, RAM, Storage
    }
    public class Fixture : ObservableObject
    {
        private TypeOfFixture _type;

        public TypeOfFixture Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _cost;

        public int Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }
        public void CopyFrom(Fixture other)
        {
            this.GetType().GetProperties().ToList().ForEach(prop => prop.SetValue(this, prop.GetValue(other)));
        }

    }
}
