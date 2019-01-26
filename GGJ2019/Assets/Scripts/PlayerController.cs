using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	private Vector3 _originalPos;
	private Vector3 _mouseDownPos;
	private Vector3 _dragDistance;
	private Vector3 _targetPos;
	private float _distance;
	private float _eventStartTime;

	//public event OnDragHandleReleaseDelegate OnDragHandleReleaseEvent;

	private void Start()
	{
		_originalPos = transform.position;
		_targetPos = _originalPos;
	}

	private void Update()
	{

		bool moveDone = transform.position == _targetPos;
		float travelledDistance = (Time.time - _eventStartTime) * speed;
		float fracJourney = travelledDistance / _distance;
		Debug.Log(fracJourney);
		if (fracJourney <= 1.0f)
		{
			transform.position = Vector3.Lerp(_originalPos, _targetPos, fracJourney);
		}
		else
		{
			_originalPos = transform.position;
		}
	}

	void OnMouseDown()
	{
		_mouseDownPos = Input.mousePosition;
		Cursor.visible = false;
	}

	private void OnMouseDrag()
	{
		Vector3 offset = Input.mousePosition - _mouseDownPos;
		if (offset.x >= 5.0f) offset.x = 5.0f;
		if (offset.x <= -5.0f) offset.x = -5.0f;
		if (offset.y >= 5.0f) offset.y = 5.0f;
		if (offset.y <= -5.0f) offset.y = -5.0f;
		_dragDistance = offset;

	}

	private void OnMouseUp()
	{
		_targetPos = invertMouseToObjectDirection(swapYZ((_originalPos + _dragDistance)));
		_distance = Vector3.Distance(_targetPos, _originalPos);
		_eventStartTime = Time.time;
		//Debug.Log(_targetPos);
		Cursor.visible = true;
	}

	private Vector3 swapYZ(Vector3 input)
	{
		Vector3 result = new Vector3(input.x, 0.5f, input.y);
		return result;
	}
	
	private Vector3 invertMouseToObjectDirection(Vector3 input)
	{
		Vector3 result = new Vector3(-input.x, 0.5f, -input.z);
		return result;
	}
}
