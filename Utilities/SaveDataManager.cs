using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorld.Utilities
{
    internal class SaveDataManager
    {

        ModDataManager dm = new ModDataManager("Dynamic World", false);

        public void Save(string data, string suffix)
        {
            dm.Save(data, suffix);
        }

      

    }
}
