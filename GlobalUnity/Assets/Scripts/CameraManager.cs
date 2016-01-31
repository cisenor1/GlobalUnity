using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public float maxZoom = 1f;
    public float minZoom = 15000f;
    public float zoomSpeed = 0.02f;
    public GameObject Earth;
    public Transform EarthOrigin;


    private string earthTag = "Earth";
    private string earthOriginTag = "EarthOrigin";
    private float zoomLevel = 0f;

    // Use this for initialization
    void Start()
    {
        this.EarthOrigin = GameObject.FindGameObjectWithTag(this.earthOriginTag).transform;
        this.Earth = GameObject.FindGameObjectWithTag(this.earthTag);
        transform.LookAt(this.EarthOrigin);
    }

    // Update is called once per frame
    void Update()
    { 
        DoZoom();
    }

    void DoZoom()
    {

        var zoom = Input.GetAxis("Mouse ScrollWheel") * this.zoomSpeed;
        RaycastHit ray;
        var direction = this.EarthOrigin.position - gameObject.transform.position;
        if (Physics.Raycast(gameObject.transform.position, direction, out ray))
        {
            var away = gameObject.transform.position - ray.point;
            Vector3 farAwayDirection = away.normalized;

            Vector3 farAwayPoint = ray.point + farAwayDirection * this.minZoom;
            Vector3 targetPoint = ray.point + farAwayDirection * this.maxZoom;
            var distance = ray.distance;
            
            this.zoomLevel += zoom;
            if (this.zoomLevel > 1)
            {
                this.zoomLevel = 1;
            }else if (this.zoomLevel < 0)
            {
                this.zoomLevel = 0;
            }   
            if (this.zoomLevel >= 0.0 &&  this.zoomLevel <= 1.0)
            {
                gameObject.transform.position = Vector3.Slerp(farAwayPoint, targetPoint, this.zoomLevel);

            }

        }

    }
}
