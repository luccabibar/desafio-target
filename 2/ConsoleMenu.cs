public static class ConsoleMenu
{
    public static string ActivateMenu(Dictionary<string, ConsoleMenuItem> itens)
    {
        if(itens.Count == 0)
            return "";

        // ordena chaves de acordo com o indice indicado no MenuItem
        List<string> chavesOrdenadas = [.. itens.Keys];
        chavesOrdenadas.Sort(
            (aa, bb) => itens[aa].indice - itens[bb].indice
        );

        // usando intervalo de 1 a N

        // imprime menu ordenadamente
        for (int ii = 0; ii < chavesOrdenadas.Count; ii++){
            Console.WriteLine($"[{ii + 1}] { itens[chavesOrdenadas[ii]].texto }");   
        }

        Console.WriteLine(
            "\n" +
            "Selecione uma opção digitando um número\n"
        );

        // le resposta
        int inp;
        string buffer;

        // le ate uma resposta valida
        do{
            buffer = Console.ReadLine();
            int.TryParse(buffer, out inp);
        }while(inp < 1 || inp > itens.Count);
        Console.WriteLine("");


        // -1 porque usando intervalo de 1 a N
        return chavesOrdenadas[inp - 1];
    }
}


public class ConsoleMenuItem
{
    public string texto { get; set; }
    public int indice { get; set; }


    public ConsoleMenuItem(string texto, int indice)
    {
        this.texto = texto;
        this.indice = indice;
    }
}