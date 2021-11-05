using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] public Slider HealthBar;
    public Health Hp;

    private void Start()
    {
        Hp = new Health();
    }
}
