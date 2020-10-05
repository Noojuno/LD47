using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraController : Singleton<CameraController>
{
    public Transform target;
    public float cameraFollowSpeed = 0.3f;
    public float mouseOffsetMultiplier = 1f;
    public Vector3 offset;
    public Texture2D cursor;

    public Vector3 mouseOffset = Vector2.zero;

    private Vector3 _offset;
    private Vector3 _oldOffset;

    public bool mouseOffsetEnabled = true;

    void Update()
    {
        //Cursor.SetCursor(this.cursor, Vector2.zero, CursorMode.Auto);

        this.mouseOffset = this.mouseOffsetEnabled ? this.CalculateMouseOffset() : Vector3.zero;

        if (this.target != null)
        {
            _oldOffset = _offset;
            _offset = Vector3.Lerp(_oldOffset, this.mouseOffset, cameraFollowSpeed);

            this.transform.position = this.target.position + this.offset + _offset;
        }
    }

    Vector3 CalculateMouseOffset()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition); //raw mouse pos
        ret *= 2;
        ret -= Vector2.one; //set (0,0) of mouse to middle of screen
        /* float max = 0.9f;

        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized; //helps smooth near edges of screen
        } */

        return ret * this.mouseOffsetMultiplier;
    }
}
