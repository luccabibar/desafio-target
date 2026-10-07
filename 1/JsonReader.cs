using System.Text.Json;

public static class JsonReader
{
    public static TT ler<TT>(string caminho)
    {
        string texto = File.ReadAllText(caminho);
        TT obj = JsonSerializer.Deserialize<TT>(texto);

        return obj;
    }
}