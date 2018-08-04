using UnityEngine;

//TODO add namespace
public class AudioTrigger : MonoBehaviour
{
    [SerializeField] AudioClip clip; //clip to play
    [SerializeField] int layerFilter = 0; //layer index on which it plays, 0 is default layer
    [SerializeField] float triggerRadius = 5f; //triggering radius
    [SerializeField] bool isOneTimeOnly = true; //is it a one time only audio clip

    [SerializeField] bool hasPlayed = false; //if the clip has already been played, a state variable
    AudioSource audioSource; //source of audio clip

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); //adding audio source component at runtime
        audioSource.playOnAwake = false; //ensure that it does not play on awake
        audioSource.clip = clip; //sets the specified clip and attaches to audiosource

        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>(); //used for Gizmo
        sphereCollider.isTrigger = true; 
        sphereCollider.radius = triggerRadius;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerFilter) //will be set to player layer later on
        {
            RequestPlayAudioClip(); //plays audioclip if play conditions are valid
        }
    }

    void RequestPlayAudioClip()
    {
        if (isOneTimeOnly && hasPlayed) //if already played and clip is one time only, DO NOT PLAY
        {
            return;
        }
        else if (audioSource.isPlaying == false) //if NOT playing
        {
            audioSource.Play();
            hasPlayed = true;//mark clip as played
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 255f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }

    //TODO need to add AudioTrigger to player for hittting enemies and 
    //taking hits from enemies.
}