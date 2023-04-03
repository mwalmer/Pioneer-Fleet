using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFleetBattle : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 5f;

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		
		Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
		
		pos += transform.rotation * velocity;

		transform.position = pos;
	}
    void Start(){
        StartCoroutine(Lifespan());
    }

    public IEnumerator Lifespan(){
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
