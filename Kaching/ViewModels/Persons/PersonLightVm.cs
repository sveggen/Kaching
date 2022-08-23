using System.ComponentModel;

namespace Kaching.ViewModels
{
    public class PersonLightVm
    {
        public int PersonId { get; set; }

        [DisplayName("Username")]
        public string? UserName { get; set; }
        
        public string? Avatar { get; set; }

        public string? ColorCode { get; set; }

    }
}