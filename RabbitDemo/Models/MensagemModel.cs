namespace RabbitDemo.Models
{
    public class MensagemModel
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Destino { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
