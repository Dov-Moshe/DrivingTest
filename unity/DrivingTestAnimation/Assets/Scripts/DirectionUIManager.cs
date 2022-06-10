using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionUIManager : MonoBehaviour
{
    public static DirectionUIManager Instance;
    // Singleton pattern
    void Awake() {
        Instance = this;
    }

    [SerializeField]
    private GameObject rightImage;
    [SerializeField]
    private GameObject leftImage;
    [SerializeField]
    private GameObject continueImage;



    // curent object or direction (static)
    private DrivingInstructionPoint.Direction currentDirection;


    public void UpdateDirection(DrivingInstructionPoint.Direction d) {
        switch (d)
            {
                case DrivingInstructionPoint.Direction.Right:
                    leftImage.SetActive(false);
                    continueImage.SetActive(false);
                    rightImage.SetActive(true);
                    break;
                case DrivingInstructionPoint.Direction.Left:
                    rightImage.SetActive(false);
                    continueImage.SetActive(false);
                    leftImage.SetActive(true);
                    break;
                case DrivingInstructionPoint.Direction.Continue:
                    rightImage.SetActive(false);
                    leftImage.SetActive(false);
                    continueImage.SetActive(true);
                    break;
                default:
                    break;
            }
            
           
    }

}
