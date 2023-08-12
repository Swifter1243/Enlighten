using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Enlighten
{
    [Plugin("Enlighten")]
    public class Enlighten
    {
        public EventGridContainer events;
        private UI ui;

        [Init]
        private void Init()
		{
            SceneManager.sceneLoaded += SceneLoaded;
            ui = new UI(this);
		}

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3) // Mapping Scene
            {
                events = UnityEngine.Object.FindObjectOfType<EventGridContainer>();
			}
        }
    }
}
