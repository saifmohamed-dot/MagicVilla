using MagicVilla_Web.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.ViewModels
{
    public class VillaNumberCreateVM
    {
        [Required]
        public VillaNumberCreateDto villaNumberCreateDto { get; set; } = new VillaNumberCreateDto();
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

    }
}
