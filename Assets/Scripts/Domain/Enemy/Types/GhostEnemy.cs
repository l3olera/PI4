using Player47.Domain.Enemy.Vision;

namespace Player47.Domain.Enemy.Types
{
    public class GhostEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();

            _vision = new TorchFearVision();
        }
    }
}