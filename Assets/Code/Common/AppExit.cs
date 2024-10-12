using UnityEditor;
using UnityEngine;

namespace GTC
{
    public class AppExit : MonoBehaviour
    {
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}