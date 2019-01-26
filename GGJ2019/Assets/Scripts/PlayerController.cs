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
	private float _normalizedDrag;
	private Vector3 _dragVector3;

    private GrandmaController grandmaHerself;
	private Transform _arrowContainer;
	private float _rotationSpeed = 10.0f;
 
	private Quaternion _lookRotation;

	private float yRotation;

	private void Start()
	{
		yRotation = Camera.main.transform.eulerAngles.y;
		_originalPos = this.transform.position;
		circleIndicator = transform.GetComponentInChildren<CircleRenderer>();
        grandmaHerself = transform.GetComponentInChildren<GrandmaController>();
		_arrowContainer = transform.Find("ArrowContainer");
		_arrowContainer.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
		_arrowContainer.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
	}
     
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_dragDistance = 0.0f;
			_direction = new Vector3();
			_mouseStartPos = Input.mousePosition;
			_normalizedDrag = 1.0f;
			circleIndicator.Render( _normalizedDrag );
			_arrowContainer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			Cursor.visible = false;
		} else if (Input.GetMouseButton(0))
		{
			_dragDistance = Vector3.Distance(Input.mousePosition, _mouseStartPos);
			_dragDistance = Mathf.Clamp(_dragDistance, 0f, 100f);
	
			_dragVector3 = Input.mousePosition - _mouseStartPos;
			_dragVector3 = clampVector3(_dragVector3);
			
			_direction = Quaternion.Euler(Vector3.up * yRotation) * invertVector3Direction(swapYZ(_dragVector3)).normalized;
			
			_normalizedDrag = Mathf.Lerp( _normalizedDrag, 1 + _dragDistance / 100f, 0.1f);
			circleIndicator.Render( _normalizedDrag );
			
			_arrowContainer.transform.localScale = new Vector3(1.0f, 1.0f, _normalizedDrag);
			_arrowContainer.transform.localEulerAngles = new Vector3((1.0f - _normalizedDrag) * 30, 0.0f, 0.0f);
			
			Quaternion rotation = Quaternion.LookRotation(_direction, Vector3.up);
			transform.rotation = rotation;
            grandmaHerself.chargeAmount = _dragDistance / 100f;

        } else if(Input.GetMouseButtonUp(0)){
			_arrowContainer.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
			_arrowContainer.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            GetComponent<CustomPhysics>().Push(_direction, _dragDistance / 100f);
        }else {
            grandmaHerself.chargeAmount = 0;
			Cursor.visible = true;
			circleIndicator.Render(0.0f);
		}
	}
	
	// Helper functions
	private Vector3 swapYZ(Vector3 input)
	{
		Vector3 result = new Vector3(input.x, 0.0f, input.y);
		return result;
	}
	
	private Vector3 invertVector3Direction(Vector3 input)
	{
		Vector3 result = new Vector3(-input.x, 0.0f, -input.z);
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
