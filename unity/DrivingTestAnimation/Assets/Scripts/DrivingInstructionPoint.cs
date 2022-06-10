using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrivingInstructionPoint : MonoBehaviour
{
    [SerializeField]
    private Direction directionPoint;
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite spriteRight;
    [SerializeField]
    private Sprite spriteLeft;
    [SerializeField]
    private Sprite spriteContinue;

    [SerializeField]
    private Angle angle;


    // Start is called before the first frame update
    void Start()
    {
        Transform child = this.transform.GetChild(0);

        if(this.angle == Angle.Down)
        {
            child.eulerAngles = new Vector3(child.eulerAngles.x, child.eulerAngles.y, child.eulerAngles.z + 180f);
        } else if(this.angle == Angle.Right)
        {
            child.eulerAngles = new Vector3(child.eulerAngles.x, child.eulerAngles.y, child.eulerAngles.z - 90f);
        } else if(this.angle == Angle.Left)
        {
            child.eulerAngles = new Vector3(child.eulerAngles.x, child.eulerAngles.y, child.eulerAngles.z + 90f);
        }

        spriteRenderer = child.GetComponent<SpriteRenderer>();
        spriteRight = Resources.Load<Sprite>("ArrowTraffic/arrow_right");
        spriteLeft = Resources.Load<Sprite>("ArrowTraffic/arrow_left");
        spriteContinue = Resources.Load<Sprite>("ArrowTraffic/arrow_forward");

        if(directionPoint == Direction.Right)
        {
            spriteRenderer.sprite = spriteRight;
        } else if(directionPoint == Direction.Left)
        {
            spriteRenderer.sprite = spriteLeft;
        } else if(directionPoint == Direction.Continue)
        {
            spriteRenderer.sprite = spriteContinue;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(directionPoint != Direction.None)
        {

        }*/
    }

    void OnTriggerEnter()
    {
        if(directionPoint != Direction.None)
        {
            GiveDirection();
            DirectionUIManager.Instance.UpdateDirection(directionPoint);
        }
    }

    void GiveDirection()
    {
        // Debug.Log(direction);
    }

    public enum Direction {
        None,
        Right,
        Left,
        Continue
    }

    public enum Angle {
        Up,
        Down,
        Right,
        Left
    }


    public Direction GetDirection => directionPoint;



}
