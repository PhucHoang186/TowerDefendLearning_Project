using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;
    private int wavepointIndex = 0;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[0];
    }
    private void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            Endpath();
            return;

        }
        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }
    void Endpath()
    {
        WaveSpawner.enemyAlive--;
        PlayerStats.Lives -= 1;
        Destroy(this.gameObject);
        return;
    }
    void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * enemy.enemy_speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
        enemy.enemy_speed = enemy.startSpeed;
    }
}
