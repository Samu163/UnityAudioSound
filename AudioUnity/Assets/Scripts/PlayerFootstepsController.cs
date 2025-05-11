using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsController : MonoBehaviour
{
    [Header("Footsteps Settings")]
    [SerializeField] private List<AudioClip> footstepSoundsDirt;
    [SerializeField] private List<AudioClip> footstepSoundsStone;
    [SerializeField] private List<AudioClip> footstepSoundsWood;
    [SerializeField] private AudioSource footstepSource;

    [SerializeField] private AudioClip jumpSoundWood;
    [SerializeField] private AudioClip jumpSoundDirt;
    [SerializeField] private AudioClip jumpSoundStone;

    [SerializeField] private AudioClip landSoundWood;
    [SerializeField] private AudioClip landSoundDirt;
    [SerializeField] private AudioClip landSoundStone;
    private bool wasGrounded = true;

    private float airStartY;
    private float airTime;
    private float airStartTime;

    [SerializeField] private float minFallDistance = 0.3f;
    [SerializeField] private float minAirTime = 0.4f;


    private enum MaterialType
    {
        Dirt,
        Stone, 
        Wood, 
        None
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private MaterialType SurfaceSelectFunctionWithRaycast()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MaterialType materialType = MaterialType.None;

            switch (hit.collider.tag)
            {
                case "Dirt":
                    materialType = MaterialType.Dirt;
                    break;
                case "Stone":
                    materialType = MaterialType.Stone;
                    break;
                case "Wood":
                    materialType = MaterialType.Wood;
                    break;
            }

            return materialType;
        }
        return MaterialType.None;
    }

    public void PlayFootstepSound()
    {
        List<AudioClip> selectedSounds = null;
        MaterialType materialType = SurfaceSelectFunctionWithRaycast();

        switch (materialType)
        {
            case MaterialType.Dirt:
                selectedSounds = footstepSoundsDirt;
                break;
            case MaterialType.Stone:
                selectedSounds = footstepSoundsStone;
                break;
            case MaterialType.Wood:
                selectedSounds = footstepSoundsWood;
                break;
        }

        if (selectedSounds != null && selectedSounds.Count > 0)
        {
            AudioClip clip = selectedSounds[Random.Range(0, selectedSounds.Count)];
            footstepSource.PlayOneShot(clip);
        }
    }

    public void PlayJumpSound()
    {
        AudioClip selectedSound = null;
        MaterialType materialType = SurfaceSelectFunctionWithRaycast();

        switch (materialType)
        {
            case MaterialType.Dirt:
                selectedSound = jumpSoundDirt;
                break;
            case MaterialType.Stone:
                selectedSound = jumpSoundStone;
                break;
            case MaterialType.Wood:
                selectedSound = jumpSoundWood;
                break;
        }

        if (selectedSound != null)
        {
            footstepSource.PlayOneShot(selectedSound);
        }
    }
    public void PlayLandSound()
    {
        AudioClip selectedSound = null;
        MaterialType materialType = SurfaceSelectFunctionWithRaycast();

        switch (materialType)
        {
            case MaterialType.Dirt:
                selectedSound = landSoundDirt;
                break;
            case MaterialType.Stone:
                selectedSound = landSoundStone;
                break;
            case MaterialType.Wood:
                selectedSound = landSoundWood;
                break;
        }

        if (selectedSound != null)
        {
            footstepSource.PlayOneShot(selectedSound);
        }
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!wasGrounded && IsGroundLayer(collision.gameObject))
    //    {
    //        wasGrounded = true;

    //        float fallDistance = airStartY - transform.position.y;
    //        airTime = Time.time - airStartTime;

    //        if (fallDistance > minFallDistance || airTime > minAirTime)
    //        {
    //           PlayLandSound();
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (IsGroundLayer(collision.gameObject))
    //    {
    //        wasGrounded = false;
    //        airStartY = transform.position.y;
    //        airStartTime = Time.time;
    //    }
    //}

    //private bool IsGroundLayer(GameObject obj)
    //{
    //    return obj.CompareTag("Dirt") || obj.CompareTag("Stone") || obj.CompareTag("Wood");
    //}

}
