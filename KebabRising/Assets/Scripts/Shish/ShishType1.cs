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
		Debug.Log(canBurn+" "+base.CanBurn);
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
		
		//burn related things will go there(animation, sound, etc.)
	}

	private void DestroyShish()
	{
		Destroy(gameObject, 0f);

		//shish destruction related things will go there(animation, sound, etc.)
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(canBurn && other.collider.CompareTag("Fire"))
		{
			Burn();
		}
	}
}
