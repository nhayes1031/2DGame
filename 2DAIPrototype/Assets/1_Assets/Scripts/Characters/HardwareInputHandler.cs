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
        SpecialCommand specialCom;
        AltSpecialCommand altSpecialCom;
        MindControlCommand mindControlCom;

        private void Start()
        {
            rightCom = new RightCommand();
            leftCom = new LeftCommand();
            attackCom = new AttackCommand();
            jumpCom = new JumpCommand();
            crouchCom = new CrouchCommand();
            idleCom = new IdleCommand();
            specialCom = new SpecialCommand();
            altSpecialCom = new AltSpecialCommand();
            mindControlCom = new MindControlCommand();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
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
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                specialCom.Execute(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                altSpecialCom.Execute(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                mindControlCom.Execute(gameObject);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                leftCom.Execute(gameObject);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rightCom.Execute(gameObject);
            }
            else
            {
                idleCom.Execute(gameObject);
            }
        }
    }
}
