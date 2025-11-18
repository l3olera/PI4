namespace Player47.Domain.Weapon.Abstractions
{
    public interface IWeapon
    {
        int ScoreMaxAmmo { get; }
        int Damage { get; }
        float FireRate { get; }

        void Shoot();

        void Reload();

        void IncreaseDamage(int amount);

        void IncreaseFireRate(float amount);
    }

}