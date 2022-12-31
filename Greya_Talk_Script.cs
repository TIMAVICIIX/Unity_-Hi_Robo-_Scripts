using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


public class Greya_Talk_Script : MonoBehaviour
{

    public Sprite roleBasic;//get Role's Basic Portrait
    public Sprite[] expression;//get Role's Basic Expressions

    public GameObject talkButton; //get public button in Asset GameObject,the Button is public and it can use by everyone(Role)
    public GameObject mainCamera;//Get Main Camera
    
    public string txtFile;//We need find Role's Talk Content and buffered "in CollectionText" Class
    
    private GameObject _talkUI;//Find the UI of Talk

    private Image _portraitIn;//We need put portrait in this Border
    private Image _expressionIn;//We have to change Expression about Role in this Border
    
    private Collider2D _player;//if find player in Trigger
    private Rigidbody2D _playerRiGi;
    
    private bool _isEnter;//Watching Player into Trigger Range and transform value to FixedUpdate()
    private bool _isTalk;//judge player talking or not  
    private bool _isInputting;//judge Talk_UI is putting words or not

    private List<OriginalConversationContentClass> _talkContent;//buffered Talk Content
    private CollectionText _buffer;//Responsible for breaking down the lines in the text,classifying them into lists and check it

    private TextMeshProUGUI _talkInput;//Input text on UI 
    private TextMeshProUGUI _roleName;//Input Role's name on UI
    //private Animator _talkContentButton;//Watching Talk Content Button

    private Robo_Controal _playerScript;//Get Player's Control Script,and unable it when player is talking 

    private int _nextContentSerial;//Get next Sentence serial

    void Awake()
    {
        _isEnter = false;//Set Talk Button is UnVisible,public Button can moving everywhere in this scene
        _talkUI = GameObject.Find("Talk_View");//Find Talk UI at First
        _buffer = new CollectionText(txtFile);//Initialize text File in Script
        _nextContentSerial = 1;
        
        if (_talkUI)
        {
            _talkUI.SetActive(false); //UnVisible
        }

        foreach (var tempObject in _talkUI.GetComponentsInChildren<Transform>())
        {
            if (tempObject.name == "Role_Pro")
            {
                _portraitIn = tempObject.GetComponent<Image>();//Find Talk Portrait Position at First
            }

            if (tempObject.name == "Role_Expression")
            {
                _expressionIn = tempObject.GetComponent<Image>();//Find Talk Portrait Expression at First
            }

            if (tempObject.name == "Role's TalkContent")
            {
                _talkInput = tempObject.transform.GetComponent<TextMeshProUGUI>();//Find Talk Text
            }

            if (tempObject.name == "Role's Name")
            {
                _roleName = tempObject.transform.GetComponent<TextMeshProUGUI>();//Get Name Text
            }

            /*if (tempObject.name == "Click_Button")
            {
                _talkContentButton = tempObject.GetComponent<Animator>();//Get Talk Content Button Animation
            }*/
            
        }
        
    }
    
    public void OnTriggerEnter2D(Collider2D other)//if we find Player into Range,Note down player message at First
    {
        if (other.CompareTag("Player"))
        {
            /*
             * A lot of components about Player
             */
            _player = other;
            _isEnter = true;
            _playerRiGi = _player.GetComponent<Rigidbody2D>();
            _playerScript = _player.GetComponent<Robo_Controal>();

        }

    }

    public void OnTriggerExit2D(Collider2D other)//Set Button Active in UnVisible when Player Exit this Area 
    {
        if (other.CompareTag("Player"))
        {
            _isEnter = false;
        }
    }

    public void FixedUpdate()
    {
        if (_isEnter&&!Input.GetKey(KeyCode.E)&&!_isTalk)
        {
            CalculationVector();//Start Method
            talkButton.SetActive(true);//Position changed,we can set Button Visible
        }else
        {
            talkButton.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E)&&_isEnter&&!_isTalk)
        {
            talkButton.SetActive(false);//Set Button in UnVisible
            _talkUI.SetActive(true);//Let Talk_UI Visible

            Talk_Start_Method();//Start Talking

        }



