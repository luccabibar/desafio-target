using System.Globalization;

decimal valor, taxa;
DateTime hoje, vencimento;
int dias;

CultureInfo format = new CultureInfo("pt-BR");

string buffer;
bool result;

// ler inputs
taxa = 0.025M;
hoje = DateTime.Now;

Console.WriteLine("Digite o valor do Capital:");
do{
  buffer = Console.ReadLine();
  result = Decimal.TryParse(buffer, out valor);
} while(!result || valor <= 0);
Console.WriteLine("");


Console.WriteLine("Digite a Data de Vencimento (formato dd/mm/aaaa):");
do{
  buffer = Console.ReadLine();
  result = DateTime.TryParse(buffer, format, out vencimento);
} while(!result);
Console.WriteLine("");

// calcla multa
dias = (hoje.Date - vencimento.Date).Days;

if(dias <= 0){
    Console.WriteLine(
        $"R$ {valor}, sob uma taxa de {(taxa * 100):0.00}% Ao Dia, vencendo em {vencimento.Date:dd/MM/yyyy} \n" +
        "Multa de R$ 0.00 (valor ainda não venceu!) \n" +
        $"Na data de hoje ({hoje:dd/MM/yyyy}) \n"
    );
}
else{
    
    decimal simples, composto;

    simples = valor * taxa * dias;
    composto = valor * (Decimal)Math.Pow((Double)(1 + taxa), dias) - valor;

    Console.WriteLine(
        $"R$ {valor}, sob uma taxa de {(taxa * 100):0.00}% Ao Dia, vencendo em {vencimento.Date:dd/MM/yyyy} ({dias} dia(s) atrás) \n" +
        $"Multa de R$ {simples:0.00} sob juros simples \n" +
        $"Multa de R$ {composto:0.00} sob juros composto \n" +
        $"Na data de hoje ({hoje.Date:dd/MM/yyyy}) \n"
    );
}