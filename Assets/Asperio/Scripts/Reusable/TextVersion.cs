using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Asperio
{
    public class TextVersion : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void Start()
        {
            _text.text = $"v. {Application.version}";
        }
    }
}