        if (_isTalk&&Input.GetKey(KeyCode.Mouse0)&&!_isInputting)
        {
            /*if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _talkContentButton.SetBool("IsClick",true);
            }
            else
            {
                _talkContentButton.SetBool("IsClick",false);
            }*/
            
            Talk_Continue_Method();
            _nextContentSerial = _buffer.NextContent(_nextContentSerial);
        }
    }

    private void CalculationVector()
    {
        Vector3 myVector3 = this.transform.position;//get Position with NPC
        Vector3 playerVector3 = _player.transform.position;//get Position with player

        double vectorX = Math.Max(myVector3.x, playerVector3.x) - Math.Min(myVector3.x, playerVector3.x);
        double vectorY = Math.Max(myVector3.y, playerVector3.y) - Math.Min(myVector3.y, playerVector3.y);
        /*
         * Try to find Max Value about X & Y,then X-Y
         * It can find a Vector between NPC and Player
         */

        talkButton.transform.position = new Vector3((float)(Math.Max(myVector3.x, playerVector3.x) - vectorX/2),
            (float)(Math.Max(myVector3.y, playerVector3.y) - vectorY/2), 0);
        /*
         *Transform Button's Position,it Between NPC and Player 
         */
        
        

    }

    private void Talk_Start_Method()
    {
        talkButton.SetActive(false);
        _isTalk = true;//Set E false,Can't Push KeyCode E to Quit Talk
        _talkContent = _buffer.GetOriginList();//Get Origin Talk Content
        
        _portraitIn.sprite = roleBasic;//Set Portrait about NPC
        _roleName.text = _buffer.GetFileName();//Set Role's Name
        
      
        
            foreach (var tempExpression in expression)
            {
                if (tempExpression.name == _talkContent[0].GetExpression())
                {
                    _expressionIn.sprite = tempExpression;//Find Right Expression replace it on
                } 
            }

            _talkInput.text = _talkContent[0].GetTalkContent();//inputting First Sentence at first 
            
            Move_Camera();


            _playerScript.enabled = false;

            _playerRiGi.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY |
                                      RigidbodyConstraints2D.FreezeRotation;//Freeze Player's X,Y,Z activity


    }

    private void Talk_Continue_Method()
    {
        if (_nextContentSerial == 0)
        {
            /*
             * If next serial have no Sentence,we must clear text Area,and ready for next talking
             */
            _talkInput.text = "";
            _roleName.text = "";
            _talkUI.SetActive(false);
            _isTalk = false;

            _playerRiGi.constraints = RigidbodyConstraints2D.FreezeRotation;//UnFreeze player's activity
            _playerScript.enabled = true;
        }


        foreach (var tempExpression in expression)
        {

            if (_talkContent[_nextContentSerial].GetExpression() == tempExpression.name)
            {
                _expressionIn.sprite = tempExpression;//This Talking Expression
            }
            
        }
        
        StartCoroutine(Content_Input_Method());//This Talking Sentence


    }


    IEnumerator Content_Input_Method()
    {
        _talkInput.text = "";
        _isInputting = true;
        
        char[] tempInput = _talkContent[_nextContentSerial].GetTalkContent().ToCharArray();

        foreach (var tempChar in tempInput)
        {
            _talkInput.text += tempChar;
            yield return new WaitForSeconds(0.1f);//0.1 Seconds per one words
        }

        _isInputting = false;

    }
    

    private void Move_Camera()
    { 
        
        /*
         * We need let Player and NPC don't covered by Talk_UI 
         */
    
        Vector3 cameraVector3 = mainCamera.transform.position;

        mainCamera.transform.position = new Vector3(cameraVector3.x, cameraVector3.y - 5f, cameraVector3.z);

    }
    
    
    
}
