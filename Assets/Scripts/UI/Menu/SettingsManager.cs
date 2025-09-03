using MidiaAdapt.Core;
using UnityEngine;

namespace MidiaAdapt.UI.Menu
{
	public class SettingsManager : MonoBehaviour
	{
		/// <summary>
		/// Responsável por gerenciar as telas da configuração do jogo, indicando para onde cada botão deve levar o usuário.
		/// </summary>
		/// 

		[Header("Settings Controller")]
        [SerializeField] SettingsController _controller;

        [Header("Panel Settings Options")]
		[SerializeField] GameObject _screeOptions;
		[SerializeField] GameObject _languageOptions;
		[SerializeField] GameObject _keyboardOptions;
        [SerializeField] GameObject _audioOptions;

        void Start()
        {
			if(_screeOptions != null && !_screeOptions.activeSelf)
			{
				_screeOptions.SetActive(true);
            }
        }

        public void SetWindowedMode(bool value)
		{
			_controller.SetWindowedMode(value);
		}
	}

}