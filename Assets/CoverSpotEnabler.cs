using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSpotEnabler : MonoBehaviour
{
    public GameObject coverspots;
    // Start is called before the first frame update
    void Start()
    {
        coverspots.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            coverspots.SetActive(true);
        }
        Destroy(gameObject);
    }
}
