using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Player47.Core.Service
{
    /// <summary>
    /// Gerencia o sistema de localização do jogo utilizando o pacote Unity Localization.
    /// 
    /// Responsabilidades:
    /// - Carregar todas as traduções configuradas no inspector.
    /// - Permitir acesso rápido às traduções já carregadas via cache.
    /// - Gerenciar a seleção de idioma pelo jogador.
    /// - Notificar outros sistemas quando as traduções forem carregadas.
    /// 
    /// Padrão usado: Singleton (para garantir apenas uma instância).
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        /// <summary>
        /// Instância única do LocalizationManager (Singleton).
        /// </summary>
        public static LocalizationManager Instance;

        /// <summary>
        /// Estrutura que representa uma entrada de tradução no inspector.
        /// </summary>
        [Serializable]
        public class TranslationEntry
        {
            /// <summary>
            /// Nome amigável da chave (identificação no código).
            /// </summary>
            public string key;

            /// <summary>
            /// Referência ao <see cref="LocalizedString"/> configurado no inspector.
            /// </summary>
            public LocalizedString localizedString;
        }

        /// <summary>
        /// Evento disparado quando todas as traduções forem carregadas com sucesso.
        /// 
        /// Permite que outros sistemas (HUD, menus, diálogos) se atualizem dinamicamente.
        /// </summary>
        public static event Action OnFinishTranslations;

        [Header("Configurações de Idioma")]
        [Tooltip("Índice do idioma selecionado na Unity Localization.")]
        int _languageIndex = 0;

        /// <summary>
        /// Índice do idioma atualmente selecionado.
        /// </summary>
        public int CurrentLanguageIndex
        {
            get => _languageIndex;
            set => _languageIndex = value;
        }

        [Header("Lista de Traduções")]
        [Tooltip("Lista de chaves e LocalizedStrings configuradas para ficarem de cache.")]
        public List<TranslationEntry> translations;

        /// <summary>
        /// Cache interno de traduções já resolvidas (key → valor traduzido).
        /// </summary>
        Dictionary<string, string> _cachedTranslations = new();

        /// <summary>
        /// Garante a inicialização do Singleton e carrega todas as traduções na memória.
        /// </summary>
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

        /// <summary>
        /// Carrega a preferência de idioma salva pelo jogador.
        /// </summary>
        void Start()
        {
            // Se não existir um idioma salvo, usa Português (0) como padrão.
            _languageIndex = PlayerPrefs.GetInt("Language", 0);
        }

        /// <summary>
        /// Carrega todas as traduções configuradas no inspector e as armazena em cache.
        /// </summary>
        async Task LoadAllTranslations()
        {
            foreach (var entry in translations)
            {
                var handle = entry.localizedString.GetLocalizedStringAsync();
                await handle.Task; // Aguarda a conclusão da operação assíncrona.
                var localizedString = handle.Result;

                // Armazena a tradução no cache.
                _cachedTranslations[entry.key] = localizedString;
            }

            // Notifica que as traduções estão prontas.
            OnFinishTranslations?.Invoke();
            Debug.Log("<color=red>Terminou todas as traduções</color>");
        }

        /// <summary>
        /// Retorna a tradução associada à chave especificada.
        /// </summary>
        /// <param name="key">Chave de tradução definida no inspector.</param>
        /// <returns>Texto traduzido ou <c>[key]</c> se não encontrado.</returns>
        public string GetTranslation(string key)
        {
            if (_cachedTranslations.TryGetValue(key, out var value))
            {
                return value;
            }
            else
            {
                return $"[{key}]"; // Retorna a chave entre colchetes se não encontrada.
            }
        }

        /// <summary>
        /// Altera o idioma do jogo.
        /// </summary>
        /// <param name="languageIndex">Índice do idioma na lista de Locales da Unity.</param>
        public async void SelectLanguage(int languageIndex)
        {
            // Define o idioma na Unity Localization
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

            // Salva a preferência de idioma
            PlayerPrefs.SetInt("Language", languageIndex);
            PlayerPrefs.Save();

            // Recarrega todas as traduções no novo idioma
            _cachedTranslations.Clear();
            await LoadAllTranslations();
        }
    }
}
