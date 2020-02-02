using UnityEngine;

public class ShardController : MonoBehaviour
{
    public GameObject crystalPrefab;
    
    public void SpawnCrystal()
    {
        foreach (Transform shard in transform)
        {
            Destroy(shard.gameObject);
        }
        
        var pos = transform.position;
        Debug.Log(pos);

        Instantiate(crystalPrefab, transform);
    }
}
