using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MAIN_MENU,
        LOADING,
        GAME,
    }

    private static Scene targetScene;

    public static void Load(Scene scene) {
        SceneManager.LoadScene((int)Scene.LOADING);
        targetScene = scene;
    }

    public static void LoadTarget() {
        SceneManager.LoadScene((int)targetScene);
    }
}
