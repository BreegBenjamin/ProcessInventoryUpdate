using System.ComponentModel.DataAnnotations;
namespace ProcessInventoryUpdate.Model
{
    public class ProccessInventoryModel
    {
        [Required]
        public Guid MessageId { get; set; }

        [Required]
        public Guid productId { get; set; }

        [Required]
        [StringLength(30)]
        public string? Sku { get; set; }

        [Required]
        [StringLength(30)]
        public string? Location { get; set; }

        [Required]
        public int Change { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        [StringLength(100)]
        public string? TriggeredBy { get; set; }
    }
}
