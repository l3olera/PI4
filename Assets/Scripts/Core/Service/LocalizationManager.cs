using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Player47.Core.Service
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;

        [System.Serializable]
        public class TranslationEntry
        {
            public string key; // Um nome amigável para identificação
            public LocalizedString localizedString; // Referência ao LocalizedString no inspector
        }

        public static event Action OnFinishTranslations;

        [Header("Configurações de Idioma")]
        int _languageIndex = 0; // Índice do idioma selecionado na Unity Localization
        
        public int CurrentLanguageIndex { 
            get => _languageIndex; 
            set => _languageIndex = value; 
        } // Índice do idioma atual

        [Header("Lista de Traduções")]
        public List<TranslationEntry> translations;

        Dictionary<string, string> _cachedTranslations = new();

        async void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                await LoadAllTranslations();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            //Verifica e aplica o idioma salvo
            _languageIndex = PlayerPrefs.GetInt("Language", 0); //0 - Português, 1 - Inglês (Se não existir, usa 0 como padrão)
        }

        async Task LoadAllTranslations()
        {
            foreach (var entry in translations)
            {
                var handle = entry.localizedString.GetLocalizedStringAsync();
                await handle.Task;
                var localizedString = handle.Result;
                _cachedTranslations[entry.key] = localizedString;
            }
            OnFinishTranslations?.Invoke();
        }

        public string GetTranslation(string key)
        {
            if (_cachedTranslations.TryGetValue(key, out var value))
            {
                return value;
            }
            else
            {
                return $"[{key}]";
            }
        }

        public void SelectLanguage(int languageIndex)
        {
            // Define o idioma na Unity Localization
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

            //Salva a preferência do idioma
            PlayerPrefs.SetInt("Language", languageIndex);
            PlayerPrefs.Save();
        }
    } 
}
