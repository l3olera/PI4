using Player47.Domain.Enemy.Vision;

namespace Player47.Domain.Enemy.Types
{
    public class CyclopsEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();

            _vision = new TorchVision();
        }
    }
}