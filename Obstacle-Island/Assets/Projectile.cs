using UnityEngine;

public class Projectile
{
    public const float GravityAcceleration = 9.8f;
    
    public static Vector3 GetProjectileVelocity(float initialVelocity, float distance, Vector3 up, Vector3 forward)
    {
        float g = Vector3.Magnitude(Physics.gravity);
        float input = (g * distance) / (initialVelocity * initialVelocity);
        if (input > 1)
            input = 1;
        if (input < -1)
            input = -1;
        float theta = 0.5f * Mathf.Asin(input);

        Vector3 Vx = (initialVelocity * Mathf.Cos(theta)) * forward;
        Vector3 Vy = (initialVelocity * Mathf.Sin(theta)) * up;

        return Vx + Vy;
    }

}
