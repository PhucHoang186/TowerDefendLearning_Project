using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Rounds;
    public static float money;
    public int startMoney = 400;
    public static int Lives;
    public  int startLives = 20;
    private void Start()
    {
        money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }

}
