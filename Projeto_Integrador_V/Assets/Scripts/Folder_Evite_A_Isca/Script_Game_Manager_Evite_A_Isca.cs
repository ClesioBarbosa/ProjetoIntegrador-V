using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Script_Game_Manager_Evite_A_Isca : MonoBehaviour
{
    string Right_Word, 
        Right_Domain;

    public GameObject Door_Prefab, 
    Door_Mold2, 
    Door_Mold3, 
    Door_Mold4, 
    Door_Mold5;

    GameObject Door_Molds;

    //Os nomes dos sites s�o sobrenomes de amigos do meu primeiro semestre, parceiros de equipe de projetos integradores, professores de oficina e orientadores das minhas equipes
    //Quem achar ruim � bobo
    List<string> Possible_Names = new List<string> { "Pereira", "Rozendo", "Nascimento", "Campos", "Rangel", "Santos", "Ferreira", "Lima", "Caridade", "Barreto", 
        "Oliveira", "Mota", "Mafra", "Jacinto", "Silva", "Lopes", "Desiderio", "Brito", "Braga", "Cerqueira", 
        "Sanches", "Barbosa", "Pacheco", "Freire", "Cruz", "Carmo", "Souto", "Cunha", "Andrade", 
        "Rodovalho", "Arnaut", "Maior", "Sales", "Forte", "Warley", "Valenca", "Araujo", "Xavier", "Mendes", 
        "Costa", "Leite", "Junior", "Almeida", "Carvalho", "Melo", "Neto", "Gomes"},
        Possible_Domains = new List<string> { ".com", ".org", ".gov", ".edu", ".info", ".io", ".net", ".online", ".blog", ".app" },
        Possible_Progressions = new List<string> { "Doors", "Time", "Difficult"};

    List<string> Generate_All_Words()
    {
        List<string> Words = new List<string>();

        for (int i = 0; i < Door_Amount; i++)
        {
            Words.Add(Generating_Wrong_Words());
        }

        return Words;
    }

    List<GameObject> Doors_List = new List<GameObject>();

    float Max_Timer,
        Current_Timer,
        Initial_Time,
        Dramatic_Time;

    public int Player_Score, 
        Player_Highscore;

    [SerializeField] public TextMeshProUGUI Right_Word_Text, 
        Timer_Display, 
        Score_Display;

    public Image Black_Fades;

    Color BlackC;

    bool Is_On_Round, 
        First_Round,
        Dramatic_Pause;

    public bool FadeIn,
        FadeOut;

    int Deception_Level, 
        Door_Amount;

    float Spacing = 3f, 
    Weed_Height;

    public Script_Camera_Logic cam;

    public Script_Camera_Fade Fade;

    AlgaeWave[] algaeScripts;

    void Start()
    {
        Player_Score = 0; 
        Door_Amount = 5; 
        Deception_Level = 1;
        Max_Timer = 30f;

        Dramatic_Pause = false;
        Dramatic_Time = 0f;

        First_Round = true;
        Initial_Time = 0f;

        Start_New_Round();
    }

    void Update()
    {

        Time_Ticking();
        Touching_Screen();
        Hiding_Screen();

        if (First_Round)
        {

            Initial_Time += Time.deltaTime;

            if(Initial_Time > 3f)
            {
                First_Round = false;
                Is_On_Round = true;
            }
        }
    }

    void Generating_Correct_Word()
    {
        Right_Word = (Possible_Names[Random.Range(0, Possible_Names.Count)]);
        Right_Domain = (Possible_Domains[Random.Range(0, Possible_Domains.Count)]);

        Right_Word_Text.text = (Right_Word + Right_Domain);

        List<string> s = Generate_All_Words();
        Spawn_Doors(s);
    }

    string Generating_Wrong_Words()
    {
        string Wrong_Word = Right_Word;
        string Wrong_Domain = Right_Domain;

        int Randomize_Chances = 0;




        switch (Deception_Level)
        {
            case 1:
                Randomize_Chances = 0;
                break;

            case 2:
                Randomize_Chances = Random.Range(0, 2);
                break;

            case 3:
                Randomize_Chances = Random.Range(0, 3);
                break;
        }

        if(Randomize_Chances == 0)
        {
            Wrong_Domain = Generating_Fake_Domain(Wrong_Domain);
        }
        else if (Randomize_Chances == 1)
        {
            Randomize_Chances = Random.Range(0, 2);


            if (Randomize_Chances == 0)
            {

                Wrong_Domain = Generating_Fake_Domain(Wrong_Domain);
            }

            Wrong_Word = Generating_Name_Without_Some_Letter(Wrong_Word);
        }
        else if(Randomize_Chances == 2)
        {

            Randomize_Chances = Random.Range(0, 3);

            if (Randomize_Chances == 0)
            {

                Wrong_Domain = Generating_Fake_Domain(Wrong_Domain);
            }

            Wrong_Word = Generating_Fake_Name(Wrong_Word);
        }
        

        return Wrong_Word + Wrong_Domain;
    }

    string Generating_Fake_Domain(string Wrong_Domain)
    {


        string Fake_Domain = Wrong_Domain;

        

        while (Fake_Domain == Right_Domain)
        {
            Fake_Domain = (Possible_Domains[Random.Range(0, Possible_Domains.Count)]);
        }

        return Fake_Domain;
    }

    string Generating_Name_Without_Some_Letter(string Wrong_Word)
    {

        int indexToRemove = Random.Range(0, Wrong_Word.Length);

        string Fake_Name = Wrong_Word.Remove(indexToRemove, 1);

        return Fake_Name;

    }

    string Generating_Fake_Name(string Wrong_Word)
    {
        List<string[]> similar_Digits = new List<string[]>
        {
        new string[] { "a", "o", "c", "0", "e" },
        new string[] { "i", "l", "1", "j" },
        new string[] { "n", "m", "w", "v", "u" },
        new string[] { "q", "p", "d", "g", "h", "k", "x", "b", "5", "6" },
        new string[] { "t", "f", "r", "y"},
        new string[] { "s", "z", "5" }
        };

        char[] chars = Wrong_Word.ToCharArray();

        
        int index = Random.Range(0, chars.Length);
        char original_Char = char.ToLower(chars[index]);

        
        foreach (var group in similar_Digits)
        {
            if (group.Contains(original_Char.ToString()))
            {
                
                string new_Char;
                do
                {
                    new_Char = group[Random.Range(0, group.Length)];
                }
                while (new_Char[0] == original_Char);

                chars[index] = new_Char[0];
                break;
            }
        }

        return new string(chars);


    }

    void Spawn_Doors(List<string> Names)
    {

        int correct_Index = Random.Range(0, Door_Amount);

        float total_Width = (Door_Amount - 1) * Spacing;
        float start_X = -total_Width / 2;

        switch (Door_Amount)
        {
            case 2: Door_Molds = Instantiate(Door_Mold2);    break;
            case 3: Door_Molds = Instantiate(Door_Mold3);    break;
            case 4: Door_Molds = Instantiate(Door_Mold4);    break;
            case 5: Door_Molds = Instantiate(Door_Mold5);    break;
        }

        algaeScripts = Door_Molds.GetComponentsInChildren<AlgaeWave>();


        foreach (AlgaeWave algae in algaeScripts)
        {
            algae.height = Random.Range(0.5f, 1.5f);
        }

        for(int i = 0; i < Door_Amount; i++)
        {
            Vector3 Door_Position = new Vector3(start_X + i * Spacing,
                Door_Prefab.transform.position.y,
                Door_Prefab.transform.position.z);

            GameObject Door_Instance = Instantiate(Door_Prefab,
                Door_Position,
                Door_Prefab.transform.rotation);

            Doors_List.Add(Door_Instance);

            string textToUse;

            Script_Doors_Evite_A_Isca Door_Script = Door_Instance.GetComponent<Script_Doors_Evite_A_Isca>();

            Door_Script.Main_Script = this.GetComponent<Script_Game_Manager_Evite_A_Isca>();

            if (i == correct_Index)
            {
                textToUse = Right_Word + Right_Domain;
                Door_Instance.tag = "Correct_Tag";
            }
            else
            {
                textToUse = Names[i];
                Door_Instance.tag = "Wrong_Tag";
            }


            TextMeshPro Door_Text = Door_Instance.GetComponentInChildren<TextMeshPro>();
            Door_Text.text = textToUse;

        }
    }

    void Start_New_Round()
    {
        cam.Had_To_Move = false;

        Score_Display.text = Player_Score.ToString();

        foreach (GameObject door in Doors_List)
        {
            Destroy(door);
        }

        if(Door_Molds != null)
        {
            Destroy(Door_Molds);
        }

        algaeScripts = null;

        Doors_List.Clear();
        Current_Timer = Max_Timer;

        if (!First_Round)
        {
            Is_On_Round = true;
        }
        
        Generating_Correct_Word();
    }

    void Time_Ticking()
    {
        if (Is_On_Round)
        {
            Current_Timer -= Time.deltaTime;

            


            if (Current_Timer <= 0f)
            {
                Defeat();
            }
        }

        Timer_Display.text = ((int)Current_Timer).ToString();
    }

    void Touching_Screen()
    {
        if (Is_On_Round)
        {
            if(Input.touchCount > 0)
            {
                Touch Getting_Touch = Input.GetTouch(0);

                if(Getting_Touch.phase == TouchPhase.Began)
                {

                    //Eu n�o pensei em nomes condinzentes para por nessas variaveis...
                    Ray r = Camera.main.ScreenPointToRay(Getting_Touch.position);
                    RaycastHit hit;

                    if(Physics.Raycast(r, out hit))
                    {
                        Script_Doors_Evite_A_Isca Door_Object = hit.transform.GetComponentInParent<Script_Doors_Evite_A_Isca>();

                        if (Door_Object != null)
                        {
                            cam.Create_Waypoint(new Vector3(Door_Object.transform.position.x, Door_Object.transform.position.y, 0f));
                            cam.Had_To_Move = true;

                            Is_On_Round = false;
                            StartCoroutine(Door_Object.Open_Door());
                        }
                        else
                        {
                            Debug.LogWarning("Nenhuma porta encontrada no objeto clicado!");
                        }
                    }
                }
            }
        }
    }

    public void Defeat()
    {
        Is_On_Round = false;

        Fade.Fade_Out = true;

        SceneManager.LoadScene("MenuMiniGames");
    }

    void Hiding_Screen()
    {
        if (FadeIn)
        {
            BlackC.a += Time.deltaTime;
            Black_Fades.color = BlackC;
            if (BlackC.a > 0.99f)
            {
                BlackC.a = 1f;
                Black_Fades.color = BlackC;

                FadeIn = false;
                Dramatic_Pause = true;
            }
        }

        if (Dramatic_Pause)
        {
            Dramatic_Time += Time.deltaTime;

            if (Dramatic_Time > 1f)
            {
                Dramatic_Pause = false;
                FadeOut = true;
                Start_New_Round();
                Dramatic_Time = 0f;
            }
        }
        else if (FadeOut)
        {
            BlackC.a -= Time.deltaTime;
            Black_Fades.color = BlackC;

            if (BlackC.a < 0.01f)
            {
                BlackC.a = 0f;
                Black_Fades.color = BlackC;

                FadeIn = false;
                FadeOut = false;
            }
        }
    }

    public void Right_Ansher()
    {

        Player_Score++;
        Score_Display.text = Player_Score.ToString();

        if (Player_Score % 5 == 0 && Possible_Progressions.Count != 0)
        {
            
            Difficulting();
        }
    }

    void Difficulting()
    {


        string Becoming_Harder = (Possible_Progressions[Random.Range(0, Possible_Progressions.Count)]);

        //Quero fazer um indicador visual do que est� ficando mais dificil
        print($"Aumentar dificuldade: {Becoming_Harder}");

        switch (Becoming_Harder)
        {
            case "Doors": Add_Doors(); break;
            case "Time":    Reduce_Max_Timer(); break;
            case "Difficult":   Rise_Deception_Level();    break;


        }
    }

    void Add_Doors()
    {
        Door_Amount++;

        print($"Quantidade de portas: {Door_Amount}");

        if(Door_Amount == 5)
        {

            Possible_Progressions.Remove("Doors");
            
        }
    }

    void Reduce_Max_Timer()
    {
        Max_Timer -= 5f;

        print($"Tempo m�ximo: {Max_Timer}");

        if (Max_Timer == 5f)
        {
            Possible_Progressions.Remove("Time");
            
        }
    }


    void Rise_Deception_Level()
    {
        Deception_Level++;

        print($"N�vel de Engana��o: {Deception_Level}");

        if (Deception_Level == 3)
        {
            Possible_Progressions.Remove("Difficult");
            
        }
    }
}
