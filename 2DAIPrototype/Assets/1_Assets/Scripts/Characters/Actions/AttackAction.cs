using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(GameObject obj) {
            Animator anim = obj.GetComponent<Animator>();

            AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            if (!(m_CurrentClipInfo[0].clip.name == "Attack")) anim.SetTrigger("Attack");
        }

        public override void OnEnter() {
            return;
        }

        public override void OnExit() {
            return;
        }
    }
}
