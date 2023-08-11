using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten
{
    [Plugin("Enlighten")]
    public class Enlighten
    {
        [Init]
        private void Init()
		{
            Debug.Log("Hi");
		}
    }
}
