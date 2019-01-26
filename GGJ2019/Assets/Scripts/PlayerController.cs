using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 _offset;

	private Vector3 _originalPos;
	
	private Vector3 _mouseDownPos;

	private Vector3 _mouseOffset;
	
	private Vector3 _currentPosition;

	private Vector3 temp;

	//public event OnDragHandleReleaseDelegate OnDragHandleReleaseEvent;

	private void Start()
	{
		_originalPos = this.transform.position;
	}

//	private void Update()
//	{
//		transform.position = _defaulPos;
//		throw new System.NotImplementedException();
//	}

	void OnMouseDown()
	{
		_mouseDownPos = Input.mousePosition;
//		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint
//		          (
//			          new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
//			          Debug.Log("Mouse X: ", Input.mousePosition.x);
//		          );
//		Cursor.visible = false;
	}

	private void OnMouseDrag()
	{
		_mouseOffset = Input.mousePosition - _mouseDownPos;
//		Debug.Log(_mouseOffset);
//		var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
//		_currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + _offset;
//
//		transform.position = _currentPosition;
	}

	private void OnMouseUp()
	{
		this.transform.Translate(swapYZ((_originalPos + _mouseOffset) * 0.1f));
		_originalPos = this.transform.position;
//		Cursor.visible = true;

//		if (OnDragHandleReleaseEvent != null)
//		{
//			OnDragHandleReleaseEvent.Invoke();
//		}
//
//		transform.position = _defaulPos;
	}

	private Vector3 swapYZ(Vector3 input)
	{
		Vector3 result = new Vector3(input.x, 0.0f, input.y);
		return result;
	}
}
