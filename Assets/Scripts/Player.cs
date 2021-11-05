using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static uint Shields = 0;
    public static uint Protection = 0;
    [SerializeField] public Slider HealthBar;
    public Health Hp;

    private void Start()
    {
        Hp = new Health();
        
        Hp.OnDeath += () => {
            Game.Status = -1;
            Destroy(transform.gameObject);
        };
    }
}
