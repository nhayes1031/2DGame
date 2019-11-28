using Platformer.Scripts.Combat;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/ThrowProjectile")]
    public class ThrowProjectile : Action
    {
        public ProjectileSpawner objectSpawner;
        public AbilityCost Cost;

        public override void Act(GameObject obj)
        {
            SpawnLocation sl = obj.GetComponent<SpawnLocation>();

            Vector2 originPoint = sl != null ? sl.location.position : obj.transform.position;

            Cost.Run(obj);
            objectSpawner.Run(originPoint);
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
