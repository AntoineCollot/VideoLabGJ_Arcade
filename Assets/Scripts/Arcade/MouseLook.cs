using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    [SerializeField] enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    [SerializeField] RotationAxes axes = RotationAxes.MouseXAndY;
    [SerializeField] float sensitivityX = 15F;
    [SerializeField] float sensitivityY = 15F;
    [SerializeField] float minimumX = -360F;
    [SerializeField] float maximumX = 360F;
    [SerializeField] float minimumY = -60F;
    [SerializeField] float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotationX;
    Quaternion originalRotationY;

    public Transform xTarget;
    public Transform yTarget;
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        if (axes == RotationAxes.MouseX)
        {
            rotationY = 0;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX = 0;
        }

        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        xTarget.localRotation = originalRotationX * xQuaternion;
        yTarget.localRotation = originalRotationY * yQuaternion;
    }
    void OnEnable()
    {
        if (xTarget == null)
            xTarget = transform;
        if (yTarget == null)
            yTarget = transform;

        originalRotationX = xTarget.localRotation;
        originalRotationY = yTarget.localRotation;
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);

    }
}