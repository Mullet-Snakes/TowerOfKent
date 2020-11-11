using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTScript : MonoBehaviour
{
    public GameObject wasdtut;
    public GameObject keytut;
    public GameObject doortut;
    public GameObject gravobjtut;
    public GameObject gravplayertut;
    public GameObject pressuretut;
    public GameObject buttontut;
    
    // Start is called before the first frame update
    void Start()
    {
        wasdtut.SetActive(false);
        keytut.SetActive(false);
        doortut.SetActive(false);
        gravobjtut.SetActive(false);
        gravplayertut.SetActive(false);
        pressuretut.SetActive(false);
        buttontut.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wasdtut")
        {
            wasdtut.SetActive(true);
        }

        else if (other.gameObject.tag == "keytut")
        {
            keytut.SetActive(true);
        }

        else if (other.gameObject.tag == "gravobjtut")
        {
            gravobjtut.SetActive(true);
            doortut.SetActive(false);
            Destroy(doortut);
        }

        else if (other.gameObject.tag == "gravplaytut")
        {
            gravplayertut.SetActive(true);
        }

        else if (other.gameObject.tag == "presstut")
        {
            pressuretut.SetActive(true);
        }

        else if (other.gameObject.tag == "buttontut")
        {
            buttontut.SetActive(true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "doortut")
        {
            doortutorialTrigger();
        }

    }
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "wasdtut")
        {
            wasdtut.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "keytut")
        {
            keytut.SetActive(false);
            Destroy(keytut);
        }

        else if (other.gameObject.tag == "gravobjtut")
        {
            gravobjtut.SetActive(false);
            Destroy(gravobjtut);
        }

        else if (other.gameObject.tag == "gravplaytut")
        {
            gravplayertut.SetActive(false);
            Destroy(gravplayertut);
        }

        else if (other.gameObject.tag == "presstut")
        {
            pressuretut.SetActive(false);
            Destroy(pressuretut);
        }

        else if (other.gameObject.tag == "buttontut")
        {
            buttontut.SetActive(false);
            Destroy(buttontut);
        }
    }

    void doortutorialTrigger()
    {
        if (KeyChainScript.HasKey("bluekey") == true)
        {
            doortut.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        doortut.SetActive(false);
        Destroy(doortut);
    }
}
