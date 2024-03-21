using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public float metalSphereVelocity;

    private Rigidbody rb;
    public GameObject metalSphere;
    public GameObject rightElastic;
    public GameObject leftElastic;
    public GameObject rightLine;
    public GameObject leftLine;
    public GameObject leather;
    public GameObject leatherLine;
    private LineRenderer rightElasticLine;
    private LineRenderer leftElasticLine;
    public AudioClip fireSoundClip;
    private AudioSource fireAudioSource;
    private float pulled;
    private int i;
    private float z;

    void Start()
    {
 
        fireAudioSource = gameObject.AddComponent<AudioSource>();

     
        fireAudioSource.clip = fireSoundClip;

        i = 1;
        z = -2;
        if (this.GetComponent<ElasticManager>() != null)
        {
            pulled = this.GetComponent<ElasticManager>().elasticResistance - 8;
        }
        else
        {
            pulled = -7;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           
            z -= 0.1f;
                if (i == 1)
                {
                    metalSphere = Instantiate(metalSphere, new Vector3(metalSphere.transform.position.x, metalSphere.transform.position.y, -3), Quaternion.identity);
                    rb = metalSphere.GetComponent<Rigidbody>();
                    metalSphere.SetActive(true);
                    metalSphere.transform.parent = this.transform;
                    i = 0;
                }

                rightElastic.GetComponent<SkinnedMeshRenderer>().enabled = false;
                leftElastic.GetComponent<SkinnedMeshRenderer>().enabled = false;
                leather.GetComponent<SkinnedMeshRenderer>().enabled = false;

                rightLine.SetActive(true);
                leftLine.SetActive(true);
                leatherLine.GetComponent<SkinnedMeshRenderer>().enabled = true;

                rightElasticLine = rightLine.transform.GetComponent<LineRenderer>();
                leftElasticLine = leftLine.transform.GetComponent<LineRenderer>();

                if (z >= pulled)
                {
                    rightElasticLine.SetPosition(1, new Vector3(0, 0, z));

                    leftElasticLine.SetPosition(1, new Vector3(0, 0, z));

                    metalSphere.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.7f);
                    leather.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.2f);
                    leatherLine.transform.localPosition = new Vector3(-1.42f, 2.286f, z + 1.2f);
                }
                else
                {
                    rightElasticLine.SetPosition(1, new Vector3(0, 0, pulled));
                    leftElasticLine.SetPosition(1, new Vector3(0, 0, pulled));
                    metalSphere.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.7f);
                    leather.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.2f);
                    leatherLine.transform.localPosition = new Vector3(-1.42f, 2.286f, pulled + 1.2f);
                }

        }
        if (Input.GetMouseButtonUp(0))
        {
            fireAudioSource.Play();
            i = 1;

            rightElastic.GetComponent<SkinnedMeshRenderer>().enabled = true;
            leftElastic.GetComponent<SkinnedMeshRenderer>().enabled = true;
            leather.GetComponent<SkinnedMeshRenderer>().enabled = true;

            rightLine.SetActive(false);
            leftLine.SetActive(false);
            leatherLine.GetComponent<SkinnedMeshRenderer>().enabled = false;

            rb.useGravity = true;

            if (z >= -7)
            {
                rb.AddForce(transform.forward * metalSphereVelocity * -z, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(transform.forward * metalSphereVelocity * 15, ForceMode.Impulse);
            }
            metalSphere.transform.parent = null;
            z = -2;
        }
    }
}
