using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BirdsFlyLeft : MonoBehaviour
{
    private Animator _animator;

    private bool Takeoff;

    private float Move_x=0.01f;

    private float Move_Y=0.01f;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            Debug.Log(other.name);
            _animator.SetBool("Is Tirrgering",true);
            Takeoff = true;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Takeoff)
        {
            transform.Translate(-Move_x,Move_Y,0);
            Invoke("DestroyMe",5);
        }
        
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
    
}
