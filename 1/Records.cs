public class Dados
{
    public List<Venda> vendas { get; set; }

    public Dados()
    {
        vendas = new List<Venda>();
    }
}


public class Venda
{
    public string vendedor { get; set; }
    public decimal valor { get; set; }

    public Venda()
    {
        this.vendedor = "";
        this.valor = 0;
    }
}


public class Registro
{
    public decimal soma { get; set; }
    public decimal comissao { get; set; }

    public Registro()
    {
        this.soma = 0;
        this.comissao = 0;
    }
}