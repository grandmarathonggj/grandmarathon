using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private CircleRenderer circleIndicator;
	private Vector3 _originalPos;
	private Vector3 _mouseStartPos;
	private Vector3 _direction;
	private float _dragDistance;
	private Vector3 _dragVector3;
	
	private float _rotationSpeed = 10.0f;
 
	private Quaternion _lookRotation;
	
	private void Start()
	{
		_originalPos = this.transform.position;
		circleIndicator = transform.GetComponentInChildren<CircleRenderer>();
	}
     
	// Update is called once per frame
	void Update()
	{
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
 
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
	}
	
	void OnMouseDown()
	{
		_dragDistance = 0.0f;
		_direction = new Vector3();
		_mouseStartPos = Input.mousePosition;
		circleIndicator.Render( 1.0f );
		Cursor.visible = false;
	}

	private void OnMouseDrag()
	{
		_dragDistance = Vector3.Distance(Input.mousePosition, _mouseStartPos);
		_dragDistance = Mathf.Clamp(_dragDistance, 0f, 100f);
		
		_dragVector3 = Input.mousePosition - _mouseStartPos;
		_dragVector3 = clampVector3(_dragVector3);
		
		_direction = invertVector3Direction(swapYZ(_dragVector3)).normalized;
		
		circleIndicator.Render(1 + _dragDistance / 100f);
	}

	private void OnMouseUp()
	{
//		GetComponent<CustomPhysics>().Push(_direction, _dragDistance);
		Cursor.visible = true;
	}

	private Vector3 swapYZ(Vector3 input)
	{
		Vector3 result = new Vector3(input.x, 0.5f, input.y);
		return result;
	}
	
	private Vector3 invertVector3Direction(Vector3 input)
	{
		Vector3 result = new Vector3(-input.x, -input.y, -input.z);
		return result;
	}
	
	private Vector3 clampVector3(Vector3 input, float min = -100f, float max = 100f)
	{
		Vector3 result;
		result.x = Mathf.Clamp(input.x, min, max);
		result.y = Mathf.Clamp(input.y, min, max);
		result.z = Mathf.Clamp(input.z, min, max);
		return result;
	}
}
