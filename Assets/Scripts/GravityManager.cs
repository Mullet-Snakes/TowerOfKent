using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class GravityManager
{
    public delegate void ChangeGravity(Vector3 worldGrav, bool changingTargeted);
    public static event ChangeGravity GravityChange;
    public static Vector3 worldGravity = new Vector3(0, -9.8f, 0);

    public static void ChangeGrav(Vector3 worldGrav, bool changingTargeted)
    {
        GravityChange?.Invoke(worldGrav, changingTargeted);
    }
}
