using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : IController
{
	public IController.OnMove onMove { get; set; }
	public IController.OnTouch onTouch { get; set; }
	public IController.OnSwipe onSwipe { get; set; }
	public IController.OnTouchPos onTouchPos { get; set; }

	private Vector2 startpos;
	private bool isTuched = false;
	private Vector2 total; // test
	private SimpleMouseControl simpleMouseControl;

	public void Init()
	{
		simpleMouseControl = new SimpleMouseControl();
		simpleMouseControl.Enable();
		EnableControll();
	}

	private void StartDrag(InputAction.CallbackContext contex)
	{
		startpos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		isTuched = true;
		onTouch?.Invoke(true);
		onTouchPos?.Invoke(startpos);
	}

	private void BeingDrag(InputAction.CallbackContext contex)
	{
		if (!isTuched) return;
		Vector2 currentPos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		Vector2 total = (currentPos - startpos);
		if (total.magnitude > ConstantVariables.MinSwipeLenghtForChangeDirection)
		{
			onMove?.Invoke(total);
		}
	}

	private void EndDrag(InputAction.CallbackContext contex) 
	{
		isTuched = false;
		onTouch?.Invoke(false);
		Vector2 currentPos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		total = (currentPos - startpos);
		if (total.magnitude >= ConstantVariables.MinSwipeLenghtForChangeDirection)
		{
			onSwipe?.Invoke(total);
		}
	}

	public void DisableControll()
	{
		simpleMouseControl.Game.Down.performed -= StartDrag;
		simpleMouseControl.Game.Position.performed -= BeingDrag;
		simpleMouseControl.Game.Down.canceled -= EndDrag;
	}

	public void EnableControll()
	{
		simpleMouseControl.Game.Down.performed += StartDrag;
		simpleMouseControl.Game.Position.performed += BeingDrag;
		simpleMouseControl.Game.Down.canceled += EndDrag;
	}

	public bool GetIsTouched()
    {
		return isTuched;
    }

	public Vector2 GetStartPos()
    {
		return startpos;
    }
}
