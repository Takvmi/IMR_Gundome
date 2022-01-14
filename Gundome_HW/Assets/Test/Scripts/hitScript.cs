using UnityEngine;

public class hitScript : MonoBehaviour
{
    public GameObject target;
    public GameObject gameManager;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.GetComponent<gameManagerScript>().Score++;
        Destroy(target);
    }
}
