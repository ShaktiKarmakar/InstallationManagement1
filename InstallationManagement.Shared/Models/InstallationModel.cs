namespace InstallationManagement.Shared.Models
{
    public class InstallationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CustomerId { get; set; }
        public string? Reference { get; set; }
        public string? InstallationNo { get; set; }
        public DateTime InstallationDate { get; set; }
        public string? MachineModality { get; set; }
        public string? ModelNo { get; set; }
        public string? SerialNo { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string? Engineer { get; set; }
        public string? WarrantyPeriod { get; set; }
        public string? ServiceNo { get; set; }
        public string? JobDetails { get; set; }
        public string? Remarks { get; set; }
    }
}