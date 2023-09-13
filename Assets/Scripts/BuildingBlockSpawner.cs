using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlockSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Vector2 spawnPosition = new Vector2(-11, 2);

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Spawn a random prefab
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.GenerateBlock, .7f);

            // Get the Rigidbody2D component and freeze it
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            // Wait for 3 seconds before unfreezing
            yield return new WaitForSeconds(2f);

            // Unfreeze the Rigidbody2D
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.velocity = new Vector2(0, -1);
            }

            // Wait for an additional 0.5 seconds before spawning the next object
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator UnfreezeAfterTime(Rigidbody2D rb, float time)
    {
        yield return new WaitForSeconds(time);
        if (rb != null)
        {
            Debug.Log("Unfreezing object at time: " + Time.time);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = new Vector2(0, -1);

        }
    }
}
