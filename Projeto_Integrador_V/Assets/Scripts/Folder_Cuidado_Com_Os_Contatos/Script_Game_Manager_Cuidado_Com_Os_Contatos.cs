using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Game_Manager_Cuidado_Com_Os_Contatos : MonoBehaviour
{
    bool Correct,
        First_Profile,
        Is_On_Round,
        Name_Changed,
        Picture_Changed;

    int Max_Messages,
        Messages,
        Score,
        Inconsistences;

    float Sending_Ratio,
        Max_Timer,
        Current_Timer, 
        Min_Swipe_Distance = 100f;

    Color c;

    List<string> Possible_Names = new List<string> { "Ana", "Andre", "Amanda", "Arthur", "Alice", "Augusto", "Aline", "Adriano", "Alessandra", "Antonio",
        "Bruno", "Bianca", "Beatriz", "Bernardo", "Barbara", "Breno", "Bruna", "Benicio", "Bento", "Beto",
        "Carlos", "Camila", "Caio", "Carolina", "Cesar", "Clara", "Cristiano", "Cintia", "Caue", "Claudio",
        "Daniel", "Daniela", "Diego", "Debora", "Davi", "Diana", "Douglas", "Denise", "Dalton", "Darlan",
        "Eduardo", "Erika", "Elias", "Elaine", "Enzo", "Ester", "Everton", "Eliane", "Emanoel", "Ellen",
        "Felipe", "Fernanda", "Fabio", "Flavia", "Francisco", "Fabiana", "Fernando", "Fatima", "Frederico", "Filipe",
        "Gabriel", "Gabriela", "Gustavo", "Giovana", "Guilherme", "Gisele", "Geraldo", "Gloria", "Geovane", "Gilberto",
        "Henrique", "Helena", "Hugo", "Heloisa", "Heitor", "Hadassa", "Higor", "Hilda", "Herbert", "Helio",
        "Igor", "Isabela", "Ivan", "Ingrid", "Icaro", "Iara", "Isaque", "Ivone", "Israel", "Irineu",
        "Joao", "Juliana", "Jose", "Julia", "Jefferson", "Jessica", "Jonas", "Janaina", "Joaquim", "Junior",
        "Kaio", "Karina", "Kelvin", "Kelly", "Kaua", "Katia", "Kleber", "Kiara", "Kawan", "Karen",
        "Lucas", "Larissa", "Leonardo", "Luana", "Luiz", "Leticia", "Leandro", "Livia", "Lorenzo", "Lucio",
        "Marcos", "Maria", "Mateus", "Mariana", "Miguel", "Marcia", "Murilo", "Mirela", "Marcelo", "Milena",
        "Nicolas", "Natalia", "Nelson", "Nicole", "Nathan", "Nayara", "Nataniel", "Neide", "Nivaldo", "Noemi",
        "Otavio", "Olivia", "Osvaldo", "Olivia", "Orlando", "Odete", "Othon", "Olga", "Omar", "Ofelia",
        "Paulo", "Patricia", "Pedro", "Priscila", "Pablo", "Pamela", "Pietro", "Paloma", "Patrick", "Penelope",
        "Quirino", "Queila", "Quesia", "Quirina", "Quincas", "Quelen", "Quesia", "Quirineu", "Quiteria", "Quiana",
        "Rafael", "Renata", "Rodrigo", "Raquel", "Ricardo", "Rita", "Ramon", "Roberta", "Ruan", "Rosana",
        "Samuel", "Sabrina", "Sergio", "Simone", "Sandro", "Sara", "Silas", "Sofia", "Saulo", "Sheila",
        "Thiago", "Tatiane", "Tiago", "Taina", "Tomas", "Teresa", "Tulio", "Talita", "Theo", "Tereza",
        "Ulisses", "Ubirajara", "Ueliton", "Uilson", "Ueslei", "Uanda", "Urias", "Uelma", "Ugo", "Ualace",
        "Victor", "Vanessa", "Vinicius", "Vitoria", "Valter", "V�nia", "Vitor", "Ver�nica", "Vicente", "Viviane",
        "William", "Wagner", "Wesley", "Wanessa", "Willian", "Wendel", "Walace", "Wilma", "Wallyson", "Washington",
        "Xavier", "Ximena", "Xande", "X�nia", "Xisto", "Xuxa", "Xadrez", "Xarleen", "Xaviera", "Ximene",
        "Yuri", "Yasmin", "Yago", "Yara", "Yan", "Yohana", "Ygor", "Yvone", "Yago", "Yandra",
        "Ze", "Zilda", "Zacarias", "Zuleica", "Zeno", "Zara", "Zaqueu", "Zoraide", "Zelia", "Zoran"},

        Possible_Progressions = new List<string> { "Quantity", "Time", "Ratio", "Inconsistences" },
        Possible_Contacts = new List<string> { "Peixe1", "Peixe2", "Peixe3", "Peixe4", "Peixe5", "Peixe6", "Peixe7" },
        Possible_Figures = new List<string> { "Circ", "Oval", "Tria", "Reta", "Trap", "Losa", "Quad", "Pent", "Hexa" },
        Messsage_Order = new List<string> { },
        Fake_Message_Order = new List<string> { } ;

    public TextMeshProUGUI Profile_Name,
        Score_Display,
        Timer_Display;

    public Sprite Circ,
        Oval,
        Tri,
        Ret,
        Trap,
        Los,
        Quad,
        Pent,
        Hex,
        Peixe1,
        Peixe2,
        Peixe3,
        Peixe4,
        Peixe5,
        Peixe6,
        Peixe7;

    public Image Profile_Picture, 
        Message_Display,
        Background,
        Black_out;

    public GameObject /*Player,
        Hook,*/
        Name_Object,
        Profile_Object;

    //Vector3 Hook_Starting_Position, Hook_Ending_Position;

    Vector2 touchStart, touchEnd;

    void Start()
    {
        /* Hook_Starting_Position = new Vector3(Hook.transform.position.x, Hook.transform.position.y, Hook.transform.position.z);
        Hook_Ending_Position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1f, Player.transform.position.z); */

        Max_Timer = 30f;
        Score = 0;
        Inconsistences = 5;
        Max_Messages = 3;
        Messages = 0;
        Sending_Ratio = 3f;
        Is_On_Round = false;
        c = new Color(255f, 255f, 255f, 0f);

        Message_Display.color = c;

        Color blackoutColor = Black_out.color;
        blackoutColor.a = 0f;
        Black_out.color = blackoutColor;

        StartRound();

        Profile_Picture.sprite = Peixe5;
    }


    void Update()
    {

        if (Is_On_Round)
        {
            Touching_System();
            Time_Ticking();
        }
        else
        {
            Current_Timer = Max_Timer;
            //Hook.transform.position = Hook_Starting_Position;
        }
        
    }

    public void StartRound()
    {
        Messsage_Order.Clear();
        Fake_Message_Order.Clear();

        //Hook.transform.position = Hook_Starting_Position;
        Profile_Name.text = (Possible_Names[Random.Range(0, Possible_Names.Count)]).ToString();
        Score_Display.text = Score.ToString();
        if(Score == 0)
        {
            Score_Display.text = "Score";
        }
        Current_Timer = Max_Timer;

        Messages = 0;
        First_Profile = true;
        Name_Changed = false; Picture_Changed = false;

        Sending_The_Message();
    }

    void Time_Ticking()
    {
        if (Is_On_Round)
        {
            Current_Timer -= Time.deltaTime;

            float t = 1f - (Current_Timer / Max_Timer);

            /*t = Mathf.Clamp01(t);

            t = Mathf.SmoothStep(0f, 1f, t);

            Hook.transform.position = Vector3.Lerp(Hook_Starting_Position, Hook_Ending_Position, t);

            if (Current_Timer <= 0f)
            {
                print("Perdeu");
            }*/

            Timer_Display.text = ((int)Current_Timer).ToString();
        }
    }

    void Touching_System()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    touchStart = touch.position;

                    break;



                case TouchPhase.Ended:

                    touchEnd = touch.position;

                    float swipeDistance =
                    touchEnd.x - touchStart.x;

                    if (Mathf.Abs(swipeDistance)
                        > Min_Swipe_Distance)
                    {

                        if (swipeDistance > 0)
                        {
                            if (Correct)
                            {
                                Right_Ansher();
                            }
                            else
                            {
                                Defeat();
                            }
                        }


                        else
                        {
                            if (!Correct)
                            {
                                Right_Ansher();
                            }
                            else
                            {
                                Defeat();
                            }
                        }

                        Is_On_Round = false;
                    }

                    break;
            }
        }
    }

    public void Sending_The_Message()
    {
        
        Messages++;


        string Figure = Possible_Figures[Random.Range(0, Possible_Figures.Count)];


        Messsage_Order.Add(Figure);


        switch (Figure)
        {
            case "Circ":
                Message_Display.sprite = Circ;
                break;

            case "Oval":
                Message_Display.sprite = Oval;
                break;

            case "Tria":
                Message_Display.sprite = Tri;
                break;

            case "Reta":
                Message_Display.sprite = Ret;
                break;

            case "Trap":
                Message_Display.sprite = Trap;
                break;

            case "Losa":
                Message_Display.sprite = Los;
                break;

            case "Quad":
                Message_Display.sprite = Quad;
                break;

            case "Pent":
                Message_Display.sprite = Pent;
                break;

            case "Hexa":
                Message_Display.sprite = Hex;
                break;
        }

        StartCoroutine(Blink_Message());
    }

    IEnumerator Blink_Message()
    {
        Color c = Message_Display.color;


        c.a = 1;
        Message_Display.color = c;

        yield return new WaitForSeconds(Sending_Ratio / 2f);


        c.a = 0;
        Message_Display.color = c;

        yield return new WaitForSeconds(Sending_Ratio / 2f);


        if (Messages < Max_Messages)
        {
            Sending_The_Message();
        }
        else
        {
            StartCoroutine(Blackout_Transition());
        }
    }

    IEnumerator Blackout_Transition()
    {
        Color c = Black_out.color;

        // Fade IN
        float duration = 2f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            c.a = Mathf.Lerp(0f, 1f, timer / duration);
            Black_out.color = c;

            yield return null;
        }

        c.a = 1f;
        Black_out.color = c;

        // Fade OUT
        timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            c.a = Mathf.Lerp(1f, 0f, timer / duration);
            Black_out.color = c;

            yield return null;
        }

        c.a = 0f;
        Black_out.color = c;

        // Quando terminar o fade
        Making_Inconsistences();
    }

    void Lower_Inconsistences()
    {
        Inconsistences--;

        if(Inconsistences == 1)
        {
            Possible_Progressions.Remove("Inconsistences");
        }
    }

    void Increase_Quantity()
    {
        Max_Timer++;

        if (Max_Timer == 20)
        {
            Possible_Progressions.Remove("Quantity");
        }
    }

    void Decrease_Time()
    {
        Max_Timer -= 2f;

        if(Max_Timer == 4f)
        {
            Possible_Progressions.Remove("Time");
        }
    }

    void Increase_Ratio()
    {
        switch (Sending_Ratio)
        {
            case 3f: Sending_Ratio = 2f; break;
            case 2f: Sending_Ratio = 1f; break;
            case 1f: Sending_Ratio = 0.8f; break;
            case 0.8f: Sending_Ratio = 0.6f; break;
            case 0.6f: Sending_Ratio = 0.4f; break;
            case 0.4f: Sending_Ratio = 0.3f; Possible_Progressions.Remove("Ratio"); break;
        }
    }

    void Making_Inconsistences()
    {
        int chance = Random.Range(0, 2);

        Fake_Message_Order = new List<string>(Messsage_Order);

        if (chance == 0)
        {
            Correct = false;

            for (int i = 0; i < Inconsistences; i++)
            {
                int type = Random.Range(0, 3);

                switch (type)
                {

                    case 0:

                        if (!Picture_Changed)
                        {
                            Picture_Changed = true;

                            string current = Profile_Picture.sprite.name;
                            string next;

                            do
                            {
                                next = Possible_Contacts[Random.Range(0, Possible_Contacts.Count)];

                            } while (next == current);


                            switch (next)
                            {
                                case "Peixe1": Profile_Picture.sprite = Peixe1; break;
                                case "Peixe2": Profile_Picture.sprite = Peixe2; break;
                                case "Peixe3": Profile_Picture.sprite = Peixe3; break;
                                case "Peixe4": Profile_Picture.sprite = Peixe4; break;
                                case "Peixe5": Profile_Picture.sprite = Peixe5; break;
                                case "Peixe6": Profile_Picture.sprite = Peixe6; break;
                                case "Peixe7": Profile_Picture.sprite = Peixe7; break;
                            }
                        }

                        break;



                    case 1:

                        if (!Name_Changed)
                        {
                            Name_Changed = true;

                            string current = Profile_Name.text;
                            string next;

                            do
                            {
                                next = Possible_Names[Random.Range(0, Possible_Names.Count)];

                            } while (next == current);

                            Profile_Name.text = next;
                        }

                        break;



                    case 2:

                        if (Fake_Message_Order.Count > 0)
                        {
                            int position = Random.Range(0, Fake_Message_Order.Count);

                            string current = Fake_Message_Order[position];

                            string next;

                            do
                            {
                                next = Possible_Figures[Random.Range(0, Possible_Figures.Count)];

                            } while (next == current);

                            Fake_Message_Order[position] = next;
                        }

                        break;
                }
            }
        }

        else
        {
            Correct = true;
        }

        Messages = 0;

        StartCoroutine(Showing_Fake_Order());
    }

    IEnumerator Showing_Fake_Order()
    {
        for (int i = 0; i < Fake_Message_Order.Count; i++)
        {
            string Figure = Fake_Message_Order[i];

            switch (Figure)
            {
                case "Circ":
                    Message_Display.sprite = Circ;
                    break;

                case "Oval":
                    Message_Display.sprite = Oval;
                    break;

                case "Tria":
                    Message_Display.sprite = Tri;
                    break;

                case "Reta":
                    Message_Display.sprite = Ret;
                    break;

                case "Trap":
                    Message_Display.sprite = Trap;
                    break;

                case "Losa":
                    Message_Display.sprite = Los;
                    break;

                case "Quad":
                    Message_Display.sprite = Quad;
                    break;

                case "Pent":
                    Message_Display.sprite = Pent;
                    break;

                case "Hexa":
                    Message_Display.sprite = Hex;
                    break;
            }

            Color c = Message_Display.color;


            c.a = 1;
            Message_Display.color = c;

            yield return new WaitForSeconds(Sending_Ratio / 2f);


            c.a = 0;
            Message_Display.color = c;

            yield return new WaitForSeconds(Sending_Ratio / 2f);
        }

        Is_On_Round = true;
    }

    void Right_Ansher()
    {
        Score++;
        Score_Display.text = Score.ToString();

        if(Possible_Progressions != null)
        {
            string Becoming_Harder = (Possible_Progressions[Random.Range(0, Possible_Progressions.Count)]);

            print($"Aumentar dificuldade: {Becoming_Harder}");

            switch (Becoming_Harder)
            {
                case "Quantity": Increase_Quantity(); break;
                case "Time": Decrease_Time(); break;
                case "Ratio": Increase_Ratio(); break;
                case "Inconsistences": Lower_Inconsistences(); break;
            }
        }

        StartRound();
    }

    void Defeat()
    {
        SceneManager.LoadScene("MenuMiniGames");
    }
}
