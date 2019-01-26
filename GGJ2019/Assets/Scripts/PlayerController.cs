using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 _offset;

	private Vector2 _originalPos;

	private Vector2 _currentPosition;

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
		temp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		Debug.Log(Input.mousePosition.x);
//		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint
//		          (
//			          new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
//			          Debug.Log("Mouse X: ", Input.mousePosition.x);
//		          );
		Cursor.visible = false;
	}

	private void OnMouseDrag()
	{
		var offset = temp - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Debug.Log(offset);
//		var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
//		_currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + _offset;
//
//		transform.position = _currentPosition;
	}

	private void OnMouseUp()
	{
		Cursor.visible = true;

//		if (OnDragHandleReleaseEvent != null)
//		{
//			OnDragHandleReleaseEvent.Invoke();
//		}

		transform.position = _defaulPos;
	}
}
