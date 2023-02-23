using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectionArrow : MonoBehaviour
{
    private MainPlayer mainPlayer;
    private SimpleMouseControl simpleMouseControl;
    private GameObject mainPlayerTop;
    private Vector2 pointTouched;
    private Vector2 pointDirection;
    private Vector2 basicDirection;
    Vector2 startPos;

    private void Start()
    {
        mainPlayer = FindObjectOfType<MainPlayer>();
        simpleMouseControl = new SimpleMouseControl();
        simpleMouseControl.Enable();
        mainPlayerTop = GameObject.FindGameObjectWithTag("Bumper");
        basicDirection = Vector2.up;
        
    }
    void Update()
    {        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            pointTouched = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            pointDirection = pointTouched - startPos;       
                     
            ChangeArrowRot();
            ChangeArrowPos();
        }       
    }

    public void ChangeArrowPos() 
    {
        if (mainPlayer.PlayerController.GetIsTouched() != false)
        {
 
            Vector2 dist = simpleMouseControl.Game.Position.ReadValue<Vector2>() - mainPlayer.PlayerController.GetStartPos();
            Vector3 mainPlayerTopPos = Camera.main.WorldToScreenPoint(mainPlayerTop.transform.position);
            mainPlayerTopPos.z = 0;
            transform.position = new Vector2(mainPlayerTopPos.x, mainPlayerTopPos.y) + dist;
            
        }

    }

    public void ChangeArrowRot()
    {
        if (mainPlayer.PlayerController.GetIsTouched() != false)
        {
            float newAngleToRotate = Vector2.SignedAngle(basicDirection, pointDirection);
            transform.rotation = Quaternion.Euler(0, 0, newAngleToRotate);

        }
    }
}
