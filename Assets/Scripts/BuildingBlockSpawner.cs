using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlockSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Transform spawnPoint;

    public float scalingDuration = 0.15f; // Duration of the scaling animation in seconds

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Spawn a random prefab
            float randomValue = Random.value;

            GameObject prefabToSpawn;

            if (randomValue <= 0.15f) // 15% chance for element 0
            {
                prefabToSpawn = prefabs[0];
            }
            else if (randomValue <= 0.15f + 0.35f) // 35% chance for element 1
            {
                prefabToSpawn = prefabs[1];
            }
            else // 50% chance for element 2
            {
                prefabToSpawn = prefabs[2];
            }

            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.GenerateBlock, .7f);

            // Get the Transform component for scaling animation
            Transform spawnedTransform = spawnedObject.transform;

            // Store the original scale
            Vector3 originalScale = spawnedTransform.localScale;

            // Initial scale (you can set this to any value you want)
            Vector3 initialScale = Vector3.zero;

            // Target scale (1, 1, 1 is the original scale)
            Vector3 targetScale = originalScale;

            float elapsedTime = 0f;

            while (elapsedTime < scalingDuration)
            {
                // Interpolate the scale over time using Lerp
                spawnedTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scalingDuration);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // Ensure the final scale is exactly the target scale
            spawnedTransform.localScale = targetScale;

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
