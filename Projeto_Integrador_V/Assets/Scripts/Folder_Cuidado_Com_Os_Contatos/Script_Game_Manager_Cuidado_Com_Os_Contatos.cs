using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        Current_Timer;

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
        Possible_Contacts = new List<string> { "Peixe1", "Peixe2", "Peixe3", "Peixe4", "Peixe5", "Peixe6", "Peixe7"},
        Possible_Figures = new List<string> { "Circ", "Oval", "Tria", "Reta", "Trap", "Losa", "Quad", "Pent", "Hexa"},
        Messsage_Order = new List<string> { };

    public TextMeshProUGUI Profile_Name,
        Score_Display;

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
        Background;

    public GameObject Player,
        Hook,
        Name_Object,
        Profile_Object;

    Vector3 Hook_Starting_Position, Hook_Ending_Position;
    void Start()
    {
        Hook_Starting_Position = new Vector3(Hook.transform.position.x, Hook.transform.position.y, Hook.transform.position.z);
        Hook_Ending_Position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1f, Player.transform.position.z);

        Max_Timer = 30f;
        Score = 0;
        Inconsistences = 5;
        Max_Messages = 3;
        Messages = 0;
        Sending_Ratio = 3f;
        Is_On_Round = false;
        c = new Color(255f, 255f, 255f, 0f);

        Message_Display.color = c;

        StartRound();
        Making_Inconsistences();

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
            Hook.transform.position = Hook_Starting_Position;
        }
        
    }

    public void StartRound()
    {
        Hook.transform.position = Hook_Starting_Position;
        Profile_Name.text = (Possible_Names[Random.Range(0, Possible_Names.Count)]).ToString();
        Score_Display.text = Score.ToString();
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

            t = Mathf.Clamp01(t);

            t = Mathf.SmoothStep(0f, 1f, t);

            Hook.transform.position = Vector3.Lerp(Hook_Starting_Position, Hook_Ending_Position, t);

            if (Current_Timer <= 0f)
            {
                print("Perdeu");
            }
        }
    }

    public void Other_Profile()
    {

    }

    public void Touching_System()
    {
        Vector2 touchStart, touchEnd;


    }

    public void Sending_The_Message()
    {
        Messages++;

        // Pega uma figura aleatória
        string Figure = Possible_Figures[Random.Range(0, Possible_Figures.Count)];

        // Guarda a ordem
        Messsage_Order.Add(Figure);

        // Escolhe sprite
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

        // Acende
        c.a = 1;
        Message_Display.color = c;

        yield return new WaitForSeconds(Sending_Ratio / 2f);

        // Apaga
        c.a = 0;
        Message_Display.color = c;

        yield return new WaitForSeconds(Sending_Ratio / 2f);

        // Verifica se continua
        if (Messages < Max_Messages)
        {
            Sending_The_Message();
        }
        else
        {
            Is_On_Round = true;
        }
    }

    IEnumerator Next_Profile()
    {

        return new WaitForSecondsRealtime(Sending_Ratio);
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
        if(chance == 0)
        {
            Correct = false;
            for (int i = 0; i < Inconsistences; i++)
            {
                print(i);
            }
        }
        else
        {
            Correct = true;
            Is_On_Round = true;
        }
        
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
    }
}
