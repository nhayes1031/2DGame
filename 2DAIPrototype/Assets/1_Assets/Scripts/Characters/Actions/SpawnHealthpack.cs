using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/SpawnHealthpack")]
    public class SpawnHealthpack : Action
    {
        public GameObject objectToSpawn;

        public override void Act(GameObject obj)
        {
            Instantiate(objectToSpawn, obj.transform.position, obj.transform.rotation);
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
