using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    public class HardwareInputHandler : MonoBehaviour
    {
        RightCommand rightCom;
        LeftCommand leftCom;
        AttackCommand attackCom;
        JumpCommand jumpCom;
        CrouchCommand crouchCom;
        IdleCommand idleCom;

        private void Start()
        {
            rightCom = new RightCommand();
            leftCom = new LeftCommand();
            attackCom = new AttackCommand();
            jumpCom = new JumpCommand();
            crouchCom = new CrouchCommand();
            idleCom = new IdleCommand();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                leftCom.Execute(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                rightCom.Execute(gameObject);
            }
            else if (Input.GetButtonDown("Jump"))
            {
                jumpCom.Execute(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                crouchCom.Execute(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attackCom.Execute(gameObject);
            }
            else
            {
                idleCom.Execute(gameObject);
            }
        }
    }
}
