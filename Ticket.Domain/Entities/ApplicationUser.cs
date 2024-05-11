using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ticket.Domain.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    // ApplicationUser sinfiga xususiyatlar qo'shish
    // orqali ilova foydalanuvchilari uchun profil ma'lumotlarini qo'shing
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(250)]
        [Display(Name = "Profile Picture")]
        public string ProfilePictureUrl { get; set; } = "/images/empty-profile.png";

        [StringLength(250)]
        [Display(Name = "Wallpaper Picture")]
        public string WallpaperPictureUrl { get; set; } = "/images/wallpaper1.jpg";

        public bool IsSuperAdmin { get; set; } = false;
        public bool IsCustomer { get; set; } = false;
        public bool IsSupportAgent { get; set; } = false;
        public bool IsSupportEngineer { get; set; } = false;
    }
}