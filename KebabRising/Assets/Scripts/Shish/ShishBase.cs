using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShishBase : MonoBehaviour
{
	protected int maxNumberOfSlots { get; set; }
	//protected Slot[] Slots { get; set; }
	protected int maxNumberOfBounces { get; set; }
	protected Vector2 moveSpeed { get; set; }
	protected bool canBreakGlass { get; set; }
	protected bool canBurn { get; set; }
}
