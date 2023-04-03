using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(Lifespan());
    }

    public IEnumerator Lifespan(){
        yield return new WaitForSeconds(3.0f);
        
        Destroy(this.gameObject);
    }
}
