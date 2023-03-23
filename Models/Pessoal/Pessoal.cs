namespace SimpleClientServices.Models.Pessoal
{
    public class Pessoal
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DataDeNacimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Setor { get; set; }
    }
}
