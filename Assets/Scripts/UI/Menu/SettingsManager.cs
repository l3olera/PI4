using UnityEngine;
using UnityEngine.UI;
using Player47.Core.Service;

namespace Player47.UI.Menu
{
    /// <summary>
    /// Responsável por gerenciar as telas da configuração do jogo, indicando para onde cada botão deve levar o usuário.
    /// </summary>
    public class SettingsManager : MonoBehaviour
	{
        [Header("Panel Settings Options")]
		[SerializeField] PanelSwitcher _switcher;
		[SerializeField]
		GameObject[] _panels;

        [SerializeField] GameObject _screeOptions;
		[SerializeField] GameObject _languageOptions;
		[SerializeField] GameObject _keyboardOptions;
        [SerializeField] GameObject _audioOptions;

		[Header("Toggle Component")]
		[SerializeField] Toggle _toggleScreen;

        void Start()
        {
			if(_screeOptions != null && !_screeOptions.activeSelf)
			{
				if(_toggleScreen != null && !_toggleScreen.isOn)
					_toggleScreen.isOn = true;

                _screeOptions.SetActive(true);
				_languageOptions.SetActive(false);
				_keyboardOptions.SetActive(false);
            }
        }

        public void ShowScreenOptions()
		{
			ShowPanel(_screeOptions);
        }

		public void ShowLanguageOptions()
		{
            ShowPanel(_languageOptions);
        }

        public void ShowKeyboardOptions()
		{
            ShowPanel(_keyboardOptions);
        }

		public void ShowAudioOptions()
		{
            ShowPanel(_audioOptions);
        }

		void ShowPanel(GameObject obj)
		{
			if (obj == null) return;

			_switcher.ShowPanel(obj, _panels);
        }
    }

}