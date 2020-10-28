using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNew : MonoBehaviour
{
    public void New()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
