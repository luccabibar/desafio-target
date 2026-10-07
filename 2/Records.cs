public class Dados
{
    public List<Produto> estoque { get; set; }

    public Dados()
    {
        estoque = new List<Produto>();
    }
}


public class Produto
{
    public int codigoProduto { get; set; } 
    public string descricaoProduto { get; set; } 
    public int estoque { get; set; } 


    public Produto()
    {
        codigoProduto = 0;
        descricaoProduto = "";
        estoque = 0;
    }
}