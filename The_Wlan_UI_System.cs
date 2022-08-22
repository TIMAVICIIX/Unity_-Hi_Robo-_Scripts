using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class The_Wlan_UI_System : MonoBehaviour
{
    //Sent Sprites in CS;
    public Sprite[] SpriteArray =new Sprite[2];
    //......Can Add more,Change the num with the ArrayList
    
    //Build and Get GameObject about UI Wlan
    public GameObject Wlan;
    public GameObject Place_Name;
    
    //Cute Robo reminding UI
    public GameObject FIND_UI;
    public float UI_CD=3f;
    private float Stratime;
    private float Livetime;

    //Get Components 
    private Animator _animator;
    private Image _image;
    
    //Get Is Enter?
    private bool InTo;
    
    //Be sure can Get Player's Position
    private Collider2D other_Alter;
    
    //Public the Size,We can Change the Range size
    private CircleCollider2D _circle;
    public float Range=60f;
    
    //Exit Change
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            Livetime = Time.time;
            _animator.SetBool("Finded",false);
            foreach (var Temp_Sprite in SpriteArray)
            {
                if (Temp_Sprite.name == "Not Found")
                {
                    _image.sprite = Temp_Sprite;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Robo")
        {
            Stratime = Time.time;
            _animator.SetBool("Finded",true);

        }
        if (Stratime - Livetime > UI_CD)
        {
            //Debug.Log("show");
            FIND_UI.SetActive(true);
            Invoke("Change_UI",2.09f);
        }
        
    }
    

    //Staying Change
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            InTo = true;
            other_Alter = other;
        }
        else
        {
            InTo = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get cute UI CD time and to false UI 
         Stratime = Time.time;
         Livetime = 0.0f;
         FIND_UI.SetActive(false);
        
        //Start Loading......
        other_Alter = GetComponent<Collider2D>();
        _animator = Wlan.GetComponent<Animator>();
        _image = Place_Name.GetComponent<Image>();
        _circle = GetComponent<CircleCollider2D>();
        
        //On the Start Method,Get Range Size
        _circle.radius = Range;

        /*Debug List
        *_animator.SetBool("Finded",true);
        *Debug.Log(SpriteArray[0].name+name);
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug List
        //Debug.Log("Startime"+Stratime);
        //Debug.Log("Livetime"+Livetime);
        
        Distance_Calculation();
        if (InTo)
        {
            Change_Sprite_With_Place_Name();
        }

    }

    void Change_UI()
    {
        FIND_UI.SetActive(false);
    }
    

    void Change_Sprite_With_Place_Name()
    {
        
        foreach (var Temp_Sprite in SpriteArray)
        {
            if (Temp_Sprite.name == name)
            {
                //Debug.Log("True");
                _image.sprite = Temp_Sprite;
            }
                
        }
        
    }
    
    

    void Distance_Calculation()
    {
        //_animator.SetBool("Finded",true);
        //Wlan Range is 60,But We can Change in outside
        
        //Get position with Player and Wlan Center and Calculation the Vector
        float Vector_X = Mathf.Abs(transform.position.x - other_Alter.transform.position.x);
        float Vector_Y = Mathf.Abs(transform.position.y - other_Alter.transform.position.y);
        //Vector
        float _distance = Mathf.Sqrt(Vector_X * Vector_X + Vector_Y * Vector_Y);

        //Compare Range and Vector,We can adapt 3/3 Method to Judge Animator
        if (_distance <= Range / 3)
        {
            _animator.SetFloat("Box_X",0.0f);
            _animator.SetFloat("Box_Y",0.0f);
        }

        if (_distance > Range / 3 && _distance <= 2 * Range / 3)
        {
            _animator.SetFloat("Box_X",0.1f);
            _animator.SetFloat("Box_Y",0.9f);
        }

        if (_distance <= Range && _distance > 2 * Range / 3)
        {
            _animator.SetFloat("Box_X",0.9f);
            _animator.SetFloat("Box_Y",0.1f);
        }


    }
}
