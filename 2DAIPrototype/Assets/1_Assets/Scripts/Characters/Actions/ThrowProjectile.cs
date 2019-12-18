using Platformer.Scripts.Combat;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/ThrowProjectile")]
    public class ThrowProjectile : Action
    {
        public ProjectileSpawner objectSpawner;
        public AbilityCost Cost;
        public AbilityCooldown Cooldown;

        public override void Act(GameObject obj)
        {
            Cost.Run(obj);
            objectSpawner.Run(obj);
        }

        public override void OnEnter()
        {
            return;
        }

        public override void OnExit()
        {
            return;
        }
    }
}
