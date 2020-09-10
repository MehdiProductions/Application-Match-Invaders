using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAlien : MonoBehaviour
{
    SpriteRenderer render;
    [SerializeField] private float delay = 0.5f;
    AudioSource audiosource;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator DestroyExplosion()
    {
        audiosource.Play();
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject, delay);
    }
}
