using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsController : MonoBehaviour
{
    [Header("Footsteps Settings")]
    [SerializeField] private List<AudioClip> footstepSoundsDirt;
    [SerializeField] private List<AudioClip> footstepSoundsStone;
    [SerializeField] private List<AudioClip> footstepSoundsWood;
    [SerializeField] private AudioSource footstepSource;





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

    private void PlayFootstepSound()
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

}
