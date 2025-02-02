using System.ComponentModel.DataAnnotations;

namespace UserProfiles_API.DataTransferObjects
{
    public class UserProfileCreateDto
    {
        [Required]
        [StringLength(35)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
