using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/Exorcise")]
    public class ExorciseAction : Action
    {
        public override void Act(GameObject obj)
        {
            Possessable poss = obj.GetComponent<Possessable>();
            poss.Exorcised();
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