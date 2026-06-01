using UnityEngine;

public class Script_Object_Rotate : MonoBehaviour
{

    private Vector3 startPos;
    float Destruction = 5f;

    public bool Destroy_Prop;

    void Start()
    {
        startPos = this.gameObject.transform.position;
    }

    void Update()
    {

        float newY = startPos.y + Mathf.Sin(Time.time * 1f) * 0.5f;
        transform.position = new Vector3(startPos.x, newY, startPos.z);


        transform.Rotate(0f, 60f * Time.deltaTime, 0f);

        Destruction -= Time.deltaTime;

        if(Destruction < 0f && Destroy_Prop)
        {
            Destroy(gameObject);
        }
    }
}
