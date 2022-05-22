using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTrack : MonoBehaviour
{
    public float TimeBtwTracks;
    public GameObject Track;
    public float TriggerRadius;
    public LayerMask Layer;

    private float curTime;

    void Start()
    {
        curTime = 0f;
        TimeBtwTracks *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime >= TimeBtwTracks)
        {
            var surTracks = Physics2D.OverlapCircleAll(transform.position, TriggerRadius, Layer);

            foreach (var track in surTracks)
                Destroy(track.gameObject);

            Instantiate(Track, transform.position, transform.rotation);
            curTime = 0f;
        }
        curTime += Time.deltaTime;
    }
}
