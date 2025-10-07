using UnityEngine;
using TMPro;
using Player47.Domain.Souls;

namespace Player47.UI.HUD
{
    /// <summary>
    /// Atualiza a interface do usuário (HUD) com a quantidade atual de almas do jogador, com base no evento do <see cref="SoulsManager"/>.
    /// </summary>
    public class SoulsHUD : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _soulsText;
        
        void Start()
        {
            //Se inscreve para ouvir mudanças na quantidade de almas
            SoulsManager.Instance.AddSoulsListener(UpdateSoulsUI); 

            //Atualiza de cara com o valor inicial
            UpdateSoulsUI(SoulsManager.Instance.Souls);
        }

        void UpdateSoulsUI(int newSouls)
        {
            _soulsText.SetText("{0}", newSouls);
        }
    }
}