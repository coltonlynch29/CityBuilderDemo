using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector instance;

    void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        cam = Camera.main;
    }

    // returns the tile that the mouse is hovering over
    public Vector3 GetCurTilePosition ()
    {
        // return if we're hovering over UI
        if(EventSystem.current.IsPointerOverGameObject())
            return new Vector3(0, -99, 0);

        // create the plane, ray and out distance
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0.0f;

        // shoot the ray at the plane
        if(plane.Raycast(ray, out rayOut))
        {
            // get the position at which we intersected the plane
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);

            // round that up to the nearest full number (nearest meter)
            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0.0f, Mathf.CeilToInt(newPos.z));

            return newPos;
        }

        return new Vector3(0, -99, 0);
    }
}