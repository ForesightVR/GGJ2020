using UnityEngine;
using System.Collections.Generic;
public class CrystalMaker : MonoBehaviour
{
    public PowerManager powerManager;
    public Animator createCrystalAnimator;
    public GameObject shards;
    public GameObject crystal;
    
    private int NUM_SHARDS = 3;
    private int numShardsInMaker = 0;
    private bool animationAlreadyTriggered = false;
    private static readonly int AllThreeShardsInBucket = Animator.StringToHash("AllThreeShardsInBucket");

    bool ready;

    List<Transform> newShards = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("CrystalShard"))
        {
            return;
        }
        other.transform.parent = transform;

        other.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        if (!newShards.Contains(other.transform))
            newShards.Add(other.transform);

        if (newShards.Count >= 3)
        {
            Debug.Log("Ready");
            ready = true;

            foreach (Transform shard in newShards)
            {
                shard.GetComponent<Collider>().enabled = false;
                shard.GetComponent<Rigidbody>().isKinematic = false;
                shard.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

        //if (++numShardsInMaker == NUM_SHARDS)
        //{
        //    foreach (Transform shard in shards.transform)
        //    {
        //    }

        //    if (!animationAlreadyTriggered)
        //    {
        //       // createCrystalAnimator.SetTrigger(AllThreeShardsInBucket);
        //        animationAlreadyTriggered = true;
        //        Debug.Log("all three shards in bucket");
        //        ready = true;
        //    }
        //} 
    }

    private void Update()
    {
        //Debug.Log(numShardsInMaker);

        if(ready)
        {
            foreach (Transform shard in newShards)
                shard.transform.position = Vector3.MoveTowards(shard.transform.position, transform.position, 5 * Time.deltaTime);

            bool allCentered = true;

            foreach(Transform shard in newShards)
            {
                if (shard.transform.position != transform.position)
                    allCentered = false;
            }

            if(allCentered)
            {
                foreach (Transform shard in newShards)
                    Destroy(shard.gameObject);

                crystal.SetActive(true);
                newShards.Clear();
                ready = false;

                powerManager.StartPanel(0);
                gameObject.SetActive(false);
            }
        }
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
