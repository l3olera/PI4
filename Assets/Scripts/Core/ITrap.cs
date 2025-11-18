using Player47.Domain.Combat;

namespace Player47.Core
{
    public interface ITrap
    {
        void Activate(IDamageable damageable);
    }
}