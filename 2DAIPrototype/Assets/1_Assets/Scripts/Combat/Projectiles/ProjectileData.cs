using System;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    public int ParentId { get; private set; }
    public void SetParentId(int id)
    {
        ParentId = id;
    }
}
