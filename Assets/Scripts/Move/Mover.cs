using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, IMovable
{
    [SerializeField] private MoveType moveType;
    [SerializeField] private Transform playerObj;

    [SerializeReference] private AbstractMoveOptions options;

    public float Speed { get => _moveStrategy.MoveSpeed; set => _moveStrategy.MoveSpeed = value; }

    #region Temp Options
    [SerializeField, HideInInspector] private MoveLinearOptions _moveLinearOptions;
    [SerializeField, HideInInspector] private MoveOrthogonallyOptions _moveOrthogonallyOptions;

    #endregion

    private AbstractMoveStrategy _moveStrategy;

    private void OnValidate() 
    {
        switch (moveType) 
        {
            case MoveType.Linear:
                options = _moveLinearOptions ?? new MoveLinearOptions();
                break;
            case MoveType.Orthogonally:
                options = _moveOrthogonallyOptions ?? new MoveOrthogonallyOptions();
                break;
        }
    }

    public void Initialize() 
    {
        SelectMoveStrategy();
    }
    
    public void Handle()
    {
        _moveStrategy.Handle();
    }

    public void SelectMoveStrategy() 
    {
        AbstractMoveStrategy selectedStrategy = null;

        switch(moveType) 
        {
            case MoveType.Linear:
                selectedStrategy = new MoveLinearStrategy(playerObj, options as MoveLinearOptions);
                break;
            case MoveType.Orthogonally:
                selectedStrategy = new MoveOrthogonallyStrategy(playerObj, options as MoveOrthogonallyOptions);
                break;
        }

        _moveStrategy = selectedStrategy;
    }
}
