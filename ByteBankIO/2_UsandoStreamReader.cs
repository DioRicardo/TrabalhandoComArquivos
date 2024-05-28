using ByteBankIO;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

partial class Program
{
    static void UsandoStreamReader()
    {
        var enderecoDoArquivo = "contas.txt";

        using (var fluxoDeArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
        {
            var leitor = new StreamReader(fluxoDeArquivo);

            //var linha = leitor.ReadLine();

            //var texto = leitor.ReadToEnd();

            //int numero = leitor.Read();

            while (!leitor.EndOfStream)
            {
                var linha = leitor.ReadLine();
                var contaCorrente = ConverterStringParaContaCorrente(linha);

                var msg = $"Titular: {contaCorrente.Titular.Nome};\n" +
                          $"Conta número: {contaCorrente.Numero};\n" +
                          $"Agência: {contaCorrente.Agencia};\n" +
                          $"Saldo: {contaCorrente.Saldo};\n";
                Console.WriteLine(msg);
            }
        }
        Console.ReadLine();
    }

    static ContaCorrente ConverterStringParaContaCorrente(string linha)
    {
        // 375 4644 2483.13 Jonatan

        var campos = linha.Split(',');

        var agencia = int.Parse(campos[0]);
        var numero = int.Parse(campos[1]);
        var saldoComoDouble = double.Parse(campos[2].Replace('.', ','));
        var titular = new Cliente();

        titular.Nome = campos[3];

        var resultado = new ContaCorrente(agencia, numero);
        resultado.Depositar(saldoComoDouble);
        resultado.Titular = titular;

        return resultado;
    }
}