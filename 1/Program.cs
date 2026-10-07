try {
    // leitura de dados
    Dados dados = JsonReader.ler<Dados>("dados.json");

    // itera sobre vendas
    Dictionary<string, Registro> registros = new Dictionary<string, Registro>();

    Console.WriteLine(dados.vendas.Count());

    foreach (Venda vv in dados.vendas){
        // se nao encontrou o vendedor, inicializa seu registro e o poe no dict
        if(!registros.ContainsKey(vv.vendedor))
            registros.Add(vv.vendedor, new Registro());

        /*
            considerei colocar a lógica de registrar soma e calcular comissao
            na propria classe Registro, mas decidi que Registro deveria ser apenas
            um objeto de dados e toda a lógica deve ficar neste arquivo
        */

        // registra soma
        registros[vv.vendedor].soma += vv.valor;

        // calcula comissao
        if(vv.valor < 100)
            registros[vv.vendedor].comissao += 0;
    
        else if(vv.valor < 500)
            registros[vv.vendedor].comissao += vv.valor * 0.01m;

        else 
            registros[vv.vendedor].comissao += vv.valor * 0.05m;
    }


    foreach(KeyValuePair<string, Registro> rr in registros)
    {
        Console.WriteLine(
            $"vendedor: {rr.Key}\n" +
            $"    Total vendido: {rr.Value.soma : 0.00}\n" +
            $"    Comissao: {rr.Value.comissao : 0.00}\n"
        );
    }
}
catch(Exception ex){
    Console.Error.WriteLine("Impossivel processar dados");
    Console.Error.WriteLine(ex.Message);
}

