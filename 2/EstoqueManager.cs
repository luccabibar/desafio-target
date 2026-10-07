using System.Collections.ObjectModel;

public class EstoqueManager
{
    // campos
    private Dictionary<int, ItemEstoque> estoque; 
    private List<Movimentacao> movimentacoes; 

    // propriedades
    public ReadOnlyDictionary<int, ItemEstoque> Estoque
    { get => new ReadOnlyDictionary<int, ItemEstoque>(estoque); }

    public ReadOnlyCollection<Movimentacao> Movimentacoes
    { get => new ReadOnlyCollection<Movimentacao>(movimentacoes); } 


    public EstoqueManager(List<Produto> produtos)
    {
        // prepara estoque
        this.estoque = new Dictionary<int, ItemEstoque>();
        foreach(Produto pp in produtos)
            estoque.Add(pp.codigoProduto, new ItemEstoque(pp));

        this.movimentacoes = new List<Movimentacao>();
    }


    public bool fazerMovimentacao(int item, int qtd, TipoMovimentacao tipo)
    {
        if(!estoque.ContainsKey(item))
            return false;

        if(qtd <= 0)
            return false;

        if(tipo == TipoMovimentacao.SAIDA)
            // verifica se saida eh possivel
            if(estoque[item].quantidade < qtd)
                return false;
            
            // substitui registro com novo valor (saida)
            else
                estoque[item] = new ItemEstoque(estoque[item].descricao, estoque[item].quantidade - qtd);

        // substitui registro com novo valor (entrada)
        else
            estoque[item] = new ItemEstoque(estoque[item].descricao, estoque[item].quantidade + qtd);

        // registra mov
        movimentacoes.Add(new Movimentacao(item, qtd, tipo));

        return true;
    }
}

// Referentes ao ESTOQUE
public class ItemEstoque
{
    public string descricao { get; private set; } 
    public int quantidade { get; private set; } 


    public ItemEstoque(Produto pp)
    {
        descricao = pp.descricaoProduto;
        quantidade = pp.estoque;
    }

    public ItemEstoque(string descricao, int quantidade)
    {
        this.descricao = descricao;
        this.quantidade = quantidade;
    }
}

// Referentes as MOVIMENTACOES
public class Movimentacao
{
    public int codigoProduto { get; private set; }
    public int quantidade { get; private set; }
    public TipoMovimentacao tipo { get; private set; }


    public Movimentacao(int codigoProduto, int quantidade, TipoMovimentacao tipo)
    {
        this.codigoProduto = codigoProduto;
        this.quantidade = quantidade;
        this.tipo = tipo;
    }
}


public enum TipoMovimentacao
{
    ENTRADA,
    SAIDA
}