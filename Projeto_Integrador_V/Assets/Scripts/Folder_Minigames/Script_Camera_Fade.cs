using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Camera_Fade : MonoBehaviour
{
    [SerializeField] Image Black_Image;
    Color Black_Color;

    public bool Fade_In;
    public bool Fade_Out;
    public bool Dramatic_Fade_In;

    public string Minigame_Scene;

    public bool Ready;

    public MonoBehaviour Minigame_Script;
    float time = 0f;

    void Start()
    {
        Black_Color = Black_Image.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (Dramatic_Fade_In)
        {

            if (Minigame_Script != null)
            {
                Minigame_Script.enabled = true;
            }

            time += Time.deltaTime;

            if (time > 2f)
            {
                Fade_In = true;
            }
        }

        if (Fade_In && Black_Image.color.a > 0f && !Ready)
        {
            Dramatic_Fade_In = false;
            Black_Color.a -= 2f * Time.deltaTime;
            Black_Image.color = Black_Color;
        }
        else if (Fade_In && Black_Image.color.a <= 0f)
        {
            Black_Color.a = 0f;
            Black_Image.color = Black_Color;
            Ready = true;
            Fade_In = false;

            
        }

        if (Fade_Out && Black_Image.color.a < 1f)
        {
            Black_Color.a += 2f * Time.deltaTime;
            Black_Image.color = Black_Color;

            if (Black_Image.color.a >= 1f)
            {
                Black_Color.a = 1f;
                Black_Image.color = Black_Color;
                SceneManager.LoadScene(Minigame_Scene);
            }
        }
    }

}
