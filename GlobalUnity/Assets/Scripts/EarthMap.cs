using UnityEngine;
using System.Collections;

public class EarthMap : MonoBehaviour
{

    public string earthOriginTagName = "EarthOrigin";
    public GameObject equator;

    private Vector3 EarthOrigin;
    private float outsideDistance = 20000;
    private Vector3 northPole;
    private Vector3 southPole;

    // Use this for initialization
    void Start()
    {
        // resize vertical scale of earth.
        MeshCollider coll = GetComponent<MeshCollider>();
        coll.sharedMesh = GetComponent<MeshFilter>().mesh;

        this.EarthOrigin = GameObject.FindGameObjectWithTag(this.earthOriginTagName).transform.position;
        RaycastHit northRay, southRay;
        Vector3 northPoint = EarthOrigin + (Vector3.up * outsideDistance);
        Vector3 southPoint = EarthOrigin + (Vector3.down * outsideDistance);
        Physics.Raycast(northPoint, Vector3.down, out northRay);
        Physics.Raycast(southPoint, Vector3.up, out southRay);
        this.northPole = northRay.point;
        this.southPole = southRay.point;

        // create north and south pole
        var n = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        n.name = "North Pole";
        n.transform.parent = gameObject.transform;
        n.transform.position = this.northPole;
        var s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        s.name = "South Pole";
        s.transform.parent = gameObject.transform;
        s.transform.position = this.southPole;

        RaycastHit eqRay;
        
        // create equator
        for (int i = 0; i < 360; i++)
        {
            Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
            Vector3 angle = q * Vector3.forward;
            Vector3 eastPoint = EarthOrigin + (angle * outsideDistance);
            Physics.Raycast(eastPoint, Vector3.Scale(angle, new Vector3(-1, -1, -1)), out eqRay);

            var x = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            x.transform.position = eqRay.point;
            x.name = i + ",90";
            x.transform.parent = this.equator.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
