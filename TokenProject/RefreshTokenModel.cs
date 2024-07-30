using System.ComponentModel.DataAnnotations;

namespace TokenProject
{
    public class RefreshTokenModel
    {
            [Required]
            public string RefreshToken { get; set; }
        
    }
}
