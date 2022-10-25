using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public float speedFactor;
    public float Delay;
    public float DelaySmol;
   
    // Start is called before the first frame update
    void Start()
    {
     
        StartCoroutine("Shoots");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shoots()
    {

        StopCoroutine("Shoots2");
        yield return new WaitForSeconds(DelaySmol);
        GameObject clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = transform.up * speedFactor;
        yield return new WaitForSeconds(DelaySmol);
        clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = transform.up * speedFactor;
        yield return new WaitForSeconds(DelaySmol);
        clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = transform.up * speedFactor;
        yield return new WaitForSeconds(DelaySmol);
        clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = transform.up * speedFactor;
        yield return new WaitForSeconds(DelaySmol);
        clone = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = transform.up * speedFactor;
        yield return new WaitForSeconds(DelaySmol);
        StartCoroutine("Shoots2");
            
         
    }

    IEnumerator Shoots2()
    {
        while (true)
        {
            //StopCoroutine("Shoots");
            yield return new WaitForSeconds(Delay);
            StartCoroutine("Shoots");
            
        }
        
    }


}
