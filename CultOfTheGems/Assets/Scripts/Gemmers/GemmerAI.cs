using System;
using UnityEngine;

public enum GemmerGroup
{
    NoGroup,
    Player,
    Group1,
    Group2, 
    Group3, 
    Group4
}
public class GemmerAI : MonoBehaviour,Damageable
{
    [SerializeField] private int hp = 10;

    public int health
    {
        get => hp;
        set => hp = value;
    }
    [SerializeField] private float moveSpeed = 1f;
    private bool moveOverride = false;
    private GemmerGroup group = GemmerGroup.NoGroup;
    
    private IGemmerState currentState;
    private Transform tr;
    private Vector2 movementPoint;
    
    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }
    
    private void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        
    }
    private void Update()
    {
        if (moveOverride)
        {
            MoveTo();
        }
        else
        {
            currentState.UpdateState();
        }
    }

    public void MoveTo()
    {
        
    }
  
    public void SetState(IGemmerState newState)
    {
        currentState = newState;
        newState.EnterState(this);
    }
    public void SetMovementPoint(Vector2 newMovPoint)
    {
        movementPoint = newMovPoint;
        moveOverride = true;
    }
}
