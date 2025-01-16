using UnityEngine;
using UnityEngine.UI;

namespace Asperio
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject _page;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Color _colorSelected;
        [SerializeField]
        private Color _colorDefault;

        public void OnButtonSelect()
        {
            _page.SetActive(true);
            _icon.color = _colorSelected;
        }

        public void OnButtonReset()
        {
            _page.SetActive(false);
            _icon.color = _colorDefault;
        }
    }
}

