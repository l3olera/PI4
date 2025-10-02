using UnityEngine;
using Player47.Core.Managers;

namespace Player47.Domain.Weapon.Implementations
{
    /// <summary>
    /// Controla a animação e poisção de mira da arma
    /// </summary>
    public class WeaponAim : MonoBehaviour
    {
        void OnEnable()
        {
            InputManager.AimInputPerformed += HandleAimInputPerformed;
        }

        void OnDisable()
        {
            InputManager.AimInputPerformed -= HandleAimInputPerformed;    
        }

        void HandleAimInputPerformed(bool isAiming)
        {
            //Colocar aqui a animação para ficar na mira da arma (iron sight)
        }
    }
}