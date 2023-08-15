using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Reflection;

namespace Enlighten
{
    [Plugin("Enlighten")]
    public class Enlighten
    {
        public EventGridContainer events;
        public AssetBundle bundle;
        public UI ui;

        [Init]
        private void Init()
		{
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Enlighten.enlighten_bundle");
            bundle = AssetBundle.LoadFromStream(stream);

            SceneManager.sceneLoaded += SceneLoaded;
            ui = new UI(this);

            bundle.UnloadAsync(false);
        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3) // Mapping Scene
            {
                events = UnityEngine.Object.FindObjectOfType<EventGridContainer>();
                ui.OnLoad();
            }
        }
    }
}
