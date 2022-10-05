using UnityEngine;
//using UnityEngine.UIElements;

public class BenchSetUI : MonoBehaviour
{
    public GameObject set01;
    //get set01 GameObject,it's public and can push it in the component
    

    public GameObject set02;

    private Animator _animator01;

    private Animator _animator02;

    private Animator _player;

    private bool _judgeF;
    
    //Min Set Position;
    private float _tempX;
    private float _tempY;

    private Collider2D _otherAlter;
    
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        set01.SetActive(false);
        set02.SetActive(false);

        _animator01 =set01.GetComponent<Animator>();
        _animator02 = set02.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            set01.SetActive(true);
            set02.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            set01.SetActive(false);
            set02.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _player =other.GetComponent<Animator>();


        if (other.name == "Robo")
        {
            //Transform the Object

            _otherAlter = other;
            _rigidbody2D = _otherAlter.GetComponent<Rigidbody2D>();
            
            //Sit down again within range
            if (!_judgeF)
            {
                set01.SetActive(true);
                set02.SetActive(true);
            }

            //judge Light UI Button Method;
            /*
             * Calculate the absolute Vector distance from the Seat to The Player
             *
             * Method:Abs,Sqrt
             *
             * 1.Step
             * Temp_X1:Abs(X1-Player)
             * Temp_Y1:Abs(Y1-Player)
             *
             * Temp_X2:Abs(X2-Player)
             * Temp_Y2:Abs(Y2-Player)
             *
             * 2.Step
             *Absolute Vector1=Sqrt(Temp_X1*Temp_X1+Temp_Y1*Temp_Y1)
             *Absolute Vector2=Sqrt(Temp_X2*Temp_X2+Temp_Y2*Temp_Y2)
             *
             * 3.Step
             * Compare Vector number one with Vector number Two
             *
             * 4.Step
             *Light the position button with small absolute Vector Value
             */

            float getX01 = Mathf.Abs(set01.transform.position.x-other.transform.position.x);
            float getY01 = Mathf.Abs(set01.transform.position.y-other.transform.position.y);
            float getX02 = Mathf.Abs(set02.transform.position.x-other.transform.position.x);
            float getY02 = Mathf.Abs(set02.transform.position.y-other.transform.position.y);

            float vector01 = Mathf.Sqrt(getX01 * getX01 + getY01 * getY01);
            float vector02 = Mathf.Sqrt(getX02 * getX02 + getY02 * getY02);
            
            if (vector01<vector02)
            {
                _animator01.SetBool("Closer",true);
                _animator02.SetBool("Closer",false);
                _tempX = set01.transform.position.x;
                _tempY = set01.transform.position.y;
            }
            else
            {
                _animator02.SetBool("Closer",true);
                _animator01.SetBool("Closer",false);
                _tempX = set02.transform.position.x;
                _tempY = set02.transform.position.y;
            }
            
           

        }

    }

    // Update is called once per frame
    
   
    void Update()
    {
        //Detect the Pressing and repressing of the F key
        //Don't Move this Method!!!!
        Seat_With_Player(Input.GetKeyDown(KeyCode.F));
       
    }

    //!!!!!!!!!LOG!!!!!!!!!//
    /*
     * I tried to put the following idea into Method "OnTriggerStay()",but the frame count and
     * performance issues of Method "OnTriggerStay()" didn't allow it to run Stably.So I mounted
     * it into Method "Update()|FixedUpdate()".
     */
    void Seat_With_Player(bool pushFTransform)
    {
        //Determine the odd and even times F is pressed
        if (pushFTransform)
        {
            if (_judgeF)
            {
                _judgeF = false;
                //Debug.Log("Off");
            }
            else
            {
                _judgeF = true;
                //Debug.Log("Sit");
            }



            if (_judgeF)
            {
                //UI disappear
                set01.SetActive(false);
                set02.SetActive(false);
                
                //Let Player on the Set,Freeze All of Rotation and Position
                _otherAlter.transform.position = new Vector3(_tempX, _tempY, 0);
                _player.SetBool("Set", true);
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX |
                                           RigidbodyConstraints2D.FreezePositionY |
                                           RigidbodyConstraints2D.FreezeRotation;
            }

            if (!_judgeF)
            {
                set01.SetActive(true);
                set01.SetActive(true);
                
                //Let Player out of the Sit and Thaw The Constraints X,Y,Freeze Rotation Z;
                _player.SetBool("Set", false);
                _otherAlter.transform.position = new Vector3(_tempX, _tempY + 0.5f, 0);
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }
    }
}

