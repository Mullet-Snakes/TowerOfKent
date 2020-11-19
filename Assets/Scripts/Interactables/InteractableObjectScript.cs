using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Default: 2")]
    [Range(0, 5)]
    protected float distanceToInteract = 2f;

    private void OnEnable()
    {
        InteractKeyManager.OnButtonPress += CheckForInteract;
    }

    private void OnDisable()
    {
        InteractKeyManager.OnButtonPress -= CheckForInteract;
    }

    protected virtual void CheckForInteract(GameObject playerPos)
    {
        return;
    }
}
