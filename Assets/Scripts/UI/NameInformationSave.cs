using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class NameInformationSave : MonoBehaviour
    {
        [SerializeField] private TMP_InputField Text;
        [SerializeField] private Transform LoginTransform;
        private bool _isNickSaved;

        public void SaveText()
        {
            if (_isNickSaved) return;
            Database.Instance.PlayerData.CurrentName = Text.text.Trim();
            LoginTransform.gameObject.SetActive(false);
            _isNickSaved = true;
            Database.Instance.GetPlayerProfile();
        }
    }
}