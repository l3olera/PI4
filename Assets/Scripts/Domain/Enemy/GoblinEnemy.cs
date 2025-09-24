using Player47.Domain.Enemy.Vision;

namespace Player47.Domain.Enemy
{
    public class GoblinEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();

            _vision = new NormalVision();
        }
    }
}