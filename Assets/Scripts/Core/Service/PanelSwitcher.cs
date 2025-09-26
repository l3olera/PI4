using UnityEditorInternal;
using UnityEngine;

namespace Player47.Core.Service
{
    /// <summary>
    /// Responsável por gerenciar a exibição de um único painél dentre vários na interface do usuário.
    /// </summary>
    public class PanelSwitcher : MonoBehaviour
    {
        public static PanelSwitcher Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Alterna o estado do painel correspondente ao índice fornecido:
        /// Deve passar o array dos painéis.
        /// ativa o painel se estiver inativo e desativa se já estiver ativo.
        /// Todos os outros painéis são desativados.
        /// </summary>
        public void TogglePanel(int index, GameObject[] panels)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(i == index && !panels[i].activeSelf);
            }
        }

        /// <summary>
        /// Alterna o estado do painel fornecido como referência:
        /// Deve passar o array dos painéis.
        /// ativa o painel se estiver inativo e desativa se já estiver ativo.
        /// Todos os outros painéis são desativados.
        /// </summary>
        public void TogglePanel(GameObject panelToShow, GameObject[] panels)
        {
            foreach (var panel in panels)
            {
                Debug.Log(panel.name);
                panel.SetActive(panel == panelToShow && !panel.activeSelf);
            }
        }

        /// <summary>
        /// Ativa apenas o painel desejado pelo índice da lista.
        /// </summary>
        public void ShowPanel(int index, GameObject[] panels)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(i == index);
            }
        }

        /// <summary>
        /// Ativa apenas o painel desejado pelo GameObject de referência.
        /// </summary>
        public void ShowPanel(GameObject panelToShow, GameObject[] panels)
        {
            foreach (var panel in panels)
            {
                panel.SetActive(panel == panelToShow);
            }
        }
    } 
}
