using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Views
{

    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            TargetType = typeof(Views.Front);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}