﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyChainScript
{
    static private Dictionary<string, bool> keyChain = new Dictionary<string, bool>();

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

    static public bool HasKey(string keyName)
    {
        if(keyChain[keyName])
        {
            return 
        }

        bool hasKey = keyChain.ContainsKey(keyName) ? true : false;

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
