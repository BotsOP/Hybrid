using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingStarManager : MonoBehaviour
{
    [SerializeField] private GameObject fallingStar;
    [SerializeField] private float speed = 1;
    [SerializeField] private float spawnStarEverySeconds = 1;
    private float time;
    
    [SerializeField] private Mesh plane;
    [SerializeField] private float planeX;
    [SerializeField] private float planeZ;
    private List<Star> stars;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireMesh(plane, 0, transform.position, transform.rotation, new Vector3(transform.lossyScale.x * 100, transform.lossyScale.z * 100));
    }

    void Start()
    {
        stars = new List<Star>();
        
        SpawnStar();
    }

    void Update()
    {
        if (Time.time - time > spawnStarEverySeconds)
        {
            time = Time.time;
            SpawnStar();
        }
        foreach (var star in stars)
        {
            star.starObject.transform.position += star.velocity;
        }
    }

    private void SpawnStar()
    {
        float posX = Random.Range(-1.0f, 1.0f);
        float posZ = Random.Range(-1.0f, 1.0f);

        Vector3 velocity = new Vector3(planeX * posX, -1, planeZ * posZ);

        Vector3 position = transform.position;
        posX = planeX * posX + position.x;
        posZ = planeZ * posZ  + position.z;
        float posY = position.y;

        GameObject tempStar = Instantiate(fallingStar, new Vector3(posX, posY, posZ), Quaternion.identity);
        
        Vector3 cameraPos = Camera.main.transform.position;
        tempStar.transform.LookAt(cameraPos);
        tempStar.transform.Rotate(90, 90, 90);

        Star star = new Star(tempStar, Vector3.Normalize(velocity) * 0.01f * speed);
        stars.Add(star);
        IEnumerator coroutine = RemoveStar(star);
        StartCoroutine(coroutine);
    }

    private IEnumerator RemoveStar(Star star)
    {
        yield return new WaitForSeconds(30f);
        stars.Remove(star);
        Destroy(star.starObject);
    }
}

struct Star
{
    public GameObject starObject;
    public Vector3 velocity;

    public Star(GameObject starObject, Vector3 velocity)
    {
        this.starObject = starObject;
        this.velocity = velocity;
    }
}
