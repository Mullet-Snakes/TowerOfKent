using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other, AdvanceToMain ATM)
    {
        if (other.gameObject.tag == "Player")
        {
            ATM.FinishGame();
        }
    }
}
