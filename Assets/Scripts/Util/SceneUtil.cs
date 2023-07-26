using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PineyPiney.Util
{
    public class SceneUtil : MonoBehaviour
    {

        static SceneAsset sceneToLoad = null;

        private void Start()
        {
        }

        private void Update()
        {
            if (sceneToLoad != null)
            {
                TryToOpen(sceneToLoad);
            }
        }

        public static SceneAsset GetSceneAsset(string name)
        {
            return Prefabs.Get<SceneAsset>($"Scenes/{name}");
        }

        public static Scene GetScene(SceneAsset a)
        {
            return SceneManager.GetSceneByName(a.name);
        }

        public static void Open(SceneAsset scene)
        {
            sceneToLoad = scene;
            Load(scene);
        }

        public static void Load(SceneAsset scene)
        {

            SceneManager.LoadSceneAsync(scene.name);
        }

        static bool TryToOpen(SceneAsset scene)
        {
            Scene s = GetScene(scene);
            if (!s.isLoaded)
            {
                return false;
            }
            else
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                SceneManager.SetActiveScene(s);
                sceneToLoad = null;
                return true;
            }
        }
    }

    public static class Scenes
    {
        public static SceneAsset CHARACTER_CUSTOMISATION { get { return SceneUtil.GetSceneAsset("CharacterCustomisationScene"); } }
        public static SceneAsset MAIN_SCENE { get { return SceneUtil.GetSceneAsset("MainScene"); } }

    }
}
