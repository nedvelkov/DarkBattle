using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkBattle.ViewModels.Items
{
  public  class ItemListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        [DisplayName("Obtain by")]
        public string ObtainBy { get; set; }

        [DisplayName("Required level")]
        public int RequiredLevel { get; set; }

    }
}
