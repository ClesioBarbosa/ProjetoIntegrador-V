using UnityEngine;
using TMPro;

public class PlayerCustomization : MonoBehaviour
{
    [Header("Modelos (GameObjects)")]
    public GameObject[] modelosPersonagens;
    public GameObject[] modelosChapeus;

    [Header("Nomes para a UI")]
    public string[] nomesPersonagens;
    public string[] nomesChapeus;

    [Header("Componentes de UI (TMP)")]
    public TextMeshProUGUI txtNomePersonagem;
    public TextMeshProUGUI txtNomeChapeu;

    // Índices atuais (o que está aparecendo na tela de seleção)
    private int indexPersonagemAtual = 0;
    private int indexChapeuAtual = 0;

    // Índices confirmados (o que foi salvo de fato)
    private int personagemConfirmado = 0;
    private int chapeuConfirmado = 0;

    private void Start()
    {
        // Carrega as escolhas salvas anteriormente (padrão é 0 se for a primeira vez)
        personagemConfirmado = PlayerPrefs.GetInt("PersonagemEscolhido", 0);
        chapeuConfirmado = PlayerPrefs.GetInt("ChapeuEscolhido", 0);

        // Define os índices visuais iniciais como os já confirmados
        indexPersonagemAtual = personagemConfirmado;
        indexChapeuAtual = chapeuConfirmado;

        // Atualiza a cena visualmente
        AtualizarVisual();
    }

    // --- SELEÇÃO DE PERSONAGEM ---
    public void MudarPersonagem(int direcao)
    {
        // Altera o índice e faz o "loop" se passar do limite da array
        indexPersonagemAtual += direcao;
        if (indexPersonagemAtual < 0) indexPersonagemAtual = modelosPersonagens.Length - 1;
        if (indexPersonagemAtual >= modelosPersonagens.Length) indexPersonagemAtual = 0;

        AtualizarVisual();
    }

    public void ConfirmarPersonagem()
    {
        personagemConfirmado = indexPersonagemAtual;
        PlayerPrefs.SetInt("PersonagemEscolhido", personagemConfirmado);
        PlayerPrefs.Save();
        Debug.Log("Personagem confirmado e salvo: " + nomesPersonagens[personagemConfirmado]);
    }

    // --- SELEÇÃO DE CHAPÉU ---
    public void MudarChapeu(int direcao)
    {
        indexChapeuAtual += direcao;
        if (indexChapeuAtual < 0) indexChapeuAtual = modelosChapeus.Length - 1;
        if (indexChapeuAtual >= modelosChapeus.Length) indexChapeuAtual = 0;

        AtualizarVisual();
    }

    public void ConfirmarChapeu()
    {
        chapeuConfirmado = indexChapeuAtual;
        PlayerPrefs.SetInt("ChapeuEscolhido", chapeuConfirmado);
        PlayerPrefs.Save();
        Debug.Log("Chapéu confirmado e salvo: " + nomesChapeus[chapeuConfirmado]);
    }

    // --- ATUALIZAÇÃO VISUAL ---
    private void AtualizarVisual()
    {
       Debug.Log("Peixe Atual: " + indexPersonagemAtual);
    Debug.Log("Chapéu Atual: " + indexChapeuAtual);

    for (int i = 0; i < modelosPersonagens.Length; i++)
    {
        modelosPersonagens[i].SetActive(i == indexPersonagemAtual);
    }

    for (int i = 0; i < modelosChapeus.Length; i++)
    {
        modelosChapeus[i].SetActive(i == indexChapeuAtual);
    }
    }
}