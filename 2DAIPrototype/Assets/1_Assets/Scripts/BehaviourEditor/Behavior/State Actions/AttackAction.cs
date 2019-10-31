﻿using SA;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI2/Actions/Attack")]
    public class AttackAction : StateActions
    {
        private AttackCommand attackCom = new AttackCommand();

        public override void Execute(StateManager states)
        {
            attackCom.Execute(states.gameObject);
        }
    }
}