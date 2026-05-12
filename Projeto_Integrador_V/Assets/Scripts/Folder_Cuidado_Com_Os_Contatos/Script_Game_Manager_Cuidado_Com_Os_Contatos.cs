using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Game_Manager_Cuidado_Com_Os_Contatos : MonoBehaviour
{
    bool Correct,
        First_Profile,
        Is_On_Round;

    int Max_Messages,
        Messages, 
        Score,
        Inconsistences;

    float Sending_Ratio, 
        Max_Timer, 
        Current_Timer;

    List<string> Possible_Names = new List<string> { "Ana", "Andr�", "Amanda", "Arthur", "Alice", "Augusto", "Aline", "Adriano", "Alessandra", "Ant�nio",
        "Bruno", "Bianca", "Beatriz", "Bernardo", "B�rbara", "Breno", "Bruna", "Ben�cio", "Bento", "Beto",
        "Carlos", "Camila", "Caio", "Carolina", "C�sar", "Clara", "Cristiano", "C�ntia", "Cau�", "Cl�udio",
        "Daniel", "Daniela", "Diego", "D�bora", "Davi", "Diana", "Douglas", "Denise", "Dalton", "Darlan",
        "Eduardo", "Erika", "Elias", "Elaine", "Enzo", "Ester", "Everton", "Eliane", "Emanoel", "Ellen",
        "Felipe", "Fernanda", "F�bio", "Fl�via", "Francisco", "Fabiana", "Fernando", "F�tima", "Frederico", "Filipe",
        "Gabriel", "Gabriela", "Gustavo", "Giovana", "Guilherme", "Gisele", "Geraldo", "Gl�ria", "Geovane", "Gilberto",
        "Henrique", "Helena", "Hugo", "Helo�sa", "Heitor", "Hadassa", "Higor", "Hilda", "Herbert", "H�lio",
        "Igor", "Isabela", "Ivan", "Ingrid", "�caro", "Iara", "Isaque", "Ivone", "Israel", "Irineu",
        "Jo�o", "Juliana", "Jos�", "J�lia", "Jefferson", "J�ssica", "Jonas", "Jana�na", "Joaquim", "J�nior",
        "Kaio", "Karina", "Kelvin", "Kelly", "Kau�", "K�tia", "Kleber", "Kiara", "Kawan", "Karen",
        "Lucas", "Larissa", "Leonardo", "Luana", "Luiz", "Let�cia", "Leandro", "L�via", "Lorenzo", "L�cio",
        "Marcos", "Maria", "Mateus", "Mariana", "Miguel", "M�rcia", "Murilo", "Mirela", "Marcelo", "Milena",
        "Nicolas", "Nat�lia", "Nelson", "Nicole", "Nathan", "Nayara", "Nataniel", "Neide", "Nivaldo", "Noemi",
        "Ot�vio", "Olivia", "Osvaldo", "Ol�via", "Orlando", "Odete", "Othon", "Olga", "Omar", "Of�lia",
        "Paulo", "Patricia", "Pedro", "Priscila", "Pablo", "Pamela", "Pietro", "Paloma", "Patrick", "Pen�lope",
        "Quirino", "Queila", "Qu�sia", "Quirina", "Quincas", "Quelen", "Qu�sia", "Quirineu", "Quit�ria", "Quiana",
        "Rafael", "Renata", "Rodrigo", "Raquel", "Ricardo", "Rita", "Ramon", "Roberta", "Ruan", "Rosana",
        "Samuel", "Sabrina", "S�rgio", "Simone", "Sandro", "Sara", "Silas", "Sofia", "Saulo", "Sheila",
        "Thiago", "Tatiane", "Tiago", "Tain�", "Tom�s", "Teresa", "T�lio", "Talita", "Theo", "Tereza",
        "Ulisses", "Ubirajara", "Ueliton", "Uilson", "Ueslei", "Uanda", "Urias", "Uelma", "Ugo", "Ualace",
        "Victor", "Vanessa", "Vin�cius", "Vit�ria", "Valter", "V�nia", "Vitor", "Ver�nica", "Vicente", "Viviane",
        "William", "Wagner", "Wesley", "Wanessa", "Willian", "Wendel", "Walace", "Wilma", "Wallyson", "Washington",
        "Xavier", "Ximena", "Xande", "X�nia", "Xisto", "Xuxa", "Xadrez", "Xarleen", "Xaviera", "Ximene",
        "Yuri", "Yasmin", "Yago", "Yara", "Yan", "Yohana", "Ygor", "Yvone", "Yago", "Yandra",
        "Z�", "Zilda", "Zacarias", "Zuleica", "Zeno", "Zara", "Zaqueu", "Zoraide", "Z�lia", "Zoran"},

        Possible_Progressions = new List<string> { "Quantity", "Time", "Ratio", "Inconsistences" };

    public TextMeshProUGUI Profile_Name,
        Score_Display;

    public Sprite Bubble,
        Circ,
        Oval,
        Tri,
        Ret,
        Trap,
        Los,
        Quad,
        Pent,
        Hex;

    public GameObject Player,
        Hook;

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

        StartRound();
        Making_Inconsistences();
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

        Is_On_Round = true;
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

    }

    IEnumerator Sending_Message()
    {

        return new WaitForSecondsRealtime(Sending_Ratio);
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
        for(int i = 0; i < Inconsistences; i++)
        {
            print(i);
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
