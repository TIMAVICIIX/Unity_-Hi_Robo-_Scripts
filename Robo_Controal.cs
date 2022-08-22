using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = Unity.Mathematics.Random;

public class Robo_Controal : MonoBehaviour
{

    public float speed;

    private Rigidbody2D _rigidbody;

    private Animator _animator;

    private float _inputX, _inputY,_stopX,_stopY,_judgeB;
    
    private bool _behavirous;

    private bool _isIdle;

    private bool _isWalk;

    private float Attack_X;

    private float Attack_Y;

    private Vector2 Mouse_Posistion_Screen;

    public bool _move;
    
    void Start()
    {
        _move = true; 
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //StartCoroutine(Time());
    }

    void Update()
    {
        
         Move();
         
    }

    // Update is called once per frame
    void Move()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(_inputX, _inputY).normalized;
        _rigidbody.velocity = input * speed;
         
        //Judge The Attack behaviours 
        bool Mouse1 = Input.GetKeyDown(KeyCode.Mouse0);
        Mouse_Posistion_Screen = Input.mousePosition;
        
        //Change The Posistion of Mouse;
        //The 1920*1080 HD is Changeing,Hope I can Maek it Better;
        Attack_X = Mouse_Posistion_Screen.x-1920/2;
        Attack_Y = Mouse_Posistion_Screen.y-1080/2;
        
        //Chage the Way about Robo to Run or Attack
        bool Push_Shift = Input.GetKey(KeyCode.LeftShift);
        if (Push_Shift)
        {
            Mouse1 = false;
        }
        else
        {
            _animator.SetBool("IsRun",false);
            speed = 5f;
        }
        
        //If Robo don't Run,He can Attack
        if (Mouse1)
        {
          _animator.SetBool("IsIdle",false);
          _animator.SetBool("IsWalk",false);
          Change_Attack_Function();
        }
        else
        {
            _animator.SetBool("IsIdle",true);
            _animator.SetBool("IsWalk", true);
            if (input != Vector2.zero)
            {
                _animator.SetBool("IsMoving", true);
                if (Push_Shift)
                {
                    _animator.SetBool("IsRun", true);
                    speed = 8f;
                }

                _stopX = _inputX;
                _stopY = _inputY;
            }
            else
            {
                _animator.SetBool("IsMoving", false);

            }
        }


        _animator.SetFloat("InputX",_stopX);
            _animator.SetFloat("InputY",_stopY);

            void Change_Attack_Function()
            {
                _animator.SetFloat("Input_X_Attack",Attack_X);
                _animator.SetFloat("Input_Y_Attack",Attack_Y);
            }

    }

    /*IEnumerator Time()
    {
        Vector2 input = new Vector2(_inputX,_inputY).normalized;
        while (true)
        {
            yield return new WaitForSeconds(Text_CD);
            
            GameObject heart = Instantiate(Text, null);
            heart.transform.position =input*speed;

            yield return new WaitForSeconds(1.5f);
            
            Destroy(heart);
            

        }
    }*/
}
