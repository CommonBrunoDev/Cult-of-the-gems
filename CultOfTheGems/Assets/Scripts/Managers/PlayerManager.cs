using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerState
    {
        Normal,
        MultiSelect,
    } 
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float cameraSpeed = 5f;
    [SerializeField] private float cameraSpeedModifier = 1.6f;
    
    private bool isHoldingInteract = false;
    
    private Vector2 moveInput;
    private Vector2 velocity;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { isHoldingInteract = true; }
        if (ctx.canceled) { isHoldingInteract = false; }

        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool canInteract = !GameManager.Instance.paused;
        
        if (ctx.performed && canInteract)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(cameraPos, Vector2.zero, 1f ,interactableLayer);
            Debug.Log("Elements n. = " + hits.Length);
            Debug.Log(cameraPos);
            if (hits.Length > 0)
            {
                RaycastHit2D hit = GetClosestInteractable(hits,cameraPos);
                Debug.Log(hit.collider.gameObject.name);
            }
            else
            {
                if (isHoldingInteract)
                {
                    
                }
            }
        }
    }
    private void Update()
    {
        
    }

    private RaycastHit2D GetClosestInteractable(RaycastHit2D[] hits, Vector3 position)
    {
        RaycastHit2D closest = hits[0];
        int interactPriority = hits[0].collider.gameObject.GetComponent<Interactable>().priority;
        float closestDistance = float.MaxValue;
        
        for (int i = 1; i < hits.Length; i++)
        {
            if (Vector3.Distance(hits[i].point, position) < closestDistance)
            {
                if (hits[i].collider.gameObject.GetComponent<Interactable>().priority > interactPriority)
                {closest = hits[i];}
            }
        }
        return closest;
    }
}
