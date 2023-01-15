﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 moveVec;
    float   speed;
    int     damage;

    public void Init(Vector3 pos, Vector3 moveVec, float speed, int damage)
    {
        transform.position = pos;
        this.moveVec       = moveVec;
        this.speed         = speed;
        this.damage        = damage;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Player":
                other.GetComponentInChildren<CharaBase>().Damage(damage);
                Destroy(this.gameObject);
                break;
            case "Ground":
            case "Wall":
                Destroy(this.gameObject);
                break;
        }
    }
}