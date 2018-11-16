
namespace Lands.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain a maximum of {1} characters lenght.")]
        [Index("UserType_Name_Index", IsUnique = true)]
        public string Name { get; set; }
    }
}
