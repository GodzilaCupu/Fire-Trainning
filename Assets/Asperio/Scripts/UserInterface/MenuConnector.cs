using UnityEngine;

namespace Asperio
{
    public class MenuConnector : MonoBehaviour
    {
        public void OpenMenu()
        {
            Menu.Instance.OpenMenu();
        }

        public void OpenMenuByIndex(int index)
        {
            Menu.Instance.OpenMenuByIndex(index);
        }

        public void CloseMenu()
        {
            Menu.Instance.CloseMenu();
        }
    }
}

