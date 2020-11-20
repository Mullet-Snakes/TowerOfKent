using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public GameObject targetTag;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {


            other.transform.GetComponentInParent<PlayerController>().OnDeath();

        }

        if (other.tag == targetTag.tag)
        {
            if (targetTag.CompareTag("Player"))
            {
                print("Player Killed");
                targetTag.GetComponent<PlayerController>().OnDeath();

                if (targetTag.GetComponent<LiftObject>().IsHolding == true)
                {
                    targetTag.GetComponent<LiftObject>().DropObject();
                }
            }
            else if (targetTag.CompareTag("LevelProp"))
            {
                if (player.GetComponent<LiftObject>().IsHolding == true)
                {
                    player.GetComponent<LiftObject>().DropObject();
                }

                print("Object Killed");
                Destroy(other.gameObject);
            }
        }
    }
}
