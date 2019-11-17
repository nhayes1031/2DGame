using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Cost/None")]
    public class NoCost : AbilityCost
    {
        public override void Run(GameObject obj) {
        }
    }
}
