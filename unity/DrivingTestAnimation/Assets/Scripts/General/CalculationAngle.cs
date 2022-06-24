using UnityEngine;

public static class CalculationAngle
{
    /*
    * calculating the angle between positin of other collider this the GameObgect
    */ 
    public static float calculateAngle(Collider other, Transform tr)
    {
        Vector3 closestPoint = other.ClosestPoint(other.gameObject.transform.position);
        Vector3 targetDir = closestPoint - tr.transform.position;
        float angle = Vector3.Angle(targetDir, tr.forward);

        if(closestPoint.x > tr.position.x)
                    angle = 360 - angle;

        return angle;
    }

    /*
    * checking if the 'angle' is between the angle 'begin' and 'end'
    */
    public static bool isActiveAngle(float angle, float begin, float end)
    {
        // first option: if begin angle smaller then end angle. second option: if the begin angle greater then end angle.
        if((begin <= end) && (begin <= angle && angle <= end))
        {
            return true;
        } else if ((begin > end) && ((begin <= angle && angle <= 360) || (0 <= angle && angle <= end)))
        {
            return true;
        }
        return false;
    }
}