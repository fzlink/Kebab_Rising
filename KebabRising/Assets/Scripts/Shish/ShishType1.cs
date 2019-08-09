using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShishType1 : ShishBase
{
	[Header("Main Properties")][Space]
	[SerializeField] [Range(0,10)] private int maxNumberOfSlots = 0;
	[SerializeField] [Range(0,100)] private int maxNumberOfBounces = 0;
	[SerializeField] [Range(0,3)] private float gravityScale = 0;
	[SerializeField] private Vector2 moveSpeed;
	[SerializeField] private bool canBreakGlass;
	[SerializeField] private bool canBurn;


    private bool isClicked;
	private int remainingBounces;
	[Header("Main Properties")][Space]
	[SerializeField] private Sprite burntShishSprite;

	void Start()
	{
		InitializeBaseProperties();
		remainingBounces = maxNumberOfBounces;
	}

	void Update()
	{
		//Debug.Log(canBurn+" "+base.CanBurn);
        TouchControl();
        TouchDrag();
	}

    private void TouchDrag()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //gameObject.transform.rotation = Vector3.Angle(Input.mousePosition, gameObject.transform.position);

            //gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), Input.mousePosition * -1);
            Debug.Log(Input.mousePosition);
            //Debug.Log(gameObject.transform.rotation);

            Vector3 mousePos = Input.mousePosition;

            //Mathf.Clamp(mousePos.y, 55, 1000);
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

            
            gameObject.transform.eulerAngles = new Vector3(0,0,Mathf.Clamp(    Mathf.Atan2(  (screenPos.y - transform.position.y), (screenPos.x - transform.position.x)   ), 1.57f,3.14f  )   * Mathf.Rad2Deg);
            
        }
    }

    private void TouchControl()
    {
    //    if (Input.GetMouseButtonDown(0))
    //        isClicked = true;

    //    if (isClicked)
    //    {
    //        //Debug.Log("HELEOLOELOEL");
    //        Vector2 angle = new Vector2(-1, 1);
    //        float moveSpeedX = angle.x * 3;
    //        float moveSpeedY = angle.y * 3;

    //        gameObject.transform.position += new Vector3(moveSpeedX, moveSpeedY, 0) * Time.fixedDeltaTime;

    //    }
    }

    //Initializes base class properties when object created.
    void InitializeBaseProperties()
	{
		base.MaxNumberOfSlots = maxNumberOfSlots;
		base.MaxNumberOfBounces = maxNumberOfBounces;
		base.GravityScale = gravityScale;
		base.MoveSpeed = moveSpeed;
		base.CanBreakGlass = canBreakGlass;
		base.CanBurn = canBurn;
	}

	private void CalculateMoveSpeed()
	{
	}

	private void Burn()
	{
		GetComponent<SpriteRenderer>().sprite = burntShishSprite;
		
		//burn related things will go here(animation, sound, etc.)
	}

	private void DestroyShish()
	{
		Destroy(gameObject, 0f);

		//shish destruction related things will go here(animation, sound, etc.)
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(canBurn && other.collider.CompareTag("Fire"))
		{
			Burn();
		}
	}
}
