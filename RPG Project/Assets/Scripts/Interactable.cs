
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float raidus = 3f;
    public Transform interactTransform;
    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;

    public virtual void Interact ()
    {
        //This method is ment to be overwritten
        Debug.Log("interact with " + transform.name);
    }

   public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

   public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactTransform.position);
            if(distance <= raidus)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactTransform.position, raidus);
    }
}


