namespace money_management_service.DTOs.Investment
{
    public class CreateUpdateInvestmentRequestDTO
    {
        public string Name { get; set; }

        public long CurrentUnitPrice { get; set; }
    }
}
