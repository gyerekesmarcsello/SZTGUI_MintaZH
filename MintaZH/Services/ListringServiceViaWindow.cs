using MintaZH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH.Services
{
    class ListringServiceViaWindow : IListingService
    {
        public void ListAll(IList<Fixture> fixtures)
        {
            string jSonData = JsonConvert.SerializeObject(fixtures);
            File.WriteAllText("data.txt", jSonData);

            new EditorWindow(fixtures).ShowDialog();
        }
    }
}
