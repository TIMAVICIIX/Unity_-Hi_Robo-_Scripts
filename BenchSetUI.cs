using UnityEngine;

public class BenchSetUI : MonoBehaviour
{
    public GameObject Set01;

    public GameObject Set02;

    private Animator _animator01;

    private Animator _animator02;

    private Animator _player;

    private bool Judge_F;

    private bool Push_F;
    //Min Set Position;
    float Temp_X;
    float Temp_Y;

    private Collider2D other_Alter;
    
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        Set01.SetActive(false);
        Set02.SetActive(false);

        _animator01 =Set01.GetComponent<Animator>();
        _animator02 = Set02.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            Set01.SetActive(true);
            Set02.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Robo")
        {
            Set01.SetActive(false);
            Set02.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _player =other.GetComponent<Animator>();


        if (other.name == "Robo")
        {
            //Transform the Object

            other_Alter = other;
            _rigidbody2D = other_Alter.GetComponent<Rigidbody2D>();
            
            //Sit down again within range
            if (!Judge_F)
            {
                Set01.SetActive(true);
                Set02.SetActive(true);
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
            float Get_X01 = Mathf.Abs(Set01.transform.position.x-other.transform.position.x);
            float Get_Y01 = Mathf.Abs(Set01.transform.position.y-other.transform.position.y);
            float Get_X02 = Mathf.Abs(Set02.transform.position.x-other.transform.position.x);
            float Get_Y02 = Mathf.Abs(Set02.transform.position.y-other.transform.position.y);

            float Vector_01 = Mathf.Sqrt(Get_X01 * Get_X01 + Get_Y01 * Get_Y01);
            float Vector_02 = Mathf.Sqrt(Get_X02 * Get_X02 + Get_Y02 * Get_Y02);
            
            if (Vector_01<Vector_02)
            {
                _animator01.SetBool("Closer",true);
                _animator02.SetBool("Closer",false);
                Temp_X = Set01.transform.position.x;
                Temp_Y = Set01.transform.position.y;
            }
            else
            {
                _animator02.SetBool("Closer",true);
                _animator01.SetBool("Closer",false);
                Temp_X = Set02.transform.position.x;
                Temp_Y = Set02.transform.position.y;
            }
            
           

        }

    }

    // Update is called once per frame
    
   
    void Update()
    {
        //Detect the Pressing and repressing of the F key
        Push_F = Input.GetKeyDown(KeyCode.F);
        bool Push_F_transform = Push_F;
        
        //Don't Move this Method!!!!
        Seat_With_Player(Push_F_transform);
       
    }

    //!!!!!!!!!LOG!!!!!!!!!//
    /*
     * I tried to put the following idea into Method "OnTriggerStay()",but the frame count and
     * performance issues of Method "OnTriggerStay()" didn't allow it to run Stably.So I mounted
     * it into Method "Update()|FixedUpdate()".
     */
    void Seat_With_Player(bool Push_F_transform)
    {
        //Determine the odd and even times F is pressed
        if (Push_F_transform)
        {
            if (Judge_F)
            {
                Judge_F = false;
                //Debug.Log("Off");
            }
            else
            {
                Judge_F = true;
                //Debug.Log("Sit");
            }



            if (Judge_F)
            {
                //UI disappear
                Set01.SetActive(false);
                Set02.SetActive(false);
                
                //Let Player on the Set,Freeze All of Rotation and Position
                other_Alter.transform.position = new Vector3(Temp_X, Temp_Y, 0);
                _player.SetBool("Set", true);
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX |
                                           RigidbodyConstraints2D.FreezePositionY |
                                           RigidbodyConstraints2D.FreezeRotation;
            }

            if (!Judge_F)
            {
                Set01.SetActive(true);
                Set01.SetActive(true);
                
                //Let Player out of the Sit and Thaw The Constraints X,Y,Freeze Rotation Z;
                _player.SetBool("Set", false);
                other_Alter.transform.position = new Vector3(Temp_X, Temp_Y + 0.5f, 0);
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }
    }
}
