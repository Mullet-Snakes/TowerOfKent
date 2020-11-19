using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public GameObject deathTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == deathTarget.tag)
        {
            if (deathTarget.CompareTag("Player"))
            {
                deathTarget.GetComponent<PlayerController>().OnDeath();

                if (deathTarget.GetComponent<LiftObject>().IsHolding == true)
                {
                    deathTarget.GetComponent<LiftObject>().DropObject();
                }
            }
            else if(deathTarget.CompareTag("LevelProp"))
            {

            }
        }
    }
}
