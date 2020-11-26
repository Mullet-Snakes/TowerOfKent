using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyChainScript
{
    static private Dictionary<string, bool> keyChain = new Dictionary<string, bool>();

    static public void ClearKeyChain()
    {
        keyChain.Clear();
    }

    static public void AddKey(string keyName)
    {
        if (keyName != null)
        {
            keyChain.Add(keyName, false);
        }       
    }

    static public void PickUpKey(string keyName)
    {
        if(keyName != null)
        {
            keyChain[keyName] = true;
        }
    }

    static public void RemoveKey(string keyName)
    {
        if (keyName != null)
        {
            keyChain[keyName] = false;
        }
    }

    static public bool HasKey(string keyName)
    {
        bool hasKey = keyChain[keyName] ? true : false;

        return hasKey;
    }

    static public void PrintKeyChain()
    {
        foreach(KeyValuePair<string, bool> key in keyChain)
        {
            Debug.Log(key.Key);
            Debug.Log(key.Value);
        }
    }
}
