using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class The_Text_Control : MonoBehaviour
{
    public Transform _Transform;

    private Vector3 offset;
    
    public float Time_CD;

    private Animator _animator;

    private int _kant;

    public GameObject Text_Area;
    // Start is called before the first frame update
    void Start()
    {
        _animator =Text_Area.GetComponent<Animator>();
        offset = transform.position - _Transform.position;
        StartCoroutine(Time ());
    }

    IEnumerator Time()
    {
        while (true)
        {
            
            
            _animator.SetFloat("JudgeY", 0f);
            _animator.SetFloat("JudgeX", 0f);
            yield return new WaitForSeconds(3f);
            Text_Area.gameObject.SetActive(false);

            yield return new WaitForSeconds(Time_CD);
            Text_Area.gameObject.SetActive(true);

            _animator.SetFloat("JudgeY", 1f);

            yield return new WaitForSeconds(3f);
            Text_Area.gameObject.SetActive(false);

            _animator.SetFloat("JudgeY", 0f);


            yield return new WaitForSeconds(Time_CD);
            Text_Area.gameObject.SetActive(true);

            _animator.SetFloat("JudgeX", 1f);
            _animator.SetFloat("JudgeY",0.1f);

            yield return new WaitForSeconds(3f);
            Text_Area.gameObject.SetActive(false);

            _animator.SetFloat("JudgeX", 0f);
            _animator.SetFloat("JudgeY",0.1f);
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _Transform.position + offset;
    }
}
