using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    public interface IController
    {
        event System.Action<Vector2> OnMove;
        event System.Action<Vector2> OnLook;
        event System.Action OnJump;
        event System.Action OnJumpReleased;
        event System.Action OnAttack;
    }
}
