﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public Text oxygenText;
    public Image oxygenBar;
    public static float CurrentOxygen = 100f;
    public float MaxOxygen = 100f;
    [SerializeField]
    private float tickRate = 10; //in ms --> 300 means -1 health every 3 seconds
    [SerializeField]
    private int decreasePerTick = 1;
    private float delaytime;
    private bool damaged = false;
    [SerializeField]
    private Image damageImage;
    [SerializeField]
    private float damageFlashSpeed;
    [SerializeField]
    private Color damageFlashColor;
    // Use this for initialization
   
    void Start()
    {
        delaytime = Time.time + tickRate;
        oxygenBar = (GameObject.Find("oxygenBar").GetComponent<Image>());
        CurrentOxygen = MaxOxygen;
        SetOxygenText();
    }
    void Update()
    {
        if (damaged) {
            DamageFlash();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentOxygen < 0)
        {
            CurrentOxygen = 0;
        }
        if (CurrentOxygen > MaxOxygen)
        {
            CurrentOxygen = MaxOxygen;
        }
        if (Time.time > delaytime)
        {
            HurtPlayer(decreasePerTick);//CurrentOxygen = CurrentOxygen - decreasePerTick;
            damaged = true;
            delaytime = Time.time + tickRate;
        }
        if (CurrentOxygen <= 0)
        {
            //Destroy(gameObject); //Death animation?
        }
        SetOxygenText();
        oxygenBar.fillAmount = CurrentOxygen / MaxOxygen;
    }
    public void HurtPlayer(int damage)
    {
        //CurrentOxygen -= damage;
        if (CurrentOxygen - damage < 0)
        {
            CurrentOxygen = 0;
        }
        else
        {
            CurrentOxygen -= damage;
        }
    }
    public void RestoreOxygen(int oxyamount)
    {
        CurrentOxygen += oxyamount;
        if (CurrentOxygen > MaxOxygen)
        {
            CurrentOxygen = MaxOxygen;
        }
    }


    public void SetOxygenText()
    {
        oxygenText.text = (CurrentOxygen/MaxOxygen*100).ToString();
    }


    public void DamageFlash()
    {
        if (damaged)
        {
            damageImage.color = damageFlashColor;

        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
}
