using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShishBase : MonoBehaviour
{
	//Make them public if you need.

	protected int MaxNumberOfSlots { get; set; }
	//protected Slot[] Slots { get; set; }
	protected int MaxNumberOfBounces { get; set; }
	protected float GravityScale { get; set; } 
	protected float MoveSpeed { get; set; }
	protected float MaxMoveSpeed { get; set; }
	protected bool CanBreakGlass { get; set; }
	protected bool CanBurn { get; set; }
}
