namespace Platformer.Scripts.Characters.AI.Newcode
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}
