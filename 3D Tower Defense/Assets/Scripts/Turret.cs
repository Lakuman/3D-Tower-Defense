﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Transform target;

    [Header("Attributes")]
    public float range = 20f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 5f;
    public GameObject bulletPrefabs;
    public Transform firePoint;
    

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else
            {
                target = null;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

    }

    // Update is called once per frame
    void Update () {
        if (target == null)
        {
            return;
        }
        //Lerp Is a smooth Transition
        //Quaternion is the equivalent of Vector3 but for rotation
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <=0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
            
        }
        fireCountDown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefabs, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet !=null)
        {
            bullet.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
