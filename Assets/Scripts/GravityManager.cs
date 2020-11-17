using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

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

    private static void NewGrav()
    {
        foreach (GravityForce g in currentGravityList)
        {
            g.SetCurrentGravity(worldGravity, false);
        }
    }

    public static void ClearCurrentList()
    {
        currentGravityList.Clear();
    }

    public static void ChangeGrav(Vector3 worldGrav, bool changingTargeted)
    {
        worldGravity = worldGrav;
        NewGrav();
        //GravityChange?.Invoke(worldGrav, changingTargeted);
    }
}
