using UnityEngine;
using UnityEngine.UI;
using Player47.Core.Service;

namespace Player47.UI.Menu
{
    public class LanguageManager : MonoBehaviour
    {
        [Header("Language toggles")]
        [SerializeField] Toggle _togglePT;
        [SerializeField] Toggle _toggleEN;

        LocalizationManager _localizationManager;

        void Start()
        {
            _localizationManager = LocalizationManager.Instance;
            if (_localizationManager != null)
            {
                SetLanguage(_localizationManager.CurrentLanguageIndex);
            }
        }

        public void SetLanguage(int languageIndex)
        {
            _localizationManager.SelectLanguage(languageIndex);

            // Atualiza os toggles sem disparar eventos
            if (languageIndex == 1)
            {
                _togglePT.SetIsOnWithoutNotify(true);
                _toggleEN.SetIsOnWithoutNotify(false);
            }
            else
            {
                _togglePT.SetIsOnWithoutNotify(false);
                _toggleEN.SetIsOnWithoutNotify(true);
            }
        }
    } 
}
