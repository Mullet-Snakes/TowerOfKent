﻿using System.Collections.Generic;
using UnityEngine;
using Gravity;

public static class GravityManager
{
    public delegate void ChangeGravity(Vector3 worldGrav, bool changingTargeted);
    public static event ChangeGravity GravityChange;
    public static Vector3 worldGravity = new Vector3(0, -9.8f, 0);
    private static List<GravityForce> currentGravityList = new List<GravityForce>();

    public static void AddToGravityList(Vector3 centre, Vector3 halfExtents, Quaternion orientation, LayerMask layer)
    {
       
        Collider[] hit = Physics.OverlapBox(centre, halfExtents / 2, orientation, layer);

        foreach(Collider c in hit)
        {
            currentGravityList.Add(c.GetComponent<GravityForce>());
        }
    }

    private static void NewGrav(Vector3 grav, bool changingTargeted)
    {
        foreach (GravityForce g in currentGravityList)
        {
            g.SetCurrentGravity(grav, changingTargeted);
        }
    }

    public static void ClearCurrentList()
    {
        currentGravityList.Clear();
    }

    public static void ChangeGrav(Vector3 worldGrav, bool changingTargeted)
    {
        worldGravity = worldGrav;
        if(changingTargeted)
        {
            NewGrav(worldGrav, true);
        }
        else
        {
            NewGrav(worldGrav, false);
        }
        
        GravityChange?.Invoke(worldGrav, changingTargeted);
    }
}
