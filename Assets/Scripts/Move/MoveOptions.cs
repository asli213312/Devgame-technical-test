using System;
using UnityEngine;

[Serializable]
public abstract class AbstractMoveOptions 
{
   [SerializeField] public float moveSpeed;
}

[Serializable]
public class MoveOrthogonallyOptions : AbstractMoveOptions
{

}

[Serializable]
public class MoveLinearOptions : AbstractMoveOptions
{
    public Vector3 direction;
}