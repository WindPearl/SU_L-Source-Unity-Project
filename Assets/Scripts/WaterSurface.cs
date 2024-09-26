﻿using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    public MeshCollider surface;
    MeshCollider meshCollider;
    public GameObject waterSplash;

    float centerY;

    // Use this for initialization
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.playerTag))
        {
            Instantiate(waterSplash, new Vector3(other.transform.position.x, transform.position.y + 0.1f, other.transform.position.z), Quaternion.identity);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameTags.playerTag))
        {
            //Vector3 otherVelocity = other.GetComponent<Rigidbody>().velocity;

            //Vector3 otherHorizontalVelocity = new Vector3(otherVelocity.x, 0, otherVelocity.z);

            ////surface.transform.localPosition = Vector3.Lerp( new Vector3(0, -5, 0), Vector3.zero, otherHorizontalVelocity.magnitude / 60);

            //if (otherHorizontalVelocity.magnitude < 42)
            //{
            //    surface.isTrigger = true;
            //}
            //else
            //{
            //    surface.isTrigger = false;
            //}
        }
        void StateDriftStart()
        {
            void StateDrift()
            {
                Instantiate(waterSplash, new Vector3(other.transform.position.x, transform.position.y + 0.1f, other.transform.position.z), Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameTags.playerTag))
        {
            other.transform.parent = null;

            surface.isTrigger = true;



            Instantiate(waterSplash, other.transform.position, Quaternion.identity);
        }


    }
}
