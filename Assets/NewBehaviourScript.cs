using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        Sensor();
		
	}
    void Sensor()
    {
        RaycastHit hit;
        float distance;
        Vector3 forward= transform.TransformDirection(Vector3.forward)*5;
        Debug.DrawRay(transform.position,forward, Color.red);
        if(Physics.Raycast(transform.position,(forward),out hit))
        {
            distance = hit.distance;
            print(distance+" : "+hit.collider.name+"Object: "+hit.collider.gameObject.name);
        }
    }
}
