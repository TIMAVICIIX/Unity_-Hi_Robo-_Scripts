using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Charing_System : MonoBehaviour
{
    //Get Battery_Low_UI
    public GameObject Battery_Low_UI;
    
    //Get Slide of Battery,define CD
    public GameObject Battery_Slide;
    private UnityEngine.UI.Slider _slider;
    public float CD=2f;
    private float Startime;
    private float Livetime;
    
    //About Charging_Station
    private Animator _animator;
    private bool In_Charging;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            In_Charging = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            In_Charging = false;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        Battery_Low_UI.SetActive(false);
        Livetime=Startime = Time.time;
        _slider = Battery_Slide.GetComponent<Slider>();
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Livetime = Time.time;
        if (Startime + CD < Livetime&&!In_Charging)
        {
            _slider.value--;
            Startime = Time.time;
        }

        if (_slider.value < 20&&!In_Charging)
        {
            Battery_Low_UI.SetActive(true);
        }
        else
        {
            Battery_Low_UI.SetActive(false);
        }

        if (In_Charging && _slider.value < 100)
        {
            _animator.SetBool("INTO",true);
            _slider.value+=0.001f;
        }
        else
        {
            _animator.SetBool("INTO",false);
        }

    }
}
