using MagicVilla_Web.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.ViewModels
{
    public class VillaNumberUpdateVM
    {
        [Required]
        public VillaNumberUpdateDto villaNumberUpdateDto { get; set; } = new VillaNumberUpdateDto();
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
