using ManipulacoesArquivos;
using System.Text;

partial class Program
{
    static void LidandoComFileStreamDiretamente()
    {

        var enderecoDoArquivo = "contas.txt";

        using (var fluxoDoArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
        {
            var leitor = new StreamReader(fluxoDoArquivo);

            //var linha = leitor.ReadLine(); mostra linha a linha

            //var texto = leitor.ReadToEnd() mostra tudo mas carrega tudo de uma vez

            //var numero = leitor.Read(); mostra os primeiros bytes do arquivo em linha

            while (!leitor.EndOfStream)// EndOfStream -> função de leitura do arquivo ao negar no while ele so para quando ler todo o arquivo.
            {
                var linha = leitor.ReadLine();
                var contaCorrente = ConverterStringParaContaCorrente(linha);

                var msg = $"{contaCorrente.Titular.Nome} : Conta número {contaCorrente.Numero}, ag {contaCorrente.Agencia}, Saldo {contaCorrente.Saldo}";

                Console.WriteLine(msg);
            }
        }

        Console.ReadLine();

    }

    static ContaCorrente ConverterStringParaContaCorrente(string linha)
    {
        // 375 4644 2483.13 Jonatan

        var campos = linha.Split(',');

        var agencia = campos[0];
        var numero = campos[1];
        var saldo = campos[2].Replace('.', ',');
        var nomeTitular = campos[3];

        var agenciaComInt = int.Parse(agencia);
        var numeroComInt = int.Parse(numero);
        var saldoComDouble = double.Parse(saldo);

        var titular = new Cliente();
        titular.Nome = nomeTitular;

        var resultado = new ContaCorrente(agenciaComInt, numeroComInt);
        resultado.Depositar(saldoComDouble);
        resultado.Titular = titular;

        return resultado;
    }

}