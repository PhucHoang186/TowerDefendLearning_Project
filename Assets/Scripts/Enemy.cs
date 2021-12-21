using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private float health;
    public float maxhealth = 100f;
    [HideInInspector]
    public float enemy_speed;
    public int worth = 50;
    public GameObject DestroyEffect;
    public float startSpeed = 10f;
    public Image Healthbar;

    private void Start()
    {
        enemy_speed = startSpeed;
        health = maxhealth;
    }
    // Update is called once per frame

    public void takeDamage(float amount)
    {
        health -= amount;
        Healthbar.fillAmount = health/maxhealth;
        if(health<=0)
        {
            DestroyEnemy();
        }
    }
    void DestroyEnemy()
    {
        GameObject DestroyFx = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        PlayerStats.money += worth;
        Destroy(DestroyFx, 5f);
        Destroy(this.gameObject);
        WaveSpawner.enemyAlive--;
    }
    public void Slow(float pct)
    {
        enemy_speed = startSpeed *(1 - pct);
    }
}
