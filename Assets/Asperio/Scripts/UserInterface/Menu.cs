using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Asperio
{
    public class Menu : Singleton<Menu>
    {
        [SerializeField]
        private GameObject _visuals;
        [SerializeField]
        private InputAction _inputToggleMenu;
        [SerializeField]
        private List<MenuButton> _listMenuButton;
        private MenuButton _currentButton;

        private void OnEnable()
        {
            SetInputAction();
            ResetMenuButton();
            CloseMenu();
        }

        private void ResetMenuButton()
        {
            for (int i = 0; i < _listMenuButton.Count; i++)
            {
                _listMenuButton[i].OnButtonReset();
            }
        }

        private void SetInputAction()
        {
            _inputToggleMenu.performed += ToggleMenuAction;
            _inputToggleMenu.Enable();
        }

        public void ToggleMenuAction(InputAction.CallbackContext obj)
        {
            ToggleMenu();
        }

        public void ToggleMenu() {
            if (_visuals.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        public void OpenConfirmStartTask(UITaskItem uiTaskItem)
        {

        }

        public void OpenMenu()
        {
            _visuals.SetActive(true);
        }

        public void OpenMenuByIndex(int index)
        {
            if (_currentButton != null)
            {
                _currentButton.OnButtonReset();
            }
            _currentButton = _listMenuButton[index];
            _currentButton.OnButtonSelect();
            _visuals.SetActive(true);
        }

        public void CloseMenu()
        {
            _visuals.SetActive(false);
        }
    }
}

