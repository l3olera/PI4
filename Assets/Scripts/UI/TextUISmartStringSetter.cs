using Player47.Core.Service;
using TMPro;
using UnityEngine;

namespace Player47.UI
{
    public class TextUISmartStringSetter : MonoBehaviour
    {
        public static TextUISmartStringSetter Instance { get; private set; }

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async void SetText(TextMeshProUGUI textUI, string key, params object[] args)
        {
            if (LocalizationManager.Instance == null)
                return;

            // Obtém o texto localizado com o valor atual de vida
            string text = await LocalizationManager.Instance.GetFormattedTranslation(
                key,
                args
            );

            // Atualiza o texto na tela
            textUI.SetText(text);
        }
    }
}