using System.ComponentModel.DataAnnotations;

namespace Cookie_Based_Authentication.Model
{
    public class Credential
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Rememner Me")]
        public bool RememberMe { get; set; }
    }
}
