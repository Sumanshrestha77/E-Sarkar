using System.ComponentModel.DataAnnotations;

namespace Esarkar.Models
{
    public class RequestModel
    {
        [Key]
        public int RequestId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
        public string Reason { get; set; }
    }
}
