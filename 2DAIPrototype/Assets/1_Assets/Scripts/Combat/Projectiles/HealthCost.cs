using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Cost/Health")]
    public class HealthCost : AbilityCost
    {
        public int healthCost = 0;
        public CostType costType = CostType.subtract;
        
        public override void Run(GameObject obj)
        {
            Health health = obj.GetComponent<Health>();
            switch (costType)
            {
                case CostType.subtract:
                    health.Subtract(healthCost);
                    break;
                case CostType.add:
                    health.Add(healthCost);
                    break;
            }
            health.TakeDamage(healthCost);
        }

        public enum CostType
        {
            add,
            subtract,
        }
    }
}
