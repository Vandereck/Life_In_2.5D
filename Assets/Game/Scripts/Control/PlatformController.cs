using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool platformRealOrShadow;           // Indicate this platform whether it's real or shadow
    public ParticleSystem shapeParticle;
    private MeshRenderer meshRenderer;
    private GameObject player;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider= GetComponent<BoxCollider>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bool playerRealOrShadow = player.GetComponent<PlayerController>().realOrShadow;
        bool sameDimension = playerRealOrShadow == platformRealOrShadow;
        float alphaNow = sameDimension ? 1.0f : 0.2f;

        Color color = new(meshRenderer.material.color.r, meshRenderer.material.color.g,
                                meshRenderer.material.color.b, alphaNow);
        meshRenderer.material.color = color;
        Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), boxCollider, !sameDimension);

        if (!sameDimension && !shapeParticle.isPlaying)
        {
            shapeParticle.Play();
        }
        else if (sameDimension && shapeParticle.isPlaying)
        {
            shapeParticle.Stop();
        }

    }
        
}
