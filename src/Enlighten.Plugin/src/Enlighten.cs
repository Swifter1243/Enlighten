using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Reflection;

namespace Enlighten.src.Enlighten.Plugin
{
    [Plugin("Enlighten")]
    public class Enlighten
    {
        public EventGridContainer events;
        public PlatformDescriptor PlatformDescriptor;
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
                LoadInitialMap.PlatformLoadedEvent += (descriptor) => PlatformDescriptor = descriptor;
                ui.OnLoad();
            }
        }

        public static string OptionToString(OptionName option)
        {
            return Enum.GetName(typeof(OptionName), option);
        }

        public static OptionName StringToOption(string option)
        {
            return (OptionName)Enum.Parse(typeof(OptionName), option);
        }
    }
}