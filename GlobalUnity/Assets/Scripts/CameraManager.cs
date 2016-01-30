using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public float maxZoom = 1f;
    private float maxSpeed = 9f;
    public GameObject Earth;
    public Transform EarthOrigin;


    private string earthTag = "Earth";
    private string earthOriginTag = "EarthOrigin";


	// Use this for initialization
	void Start () {
        this.EarthOrigin = GameObject.FindGameObjectWithTag(this.earthOriginTag).transform;
        this.Earth = GameObject.FindGameObjectWithTag(this.earthTag);
        transform.LookAt(this.EarthOrigin);
	}
	
	// Update is called once per frame
	void Update () {
        DoZoom();
	}

    void DoZoom()
    {
        var zoom = Input.GetAxis("Mouse ScrollWheel");
        RaycastHit ray;
        var direction = this.EarthOrigin.position - gameObject.transform.position;
        if(Physics.Raycast(gameObject.transform.position, direction, out ray))
        {
            Debug.Log(ray.distance);
        }
    }
}
