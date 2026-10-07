using System.Collections.ObjectModel;

// esse metodo pede o dict e o id (ao inves de apenas o item) para poder mostrar tambem o codigo do produto
void printItemEstoque(ReadOnlyDictionary<int, ItemEstoque> estoque, int id)
{
    if(estoque.ContainsKey(id))
        Console.WriteLine(
            $"Código [{id}]: {estoque[id].descricao} \n" +
            $"{estoque[id].quantidade} em estoque \n"
        );
}


// esse metodo pede o dict e o id (ao inves de apenas o item) para poder mostrar tambem o codigo do produto
void printMovimentacao(ReadOnlyCollection<Movimentacao> movimentacoes, int id, ReadOnlyDictionary<int, ItemEstoque> estoque)
{
    if(id >= 0 && id < movimentacoes.Count()){
        string tipoText = movimentacoes[id].tipo == TipoMovimentacao.ENTRADA ? "Entrada" : "Saída";
        
        Console.WriteLine(
            $"Identificador [{id}]: {estoque[ movimentacoes[id].codigoProduto ].descricao}\n" +
            $"{tipoText} de { movimentacoes[id].quantidade } unidade(s) \n"
        );
    }
}


void movimentacaoNova(ref EstoqueManager manager)
{
    // pega referencia ao dict
    ReadOnlyDictionary<int, ItemEstoque> itens = manager.Estoque;
    ReadOnlyCollection<Movimentacao> movimentacoes = manager.Movimentacoes;

    Console.WriteLine("Nova Movimentacao: \n");

    int cod, qtd;
    TipoMovimentacao tipo;
    string buffer;

    // encontra produto
    do{
        Console.WriteLine(
            "Digite o código do produto \n" +
            "ou digite [-1] para cancelar \n");

        // le input int
        do{
            buffer = Console.ReadLine();  
        }while(!int.TryParse(buffer, out cod));
        Console.WriteLine("");

        // resolucao 

        // cancela operacao
        if(cod == -1){
            return;
        }
        // tenta novamente
        else if (!itens.ContainsKey(cod)){
            Console.WriteLine($"Movimentação [{cod}] não identificada");
        }
        // prox passo
        else{
            printItemEstoque(itens, cod);
            break;
        }
    } while(true);

    Console.WriteLine("Tipo de movimentacao: \n");

    Dictionary<string, ConsoleMenuItem> menuMov = new Dictionary<string, ConsoleMenuItem> {
        { "entrada",    new ConsoleMenuItem("Entrada", 10) },
        { "saida",      new ConsoleMenuItem("Saída", 10) },
        { "cancela",    new ConsoleMenuItem("Cancelar", 30) },
    };

    buffer = ConsoleMenu.ActivateMenu(menuMov);

    // designa tipo ou cancela
    switch(buffer){
    case "entrada":
        tipo = TipoMovimentacao.ENTRADA;
        break;

    case "saida":
        tipo = TipoMovimentacao.SAIDA;
        break;

    case "cancela":
    default:
        return;
    }

    // le valor DA MOVIMENTACAO
    Console.WriteLine(
        "Digite a quantidade de itens \n" +
        "ou digite [-1] para cancelar \n");

    // le input int (> 0 e  != -1)
    do{
        buffer = Console.ReadLine();  
    }while(
        !int.TryParse(buffer, out qtd) ||
        (qtd <= 0 && qtd != -1) 
    );

    // tenta realizar movimentacao 
    if(manager.fazerMovimentacao(cod, qtd, tipo)){
        Console.WriteLine("Movimentação realizada com sucesso\n");
        
        // atualiza ref estoque para print
        itens = manager.Estoque;
        printItemEstoque(itens, cod);
    }
    else{
        Console.WriteLine("Impossível realizar saída com quantia maior que a presente no estoque\n");
        printItemEstoque(itens, cod);
    }
}

void movimentacaoLista(EstoqueManager manager)
{
    // pega referencia ao dict
    ReadOnlyDictionary<int, ItemEstoque> itens = manager.Estoque;
    ReadOnlyCollection<Movimentacao> movimentacoes = manager.Movimentacoes;

    Console.WriteLine("Registros de Movimentações: \n");

    if(movimentacoes.Count == 0)
        Console.WriteLine("Nenhuma movimentação realizada \n");

    else
        for(int ii = 0; ii < movimentacoes.Count; ii++)
            printMovimentacao(movimentacoes, ii, itens);
}

