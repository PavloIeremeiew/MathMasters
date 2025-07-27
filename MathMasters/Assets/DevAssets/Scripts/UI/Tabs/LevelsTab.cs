using UnityEngine;

namespace MathMasters
{
    public class LevelsTab : Tab
    {
        [SerializeField] 
        private MenuManager _menuManager;

        public override void OnTabSelected()
        {
            _menuManager.Init();
        }
    }
}
