using Microsoft.Toolkit.Mvvm.Messaging;
using MintaZH.Models;
using System.Collections.Generic;

namespace MintaZH.Logic
{
    public interface IFixtureLogic
    {
        int AllCost { get; }
        int AllCount { get; }

        void SetUpCollections(IList<Fixture> left, IList<Fixture> right);
        void AddToRightSide(Fixture fixture);
        void DiscountSelected(Fixture fixture);
        void ListAll();
        void LoadFromFile();
        void RemoveFromRightSide(Fixture fixture);
    }
}