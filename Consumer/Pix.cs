namespace Consumer;

public class Pagador
{
    public string Nome { get; set; }
    public string Banco { get; set; }
    public string Conta { get; set; }
    public string Agencia { get; set; }
    public string CpfCnpj { get; set; }
    public string TipoConta { get; set; }
    public string TipoPessoa { get; set; }
}

public class ComponenteValor
{
    public string Original { get; set; }
}

public class Pix
{
    public string TxId { get; set; }
    public string Chave { get; set; }
    public decimal Valor { get; set; }
    public DateTime Horario { get; set; }
    public Pagador Pagador { get; set; }
    public List<string> Devolucoes { get; set; }
    public string EndToEndId { get; set; }
    public string InfoPagador { get; set; }
    public ComponenteValor ComponentesValor { get; set; }
}

public class Payload
{
    public List<Pix> Pix { get; set; }
}
