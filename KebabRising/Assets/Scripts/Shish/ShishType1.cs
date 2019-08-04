using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShishType1 : ShishBase
{

	[SerializeField] [Range(0,10)] private new int maxNumberOfSlots = 0;
	[SerializeField] [Range(0,100)] private new int maxNumberOfBounces = 0;
	[SerializeField] private new Vector2 moveSpeed;
	[SerializeField] private new bool canBreakGlass;
	[SerializeField] private new bool canBurn;


	private void CalculateMoveSpeed()
	{
		//moveSpeed = Vector2(germe.x, germe.y - (if exist) gravityFactor);
	}

	/*
	void OnCollisionEnter2D(Collider2D other)
	{
		if(base.canBreakGlass && other.CompareTag("Glass"))
		{
			other.gameObject.GetComponent<Script That Contains Code Block To Break The Glass>().Break();
		}
		if(base.canBurn && other.CompareTag("Fire"))
		{
			GetComponent<Script That Contains Code Block To Burn>().Burn();
			OR
			Burn();
		}
	}
	*/
}
