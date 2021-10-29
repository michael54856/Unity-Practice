using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public Transform startShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray finder = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit contact;
            Vector3 target;
            if (Physics.Raycast(finder, out contact))
            {
                // save the contact point
                target = contact.point;
                Vector3 dir = target - startShoot.position;
                // spawn the projectile on the gun and rotate it in the direction of the contact point
                GameObject myBullet = Instantiate(bulletPrefabs, startShoot.position, Quaternion.LookRotation(dir));
                myBullet.GetComponent<Rigidbody>().AddForce(myBullet.transform.forward * 3000);

            }
            else
            {
                target = finder.GetPoint(750);
                GameObject myBullet = Instantiate(bulletPrefabs, startShoot.position, Quaternion.LookRotation(target));
                myBullet.GetComponent<Rigidbody>().AddForce(myBullet.transform.forward * 3000);

            }
            
        }
    }
}
