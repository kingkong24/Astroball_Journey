using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOnOffButton : MonoBehaviour
{
    [SerializeField] BlockOnOffManagement blockOnOffManagement;
    [SerializeField] string OnOffNum;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            blockOnOffManagement.OnOffBlocks(OnOffNum);
        }
    }
}
