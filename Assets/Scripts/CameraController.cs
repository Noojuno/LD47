using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraFollowSpeed = 0.3f;
    public float mouseOffsetMultiplier = 1f;
    public Vector3 offset;
    public Texture2D cursor;

    public Vector3 mouseOffset = Vector2.zero;

    void Update()
    {
        Cursor.SetCursor(this.cursor, Vector2.zero, CursorMode.Auto);

        this.mouseOffset = this.CalculateMouseOffset();
    }

    Vector3 CalculateMouseOffset()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition); //raw mouse pos
        ret *= 2;
        ret -= Vector2.one; //set (0,0) of mouse to middle of screen
        float max = 0.9f;

        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized; //helps smooth near edges of screen
        }

        return ret * this.mouseOffsetMultiplier;
    }

    void FixedUpdate()
    {
        if (this.target != null)
        {
            Vector3 targetPosition = target.position + offset;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition + this.offset + this.mouseOffset, cameraFollowSpeed);
        }
    }
}
