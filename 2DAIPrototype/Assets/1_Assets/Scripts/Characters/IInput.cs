using UnityEngine;

namespace Platformer.Scripts.Characters
{
    public interface IInput
    {
        void ReadInput();
        Vector2 Walk { get; }
        Vector2 Look { get; }
        bool JumpReleased { get; }
        bool JumpPressed { get; }
        bool PossessionPressed { get;}
        bool DashPressed { get; }
    }
}
