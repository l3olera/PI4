using UnityEngine;

namespace Player47.Domain.Weapon.Abstractions
{
    public interface IWeapon
	{
		[SerializeField] int scoreMaxAmmo { get; }
        
		void Shoot();
		
		void Reload();
	}

}