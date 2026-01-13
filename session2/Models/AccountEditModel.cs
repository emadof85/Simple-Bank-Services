using System.ComponentModel.DataAnnotations;

namespace session2.Models
{
    public class AccountEditModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
    }

}
