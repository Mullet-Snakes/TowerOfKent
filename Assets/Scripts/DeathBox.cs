﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private GameObject spawnObject = null;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().OnDeath(spawnObject.transform.position);

            LiftObject lift = player.GetComponent<LiftObject>();

            if(lift != null)
            {
                if(lift.IsHolding)
                {
                    lift.DropObject();
                }
            }
        }

        if(other.CompareTag("LevelProp"))
        {
            LiftObject lift = player.GetComponent<LiftObject>();

            if(lift.IsHolding)
            {
                lift.DropObject();
            }

            Destroy(other.gameObject);
        }
    }
}
