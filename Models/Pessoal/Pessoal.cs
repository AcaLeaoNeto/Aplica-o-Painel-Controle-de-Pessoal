namespace SimpleClientServices.Models.Pessoal
{
    public class Pessoal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DataDeNacimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Setor { get; set; }
    }
}
