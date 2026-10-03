namespace Práctica_Controller_Service_Repository.Models.DTO.Reponses
{
    public class ProductStatsDto
    {
        public int Total { get; set;}
        public decimal AveragePrice { get; set; }
        public string MostExpensiveName { get; set;}
    }
}
