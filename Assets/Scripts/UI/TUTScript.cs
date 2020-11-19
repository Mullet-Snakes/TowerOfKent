using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTScript : MonoBehaviour
{
    public GameObject wasdtut;
    public GameObject keytut;
    public GameObject doortut;
    public GameObject doortutCollider;
    public GameObject gravobjtut;
    public GameObject gravobjtutR;
    public GameObject gravplayertut;
    public GameObject pressuretut;
    public GameObject buttontut;
    public GameObject textbox;
    
    // Start is called before the first frame update
    void Start()
    {
        wasdtut.SetActive(false);
        keytut.SetActive(false);
        doortut.SetActive(false);
        gravobjtut.SetActive(false);
        gravobjtutR.SetActive(false);
        gravplayertut.SetActive(false);
        pressuretut.SetActive(false);
        buttontut.SetActive(false);
        textbox.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wasdtut")
        {
            wasdtut.SetActive(true);
            textbox.SetActive(true);
        }

        else if (other.gameObject.tag == "keytut")
        {
            keytut.SetActive(true);
            textbox.SetActive(true);
        }

        else if (other.gameObject.tag == "gravobjtut")
        {
            gravobjtut.SetActive(true);
            gravobjtutR.SetActive(true);
            textbox.SetActive(true);
            doortut.SetActive(false);
            Destroy(doortutCollider);
        }

        else if (other.gameObject.tag == "gravplaytut")
        {
            gravplayertut.SetActive(true);
            textbox.SetActive(true);
        }

        else if (other.gameObject.tag == "presstut")
        {
            pressuretut.SetActive(true);
            textbox.SetActive(true);
        }

        else if (other.gameObject.tag == "buttontut")
        {
            buttontut.SetActive(true);
            textbox.SetActive(true);
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
            textbox.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "keytut")
        {
            keytut.SetActive(false);
            textbox.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "gravobjtut")
        {
            gravobjtut.SetActive(false);
            gravobjtutR.SetActive(false);
            textbox.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "gravplaytut")
        {
            gravplayertut.SetActive(false);
            textbox.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "presstut")
        {
            pressuretut.SetActive(false);
            textbox.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "buttontut")
        {
            buttontut.SetActive(false);
            textbox.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    void doortutorialTrigger()
    {
        if (KeyChainScript.HasKey("bluekey") == true)
        {
            doortut.SetActive(true);
            textbox.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        doortut.SetActive(false);
        textbox.SetActive(false);
        Destroy(doortutCollider);
    }
}
