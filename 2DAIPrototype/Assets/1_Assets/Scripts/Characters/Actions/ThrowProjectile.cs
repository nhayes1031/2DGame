﻿using Platformer.Scripts.Combat;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/ThrowProjectile")]
    public class ThrowProjectile : Action
    {
        public ProjectileSpawner objectSpawner;
        public AbilityCost healthCost;

        public override void Act(GameObject obj)
        {
            healthCost.Run(obj);
            objectSpawner.Run(obj.transform.position);
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