namespace POS.Application.Dtos.Provider.Request
{
    public class ProviderRequestDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int DocumentTypeId { get; set; }

        public string DocumentNumber { get; set; } = null!;

        public string? Address { get; set; }

        public string Phone { get; set; } = null!;

        public int State { get; set; }
    }
}
