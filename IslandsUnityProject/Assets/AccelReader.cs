using UnityEngine;
using System.Collections;

public class AccelReader {

    public static float sensH = 8;
    public static float sensV = 8;
    public static float smooth = 0.1f;
    public static float deadzone = 0.2f;

    static float GetAxisH = 0;
    static float GetAxisV = 0;

    static Vector3 zeroAc;
    static Vector3 curAc;

    public static void SetDefault()
    {
        zeroAc = Input.acceleration;
        curAc = Vector3.zero;
    }

    public static void Reload()
    {
        sensH = PlayerPrefs.GetFloat(Strings.axisSensX);
        sensV = PlayerPrefs.GetFloat(Strings.axisSensY);
        deadzone = PlayerPrefs.GetFloat(Strings.axisDeadzone);
    }

    public static void Set(float a, float b, float c)
    {
        sensH = a;
        sensV = b;
        deadzone = c;
    }

    public static Vector2 GetAccelMovement()
    {

        curAc = Vector3.Lerp(curAc, Input.acceleration - zeroAc, Time.deltaTime / smooth);

        GetAxisV = Mathf.Clamp(curAc.y * sensV, -1, 1);
        GetAxisH = Mathf.Clamp(curAc.x * sensH, -1, 1);

        GetAxisH = Mathf.Abs(GetAxisH) < deadzone ? 0 : GetAxisH * (1 + deadzone) - Mathf.Sign(GetAxisH) * deadzone;
        GetAxisV = Mathf.Abs(GetAxisV) < deadzone ? 0 : GetAxisV * (1 + deadzone) - Mathf.Sign(GetAxisV) * deadzone;

        return new Vector2(GetAxisH, GetAxisV);
    }

}
