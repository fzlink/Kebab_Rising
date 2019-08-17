using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShishType1 : ShishBase
{
	[Header("Main Properties")][Space]
	[SerializeField] [Range(0,10)] private int maxNumberOfSlots = 0;
	[SerializeField] [Range(0,100)] private int maxNumberOfBounces = 0;
	[SerializeField] [Range(0,3)] private float gravityScale = 0;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float maxMoveSpeed;
	[SerializeField] private bool canBreakGlass;
	[SerializeField] private bool canBurn;

	private int remainingBounces;

	[Header("Other Physics")][Space]
	[SerializeField] private Sprite burntShishSprite;
	[SerializeField] private float maxScreenPosX = 5.3f;
	[SerializeField] private float minScreenPosY = -3.5f;
	[SerializeField] private float maxStretch = 300f;
	[SerializeField] private float screenSizeMultiplier = 3f;

	private Vector3 lastMousePosition;
	private Vector3 screenPos;
	private Vector3 clickPosition;	//first click position
	private float stretchAmount = 0f;
	private bool isFirstClick = false;
	//will be used to calculate same max stretch for different screen sizes.

	RectTransform rectTransform;
	void Start()
	{
		InitializeBaseProperties();
		remainingBounces = maxNumberOfBounces;
		rectTransform = GetComponent<RectTransform>();
		maxStretch = Vector2.Distance(new Vector2 (0,Screen.height), rectTransform.position) / screenSizeMultiplier;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isFirstClick = true;
		}
		if (Input.GetMouseButton(0))
        {
			RotateShish();
			StretchShish();
		
			//TODO	Delete when you visualize shish stretch power.
			slider.GetComponent<Slider>().value = stretchAmount / maxStretch * maxMoveSpeed;
			////////////////////////////////////////////////

		}
		if (Input.GetMouseButtonUp(0))
		{
			lastMousePosition = Input.mousePosition;
			CalculateMoveSpeed();
		}
		if(moveSpeed > 0)
		{
			MoveShish();
		}
	}

	//Initializes base class properties when object created.
    void InitializeBaseProperties()
	{
		base.MaxNumberOfSlots = maxNumberOfSlots;
		base.MaxNumberOfBounces = maxNumberOfBounces;
		base.GravityScale = gravityScale;
		base.MoveSpeed = moveSpeed;
		base.MaxMoveSpeed = maxMoveSpeed;
		base.CanBreakGlass = canBreakGlass;
		base.CanBurn = canBurn;
	}

    private void RotateShish()
    {
		Vector3 mousePos = Input.mousePosition;

        screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

		if (isFirstClick)
		{
			clickPosition = mousePos;
			isFirstClick = false;
		}

		screenPos.x = (screenPos.x > maxScreenPosX ? maxScreenPosX : screenPos.x); 
		screenPos.y = (screenPos.y < minScreenPosY ? minScreenPosY : screenPos.y); 


		rectTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - rectTransform.position.y), (mousePos.x - rectTransform.position.x)) * Mathf.Rad2Deg);
		if(rectTransform.eulerAngles.z > 180)
		{
			rectTransform.eulerAngles = new Vector3 (0, 0, 180);
		}
		else if(rectTransform.eulerAngles.z < 90)
		{
			rectTransform.eulerAngles = new Vector3 (0, 0, 90);
		}

		//Clamp shish's rotation
		Vector3 tmpRotation = rectTransform.eulerAngles;
		Mathf.Clamp(tmpRotation.z, 90, 180);
		rectTransform.eulerAngles = tmpRotation;
    }

	
	///TODO	Delete when you visualize shish stretch power.
	public GameObject slider;
	/////////////////////////
	
	private void StretchShish()
	{
		stretchAmount = Vector3.Distance(clickPosition, rectTransform.position) - Vector3.Distance(Input.mousePosition, rectTransform.position);

		stretchAmount = Mathf.Clamp(stretchAmount, 0, maxStretch);
	}

	private void MoveShish()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed, Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "WallUpDown")
		{
			transform.eulerAngles = new Vector3(180-transform.eulerAngles.x, 180-transform.eulerAngles.y, 180-transform.eulerAngles.z);
		}
		else if(other.gameObject.tag == "WallLeftRight")
		{
			transform.eulerAngles = new Vector3(0, 0, 180-transform.eulerAngles.z);
		}
	}



    //private void TouchControl()
    //{
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
    //}



	private void CalculateMoveSpeed()
	{
		moveSpeed = stretchAmount / maxStretch * maxMoveSpeed;
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
