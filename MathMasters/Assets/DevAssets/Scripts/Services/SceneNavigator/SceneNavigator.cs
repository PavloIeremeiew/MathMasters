using UnityEngine.SceneManagement;

namespace MathMasters.Services
{
    public class SceneNavigator : ISceneNavigator
    {
        private const string LEVEL_SCENE_NAME = "Level";
        private const string MENU_SCENE_NAME = "LevelsMenu";

        public void OpenLevel()
        {
            OpenScene(LEVEL_SCENE_NAME);
        }

        public void OpenMenu()
        {
            OpenScene(MENU_SCENE_NAME);
        }
        private void OpenScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
