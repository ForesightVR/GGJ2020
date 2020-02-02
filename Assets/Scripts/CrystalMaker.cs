using UnityEngine;

public class CrystalMaker : MonoBehaviour
{
    public Animator createCrystalAnimator;
    public GameObject shards;
    
    
    private int NUM_SHARDS = 3;
    private int numShardsInMaker = 0;
    private bool animationAlreadyTriggered = false;
    private static readonly int AllThreeShardsInBucket = Animator.StringToHash("AllThreeShardsInBucket");

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("CrystalShard"))
        {
            return;
        }

        if (++numShardsInMaker == NUM_SHARDS)
        {
            foreach (Transform shard in shards.transform)
            {
                shard.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (!animationAlreadyTriggered)
            {
                createCrystalAnimator.SetTrigger(AllThreeShardsInBucket);
                animationAlreadyTriggered = true;
                Debug.Log("all three shards in bucket");
            }
        } 
    }

    private void Update()
    {
        //Debug.Log(numShardsInMaker);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("CrystalShard"))
        {
            return;
        }

        numShardsInMaker--;
    }
}
