using UnityEngine;

namespace MidiaAdapt.Core.Interfaces
{
    public interface IWeapon
	{
		 [SerializeField] int scoreMaxAmmo { get; }

        void Shoot();
		void Reload();
	}

}