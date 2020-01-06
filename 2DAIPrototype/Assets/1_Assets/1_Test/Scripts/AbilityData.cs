using System.Collections;
using UnityEngine;

public class AbilityData : MonoBehaviour
{
    [SerializeField] public AbilityNames ability;
}

public enum AbilityNames
{
    DoubleJump,
    Dash
};