void movimentacaoBusca(EstoqueManager manager)
{
    // pega referencia ao dict
    ReadOnlyDictionary<int, ItemEstoque> itens = manager.Estoque;
    ReadOnlyCollection<Movimentacao> movimentacoes = manager.Movimentacoes;

    int inp;
    string buffer;

    do{
        Console.WriteLine(
            "Digite o identificador da movimentação \n"+
            "ou digite [-1] para voltar \n");

        // le input int
        do{
            buffer = Console.ReadLine();  
        }while(!int.TryParse(buffer, out inp));
        Console.WriteLine("");

        // logica de saida  
        if(inp == -1)
            continue;

        else if (inp >= 0 && inp < movimentacoes.Count())
            printMovimentacao(movimentacoes, inp, itens);

        else
            Console.WriteLine(
                $"Código [{inp}] não cadastrado \n"
            );

    } while(inp != -1);
}

// Lista itens no estoque
void estoqueLista(EstoqueManager manager)
{
    // pega referencia ao dict
    ReadOnlyDictionary<int, ItemEstoque> itens = manager.Estoque;

    Console.WriteLine("Registros do Estoque: \n");

    if(itens.Count == 0)
        Console.WriteLine("Nenhum item cadastrado \n");

    else
        foreach (int kk in itens.Keys)
            printItemEstoque(itens, kk);
}

// busac item espeicfico no estoque 
void estoqueBusca(EstoqueManager manager)
{
    // pega referencia ao dict
    ReadOnlyDictionary<int, ItemEstoque> itens = manager.Estoque;

    int inp;
    string buffer;

    do{
        Console.WriteLine(
            "Digite o código do produto para pesquisar \n"+
            "ou digite [-1] para voltar \n");

        // le input int
        do{
            buffer = Console.ReadLine();  
        }while(!int.TryParse(buffer, out inp));
        Console.WriteLine("");

        // logica de saida  
        if(inp == -1)
            continue;

        if (itens.ContainsKey(inp))
            printItemEstoque(itens, inp);

        else
            Console.WriteLine(
                $"Código [{inp}] não cadastrado \n"
            );

    } while(inp != -1);
}


try {
    // leitura de dados
    Dados dados = JsonReader.ler<Dados>("dados.json");

    // carrega os dados em uma estrutura mais conveniente
    EstoqueManager manager = new EstoqueManager(dados.estoque);

    // loop de uso
    string inp;

    // prepara menu
    Dictionary<string, ConsoleMenuItem> menuItens = new Dictionary<string, ConsoleMenuItem> {
        { "movimentacaoNova",       new ConsoleMenuItem("Nova movimentação", 10) },
        { "movimentacaoLista",      new ConsoleMenuItem("Registro de movimentações", 20) },
        { "movimentacaoBusca",      new ConsoleMenuItem("Buscar movimentação", 30) },
        { "estoqueLista",           new ConsoleMenuItem("Registro de estoque", 40) },
        { "estoqueBusca",           new ConsoleMenuItem("Buscar no estoque", 50) },
        { "sair",                   new ConsoleMenuItem("Sair", 60) },
    };

    do{
        Console.WriteLine("GERENCIADOR DE ESTOQUE\n");
        inp = ConsoleMenu.ActivateMenu(menuItens);

        switch (inp){
        case "movimentacaoNova":
            movimentacaoNova(ref manager);            
            break;

        case "movimentacaoLista":
            movimentacaoLista(manager);            
            break;

        case "movimentacaoBusca":
            movimentacaoBusca(manager);            
            break;

        case "estoqueLista":
            estoqueLista(manager);            
            break;

        case "estoqueBusca":
            estoqueBusca(manager);            
            break;

        case "sair":
        default:
            // blank
            break;
        }

    }while(inp != "sair");
}
catch(Exception ex){
    Console.Error.WriteLine("Impossivel processar dados");
    Console.Error.WriteLine(ex.Message);
}

