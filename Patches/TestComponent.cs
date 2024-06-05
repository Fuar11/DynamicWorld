using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorld.Patches
{
    [RegisterTypeInIl2Cpp]

    internal class TestComponent : MonoBehaviour
    {

        public int testValue = 0;

    }
}
