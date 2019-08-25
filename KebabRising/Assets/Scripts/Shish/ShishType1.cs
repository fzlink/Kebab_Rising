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


	private float rayDistance;
	void Start()
	{
		InitializeBaseProperties();
		PlaceShish();
		remainingBounces = maxNumberOfBounces;

	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawLine(transform.position, transform.position + transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f));
		Gizmos.DrawLine(transform.position + transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f) , (transform.position - transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f)));
	}
	Vector3 newDirection;
	void Update()
	{
		if(remainingBounces == -1)
		{
			DestroyShish();
		}
		if (Input.GetMouseButtonDown(0))
		{
			isFirstClick = true;
		}
		if (Input.GetMouseButton(0))
        {
			RotateShish();
			StretchShish();
			//TODO	Delete when you visualize shish stretch power.
			slider.GetComponent<Slider>().value = stretchAmount;
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
    private void InitializeBaseProperties()
	{
		base.MaxNumberOfSlots = maxNumberOfSlots;
		base.MaxNumberOfBounces = maxNumberOfBounces;
		base.GravityScale = gravityScale;
		base.MoveSpeed = moveSpeed;
		base.MaxMoveSpeed = maxMoveSpeed;
		base.CanBreakGlass = canBreakGlass;
		base.CanBurn = canBurn;
	}

	//to position the shish correctly for different screen sizes/resolutions.
	private void PlaceShish()
	{
		//get 2d screen size
		Rect viewportRect = Camera.main.pixelRect; 

		//pick a point at bottom right corner, you might need to adjust z-axis part if you change camera's position on Z
		Vector3 newPos = new Vector3(viewportRect.xMax, viewportRect.yMin, Mathf.Abs(Camera.main.transform.position.z));		
		newPos = Camera.main.ScreenToWorldPoint(newPos);

		//leave room for the size of our shish object based on sprite size in world units
		//add small offset to prevent collision between walls and shish.
		newPos.x -= GetComponent<SpriteRenderer>().bounds.extents.x + 0.1f;
		newPos.y += GetComponent<SpriteRenderer>().bounds.extents.y + 0.1f;
		transform.position = newPos;
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
	
		transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);

		//Clamp shish's rotation
		Vector3 tmpRotation = transform.eulerAngles;
		tmpRotation.z = Mathf.Clamp(tmpRotation.z, 90, 180);
		transform.eulerAngles = tmpRotation;
    }

	//TODO	Delete when you visualize shish stretch power.
	public GameObject slider;
	/////////////////////////
	
	private void StretchShish()
	{
		Vector3 posInScreen = Camera.main.WorldToScreenPoint(transform.position);
		stretchAmount = Vector3.Distance(clickPosition, posInScreen) - Vector3.Distance(Input.mousePosition, posInScreen);
		stretchAmount = Mathf.Clamp(stretchAmount, 0, maxStretch);
	}
	
	private void CalculateMoveSpeed()
	{
		moveSpeed = stretchAmount / maxStretch * maxMoveSpeed;
	}

	private void MoveShish()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed, Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "WallUpDown")
		{
	//		transform.eulerAngles = new Vector3(180-transform.eulerAngles.x, 180-transform.eulerAngles.y, 180-transform.eulerAngles.z);
			float xOffset = (transform.position + transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f) - (transform.position - transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f))).x;
			xOffset = Mathf.Abs(xOffset);
			xOffset *= Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x);
			//transform.position = new Vector2(transform.position.x + xOffset, transform.position.y);
			Vector3 eulerAngles = transform.eulerAngles;
			eulerAngles.z += 2 * (180 - eulerAngles.z);
			transform.eulerAngles = eulerAngles;
			--remainingBounces;
		}
		else if(other.gameObject.tag == "WallLeftRight")
		{
			float yOffset = (transform.position + transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f) - (transform.position - transform.right * (GetComponent<PolygonCollider2D>().bounds.extents.magnitude - 0.05f))).y;
			//transform.position = new Vector2(transform.position.x, transform.position.y + yOffset);
			Vector3 eulerAngles = transform.eulerAngles;
			eulerAngles.z = 180 - eulerAngles.z;
			transform.eulerAngles = eulerAngles;
			--remainingBounces;
		}
	}

	 void OnCollisionEnter2D(Collision2D collision) {
        foreach (ContactPoint2D contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
			//transform.RotateAround(collision.GetContact(0).point, -Vector3.forward, -180 - 2*(180-transform.eulerAngles.z));
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

	//void OnCollisionEnter2D(Collision2D other)
	//{
	//	if(canBurn && other.collider.CompareTag("Fire"))
	//	{
	//		Burn();
	//	}
	//}
}
