using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGun : MonoBehaviour
{
    public Transform endPos;
    public Transform startPos;

    public float smoothing = .5f;

    private float cooldownTime = 0;
    private float timer = 0.5f;
    private float speed = 10;
    public Vector3 angle;

    bool lerp = false;

    // Update is called once per frame
    void Update()
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
    void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity);
        Debug.DrawRay(transform.position, Vector3.forward, Color.white, .1f);
        hit.collider.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }
    IEnumerator Lerp()
    {

        while (Vector3.Distance(transform.position, endPos.position) > 0.0001f)
        {
            transform.position = Vector3.Lerp(transform.position, endPos.transform.position, smoothing * Time.deltaTime);
            transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

            yield return null;
        }
        yield return new WaitForSeconds(3f);

    }
}
