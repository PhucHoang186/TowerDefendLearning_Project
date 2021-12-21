using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    private Transform target;
    private Enemy targetEnemy;
    public float range = 15f;
    [Header("Use Bullets")]
    public GameObject BulletPrefab;
    private float fireCountDown = 0f;
    public float fireRate = 1f;
    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem ImpactEffect;
    public Light impactLight;
    public int damageOvertime = 30;
    public float slowPercent  =.5f;
    [Header("Unity setup fields")]
    public Transform partToRotate;
    public Transform firePoint;
    public float turnspeed = 10f;
    
    private void Start()
    {
        InvokeRepeating("updateTarget", 0f,0.5f);
        fireCountDown = 0f;
    }
    private void Update()
    {
        if (target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    ImpactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }
        LockOnTarget();
        if(useLaser)
        {
            Laser();

        }
        else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

        }
        fireCountDown -= Time.deltaTime;
    }
    void Laser()
    {
        targetEnemy.takeDamage(damageOvertime * Time.deltaTime);
        targetEnemy.Slow(slowPercent);
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            ImpactEffect.Play();
        }
        Vector3 dir = ( firePoint.position - target.position).normalized;
        ImpactEffect.transform.position = target.position + dir ;
        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bullet_clone = Instantiate(BulletPrefab,firePoint.position,Quaternion.identity);
        Bullet bullet = bullet_clone.GetComponent<Bullet>();
        if(bullet !=null)
        {
            bullet.SeekTarget(target);

        }
        
    }
    void updateTarget()
    {   
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            float DistancetoEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(DistancetoEnemy < shortestDistance)
            {
                shortestDistance = DistancetoEnemy;
                nearestEnemy = enemy;
            }
            if(nearestEnemy !=null && shortestDistance<= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
            else
            {
                target = null;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
