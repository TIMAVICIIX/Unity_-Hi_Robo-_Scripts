using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_UI_Control : MonoBehaviour
{
    private GameObject _Normal_UI;

    private GameObject _Inside_UI;

    private bool Judge_E;
    //private bool Push_E;
    // Start is called before the first frame update
    void Start()
    {
        _Normal_UI = GameObject.Find("The Stuation");
         _Inside_UI=GameObject.Find("Inside_UI");
        
        _Inside_UI.SetActive(false);
        _Normal_UI.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        bool Push_E = Input.GetKeyDown(KeyCode.Tab);
        if (Push_E)
        {
            if (Judge_E)
            {
                Judge_E = false;
            }
            else
            {
                Judge_E = true;
            }

            if (Judge_E)
            {
                Judge_UI_ON();
            }
            else
            {
                Judge_UI_Off();
            }
        }
    }

    void Judge_UI_ON()
    {
        _Inside_UI.SetActive(true);
       _Normal_UI.SetActive(false);
       Time.timeScale = 0;
    }

    void Judge_UI_Off()
    {
        _Inside_UI.SetActive(false);
        _Normal_UI.SetActive(true);
        Time.timeScale = 1;
    }
    
}
