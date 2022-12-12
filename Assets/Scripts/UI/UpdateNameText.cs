using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UpdateNameText : MonoBehaviour
    {
        [SerializeField] private TMP_InputField Text;
        [SerializeField] private Button Button;
        [SerializeField] private CanvasGroup ButtonAlpha;

        public void UpdateTextValue()
        {
            var value = Text.text.Trim();
            if (value.Equals("") || value.Length < 3)
            {
                ButtonAlpha.alpha = 0.2f;
                Button.interactable = false;
            }
            else
            {
                ButtonAlpha.alpha = 1f;
                Button.interactable = true;
            }
        }
    }
}