using Microsoft.Toolkit.Mvvm.Messaging;
using MintaZH.Models;
using MintaZH.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH.Logic
{
    public class FixtureLogic : IFixtureLogic
    {
        IMessenger _messenger;
        IListingService _listringService;
        IList<Fixture> _left;
        IList<Fixture> _right;

        public int AllCost { get { return (_right.Count == 0 ? 0 : _right.Sum(t => t.Cost)); } }
        public int AllCount { get { return _right.Count; } }

        public FixtureLogic(IMessenger messenger, IListingService listringService)
        {
            _messenger = messenger;
            _listringService = listringService;
        }

        public void SetUpCollections(IList<Fixture> left, IList<Fixture> right)
        {
            _left = left;
            _right = right;
        }

        public void LoadFromFile()
        {
            //TODO
            _left.Add(new Fixture() { Name = "1. Alaplap", Type = TypeOfFixture.Motherboard, Cost = 100 });
            _left.Add(new Fixture() { Name = "2. Alaplap", Type = TypeOfFixture.Motherboard, Cost = 100 });
            _left.Add(new Fixture() { Name = "3. Alaplap", Type = TypeOfFixture.Motherboard, Cost = 100 });

            _left.Add(new Fixture() { Name = "1. CPU", Type = TypeOfFixture.CPU, Cost = 100 });
            _left.Add(new Fixture() { Name = "2. CPU", Type = TypeOfFixture.CPU, Cost = 100 });
            _left.Add(new Fixture() { Name = "3. CPU", Type = TypeOfFixture.CPU, Cost = 100 });

            _left.Add(new Fixture() { Name = "1. RAM", Type = TypeOfFixture.RAM, Cost = 100 });
            _left.Add(new Fixture() { Name = "2. RAM", Type = TypeOfFixture.RAM, Cost = 100 });
            _left.Add(new Fixture() { Name = "3. RAM", Type = TypeOfFixture.RAM, Cost = 100 });

            _left.Add(new Fixture() { Name = "1. TÁROLÓ", Type = TypeOfFixture.Storage, Cost = 100 });
            _left.Add(new Fixture() { Name = "2. TÁROLÓ", Type = TypeOfFixture.Storage, Cost = 100 });
            _left.Add(new Fixture() { Name = "3. TÁROLÓ", Type = TypeOfFixture.Storage, Cost = 100 });

            if (File.Exists("data.txt"))
            {
                var FIXTURES = JsonConvert.DeserializeObject<Fixture[]>(File.ReadAllText("data.txt"));
                foreach (var item in FIXTURES)
                {
                    _left.Add(item);
                }
            }
        }

        public void RemoveFromRightSide(Fixture fixture)
        {
            _right.Remove(fixture);
            _messenger.Send("Remove OK", "LogicResult");
        }

        public void AddToRightSide(Fixture fixture)
        {
            Fixture f = new Fixture();
            f.CopyFrom(fixture);

            if (f.Type == TypeOfFixture.RAM || f.Type == TypeOfFixture.Storage)
            {
                _right.Add(f);
            }
            else if (f.Type == TypeOfFixture.Motherboard && _right.Select(t => t.Type).Where(t => (t == TypeOfFixture.Motherboard)).ToList().Count == 0)
            {
                _right.Add(f);
            }
            else if (f.Type == TypeOfFixture.CPU && _right.Select(t => t.Type).Where(t => (t == TypeOfFixture.CPU)).ToList().Count == 0)
            {
                _right.Add(f);
            }

            _messenger.Send("Add OK", "LogicResult");
        }

        public void DiscountSelected(Fixture fixture)
        {
            fixture.Cost = (int)((double)fixture.Cost * 0.9);
            _messenger.Send("Disc OK", "LogicResult");
        }

        public void ListAll()
        {
            _listringService.ListAll(_right);
        }




    }
}
