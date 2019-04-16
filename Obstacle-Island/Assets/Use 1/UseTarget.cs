using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseTarget : MonoBehaviour
{
    public bool shouldBeDestroyedAfterUse = false;
    /**
     * In the Unity editor, set a callback function
     * that will be triggered whenever this
     * use target gets used
     */
    public UnityEvent OnItemUsed;

    /**
     * Can be called by another script to perform
     * "use" on this target. This causes the
     * callback to trigger
     */
    public void Use()
    {
        if (OnItemUsed != null)
        {
            OnItemUsed.Invoke();
            
            if (shouldBeDestroyedAfterUse)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
