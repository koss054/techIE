namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class UserMProduct
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Required]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(MarketplaceProduct))]
        public int MarketplaceProductId { get; set; }

        [Required]
        public MarketplaceProduct MarketplaceProduct { get; set; } = null!;
    }
}
