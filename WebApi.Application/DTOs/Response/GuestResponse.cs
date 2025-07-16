using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;

namespace WebApi.Application.DTOs.Response
{
    public class GuestResponse
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        [Required(ErrorMessage ="Guest Group is required")]
        public string? GuestGroup { get; set; }
        public int Pax { get; set; }
        public string? InvitedBy { get; set; }
        public string? Notes { get; set; }
    }
}