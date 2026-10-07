public static class GameManager
{
    public static int Pontos;
    public static int Estrelas;
    public static int TotalEstrelas;
    public static int VidasRestantes;
    public static float TempoFinal;
    public static bool Vitoria;

    public static void Reiniciar()
    {
        Pontos = 0;
        Estrelas = 0;
        TotalEstrelas = 0;
        VidasRestantes = 0;
        TempoFinal = 0f;
        Vitoria = false;
    }

    public static string FormatarTempo(float segundos)
    {
        int total = UnityEngine.Mathf.Max(0, UnityEngine.Mathf.CeilToInt(segundos));
        return (total / 60).ToString("00") + ":" + (total % 60).ToString("00");
    }
}
