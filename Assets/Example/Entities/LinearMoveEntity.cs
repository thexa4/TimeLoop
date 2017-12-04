using UnityEngine;
using System.Collections;

public struct LinearMoveData : LoopLib.IEntityState
{
    public float X { get; set; }
    public float Y { get; set; }
}

public class LinearMoveEntity : LoopLib.EntityType<LinearMoveData>
{
    public LinearMoveEntity() : base("LinearMove", 128) { }
}
