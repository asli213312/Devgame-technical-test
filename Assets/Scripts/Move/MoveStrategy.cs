using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { Linear, Orthogonally }

public abstract class AbstractMoveStrategy
{
    public float MoveSpeed { get; set; }

    public AbstractMoveStrategy(AbstractMoveOptions options) 
    {
        MoveSpeed = options.moveSpeed;
    }

	public abstract void Handle();
}

public class MoveOrthogonallyStrategy : AbstractMoveStrategy 
{
    private Transform _moveObj;
    public MoveOrthogonallyStrategy(Transform moveObj, MoveOrthogonallyOptions options) : base (options)
    {
        _moveObj = moveObj;
    }

    public override void Handle()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0f, moveZ).normalized;

        _moveObj.Translate(moveDir * MoveSpeed * Time.deltaTime, Space.World);
    }
}

public class MoveLinearStrategy : AbstractMoveStrategy 
{
    private Vector3 _direction;
    private Transform _playerObj;

    public MoveLinearStrategy(Transform moveObj, MoveLinearOptions options) : base(options)
    {
        _playerObj = moveObj;
        _direction = options.direction;
    }

    public override void Handle() 
    {
        _playerObj.position += _direction * MoveSpeed * Time.deltaTime;
    }
}
