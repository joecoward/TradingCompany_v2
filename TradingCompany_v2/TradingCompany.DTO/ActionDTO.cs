namespace TradingCompany.DTO
{
    public class ActionDTO
    {
        public int ActionId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusDTO? Status { get; set; }


    }
}
