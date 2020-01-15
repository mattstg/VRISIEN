using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//StunGun
public class StunGun : MonoBehaviour
{
    private Transform endPos; // can be the  player's location where the gun will lerp back and forth 
    private Transform startPos;


    public float smoothing = 1f;

    private float cooldownTime = 0; // time difference between shooting of bullets
    private float timer = 0.5f;

    public Vector3 angle;

    bool lerp = false;

    public void Initialize()
    {

        endPos = GameObject.Find("endingPos").transform;
        startPos = this.transform;

    }
    public void Refresh()
    {
        cooldownTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1) && cooldownTime > timer)
        {
            Shoot();
            cooldownTime = 0;

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Lerp());
        }
    }
    public void PhysicsRefresh()
    {

    }
    void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity);
        Debug.DrawRay(transform.position, Vector3.forward, Color.white, .1f);
        hit.collider.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }
    IEnumerator Lerp()
    {
        // needs to be fixed.
        if (this.transform.position == endPos.position)
        {
            while (Vector3.Distance(transform.position, startPos.position) != startPos.position.sqrMagnitude)
            {
                transform.position = Vector3.Lerp(transform.position, startPos.transform.position, smoothing * Time.deltaTime);
                transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

                yield return null;
            }
        }
        else
        {
            while (Vector3.Distance(transform.position, endPos.position) != endPos.position.sqrMagnitude)
            {
                transform.position = Vector3.Lerp(transform.position, endPos.transform.position, smoothing * Time.deltaTime);
                transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);

    }
}
