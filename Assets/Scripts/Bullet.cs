using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0;
    public GameObject Bullet_Impact;
    public int damage =50;
    void Start()
    {
        
    }
    public void SeekTarget(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if(target ==null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distancethisframe = speed * Time.deltaTime;
        if(dir.magnitude<= distancethisframe)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distancethisframe,Space.World);
        transform.LookAt(target);
    }
    void HitTarget()
    {
        GameObject bullet_impactGO = Instantiate(Bullet_Impact, transform.position, transform.rotation);
        Destroy(bullet_impactGO, 5f);
        if(explosionRadius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if(collider.CompareTag("Enemy"))
                {
                    Damage(collider.transform);
                }
            }
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
        
        Debug.Log("Hit");
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e !=null)
        {
            e.takeDamage(damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
