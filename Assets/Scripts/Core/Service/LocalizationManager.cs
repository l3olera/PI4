using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Player47.Core.Service
{
    /// <summary>
    /// Gerencia todo o sistema de localização do jogo utilizando o pacote **Unity Localization**.
    /// 
    /// Este gerenciador centraliza o acesso às traduções, permitindo:
    /// - Carregar e armazenar todas as traduções configuradas no inspector.
    /// - Acessar traduções simples e textos com variáveis dinâmicas (Smart Strings).
    /// - Trocar o idioma do jogo em tempo de execução.
    /// - Notificar outros sistemas (HUD, menus, diálogos) quando as traduções forem carregadas.
    /// 
    /// <para><b>Padrão de Projeto:</b> Singleton — garante apenas uma instância ativa no jogo.</para>
    /// 
    /// <para><b>Smart Strings:</b> permite interpolar valores dinâmicos dentro de textos localizados
    /// utilizando placeholders. Exemplo:</para>
    /// <example>
    /// Texto no Localization Table:
    /// - PT-BR: "Vida Atual: {0}"
    /// - EN: "Current Health: {0}"
    /// 
    /// Código:
    /// <code>
    /// string texto = await LocalizationManager.Instance.GetFormattedTranslation("UI_HealthInfo", 120);
    /// // Resultado: "Vida Atual: 120"
    /// </code>
    /// </example>
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 SINGLETON
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Instância única do LocalizationManager disponível globalmente.
        /// </summary>
        public static LocalizationManager Instance;

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 ESTRUTURAS DE DADOS
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Representa uma entrada de tradução configurada manualmente no Inspector.
        /// Cada entrada possui uma chave identificadora e uma referência a um <see cref="LocalizedString"/>.
        /// </summary>
        [Serializable]
        public class TranslationEntry
        {
            /// <summary>
            /// Nome amigável da chave de tradução usada pelo código.
            /// Exemplo: "UI_HealthInfo" ou "Menu_StartButton".
            /// </summary>
            public string key;

            /// <summary>
            /// Referência à string localizada criada pelo sistema da Unity.
            /// Deve estar vinculada a uma tabela no Localization Settings.
            /// </summary>
            public LocalizedString localizedString;
        }

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 EVENTOS
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Evento disparado quando todas as traduções forem carregadas e o cache estiver pronto.
        /// Permite que sistemas externos atualizem seus textos (ex: menus ou HUDs).
        /// </summary>
        public static event Action OnFinishTranslations;

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 CONFIGURAÇÕES DE IDIOMA
        //───────────────────────────────────────────────────────────────────────────────

        [Header("Configurações de Idioma")]
        [Tooltip("Índice do idioma selecionado atualmente na Unity Localization.")]
        int _languageIndex = 0;

        /// <summary>
        /// Índice do idioma atual selecionado no sistema de Locales da Unity.
        /// É salvo e restaurado automaticamente pelo PlayerPrefs.
        /// </summary>
        public int CurrentLanguageIndex
        {
            get => _languageIndex;
            set => _languageIndex = value;
        }

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 LISTA DE TRADUÇÕES CONFIGURADAS
        //───────────────────────────────────────────────────────────────────────────────

        [Header("Lista de Traduções")]
        [Tooltip("Lista de chaves e LocalizedStrings que serão pré-carregadas e armazenadas em cache.")]
        public List<TranslationEntry> translations;

        /// <summary>
        /// Cache interno de traduções já resolvidas.
        /// Utilizado para strings simples (sem placeholders).
        /// <br/> key → texto traduzido.
        /// </summary>
        Dictionary<string, string> _cachedTranslations = new();

        /// <summary>
        /// Cache interno de referências diretas a <see cref="LocalizedString"/>.
        /// Usado quando há variáveis dinâmicas (Smart Strings com placeholders).
        /// </summary>
        Dictionary<string, LocalizedString> _localizedReferences = new();

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 CICLO DE VIDA
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Garante a inicialização única (Singleton) e carrega as traduções.
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
        /// Recupera o idioma salvo anteriormente (ou define Português como padrão).
        /// </summary>
        void Start()
        {
            _languageIndex = PlayerPrefs.GetInt("Language", 0); // 0 = Português por padrão
        }

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 CARREGAMENTO DE TRADUÇÕES
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Carrega todas as traduções definidas no inspector e as armazena em cache.
        /// </summary>
        async Task LoadAllTranslations()
        {
            _cachedTranslations.Clear();
            _localizedReferences.Clear();

            foreach (var entry in translations)
            {
                // Solicita o texto localizado atual (sem variáveis)
                var handle = entry.localizedString.GetLocalizedStringAsync();
                await handle.Task;

                // Armazena tanto o texto quanto a referência original
                _cachedTranslations[entry.key] = handle.Result;
                _localizedReferences[entry.key] = entry.localizedString;
            }

            // Notifica outros sistemas que as traduções estão prontas
            OnFinishTranslations?.Invoke();
            Debug.Log("<color=red>🗣️ Todas as traduções foram carregadas com sucesso!</color>");
        }

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 ACESSO A TRADUÇÕES
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retorna uma tradução simples (sem variáveis dinâmicas) com base na chave informada.
        /// </summary>
        /// <param name="key">Chave de tradução registrada na lista do inspector.</param>
        /// <returns>Texto traduzido ou <c>[key]</c> se a chave não for encontrada.</returns>
        public string GetTranslation(string key)
        {
            if (_cachedTranslations.TryGetValue(key, out var value))
                return value;

            return $"[{key}]"; // Exibe a chave entre colchetes para depuração
        }

        /// <summary>
        /// Retorna uma tradução formatada que utiliza placeholders numéricos (Smart Strings posicionais).
        /// 
        /// <para>Exemplo:</para>
        /// Tabela de tradução:
        /// - PT-BR: "Vida Atual: {0} / {1}"
        /// - EN: "Current Health: {0} / {1}"
        /// 
        /// <para>Uso no código:</para>
        /// <code>
        /// string texto = await LocalizationManager.Instance.GetFormattedTranslation("UI_HealthInfo",
        ///     PlayerHealth.Instance.CurrentHealth,
        ///     PlayerHealth.Instance.MaxHealth);
        /// </code>
        /// </summary>
        /// <param name="key">Chave da string localizada.</param>
        /// <param name="args">Valores que substituirão os placeholders ({0}, {1}, ...).</param>
        /// <returns>Texto localizado já formatado com os valores inseridos.</returns>
        public async Task<string> GetFormattedTranslation(string key, params object[] args)
        {
            if (!_localizedReferences.TryGetValue(key, out var localized))
            {
                Debug.LogWarning($"Chave de localização '{key}' não encontrada.");
                return $"[{key}]";
            }

            // Cria uma cópia local da LocalizedString para evitar conflitos entre múltiplas chamadas
            var localizedCopy = new LocalizedString(localized.TableReference, localized.TableEntryReference)
            {
                Arguments = args
            };

            // Aguarda a resolução da string localizada
            var handle = localizedCopy.GetLocalizedStringAsync();
            await handle.Task;

            return handle.Result;
        }

        //───────────────────────────────────────────────────────────────────────────────
        // 🔹 TROCA DE IDIOMA
        //───────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Altera o idioma do jogo e recarrega todas as traduções no novo idioma.
        /// </summary>
        /// <param name="languageIndex">Índice do idioma na lista de Locales configurada na Unity.</param>
        public async void SelectLanguage(int languageIndex)
        {
            // Define o idioma ativo na Unity Localization
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

            // Salva a preferência do jogador
            PlayerPrefs.SetInt("Language", languageIndex);
            PlayerPrefs.Save();

            // Recarrega o cache com as novas traduções
            await LoadAllTranslations();
        }
    }
}
