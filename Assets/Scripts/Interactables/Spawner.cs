using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemToSpawn = null;
    private GameObject currentObject = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckIfNull");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator CheckIfNull()
    {
        YieldInstruction wfs = new WaitForSeconds(0.2f);

        while (true)
        {
            //print("checking");
            if (currentObject == null)
            {
                Spawn(transform.position, transform.rotation);
            }
            yield return wfs;
        }
    }

    public void Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject go = Instantiate(itemToSpawn, pos, rot) as GameObject;
        go.SetActive(true);
        currentObject = go;

    }
}
