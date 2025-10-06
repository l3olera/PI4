using UnityEngine;

namespace Player47.Domain.Weapon.Abstractions
{
    public interface IWeapon
	{
		[SerializeField] int ScoreMaxAmmo { get; }
        
		void Shoot();
		
		void Reload();
	}

}