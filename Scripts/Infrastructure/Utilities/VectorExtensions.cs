using UnityEngine;

namespace Utilities
{
  public static class VectorExtensions
  {
    public static void SetX(this ref Vector3 vector, float x)
    {
      vector = new Vector3(x, vector.y, vector.z);
    }

    public static void SetY(this ref Vector3 vector, float y)
    {
      vector = new Vector3(vector.x, y, vector.z);
    }

    public static void SetZ(this ref Vector3 vector, float z)
    {
      vector = new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 AddAngle(this Vector3 vector3, float angle)
    {
      float randomHorizontalAngle = Random.Range(-angle, angle);
      float randomVerticalAngle = Random.Range(-angle, angle);

      Quaternion horizontalRotation = Quaternion.AngleAxis(randomHorizontalAngle, Vector3.up);
      Quaternion verticalRotation = Quaternion.AngleAxis(randomVerticalAngle, Vector3.right);

      Quaternion rotation = horizontalRotation * verticalRotation;

      vector3 = rotation * vector3;

      return vector3.normalized;
    }
  }
}