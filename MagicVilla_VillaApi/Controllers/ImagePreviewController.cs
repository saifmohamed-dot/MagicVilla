using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagePreviewController : ControllerBase
    {
        readonly IVillaPreviewImagesRepository _imagesRepository;
        readonly APIResponse _aPIResponse;
        readonly IMapper _mapper;
        public ImagePreviewController(IVillaPreviewImagesRepository imagesRepository , IMapper mapper)
        {
            _imagesRepository = imagesRepository;
            _aPIResponse = new APIResponse();
            _mapper = mapper;
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetVillaPreviewImage(int? id)
        {
            if(id == null)
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest , ["Villa Id Can not Be Null"]);
                return BadRequest(_aPIResponse);
            }
            IEnumerable<VillaPreviewImages> images =  await _imagesRepository.FindAllAsync(vpi => vpi.VillaId == id);
            _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.OK, images);
            return Ok(images);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APIResponse>> CreateVillaPreviewImages(IEnumerable<VillaImagesPreviewCreateDto> dtos)
        {
            if(dtos == null || !dtos.Any())
            {
                _aPIResponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest, ["Images Null Or Empty"]);
                return BadRequest(_aPIResponse);
            }
            await _imagesRepository.BulkInsertImages(_mapper.Map<IEnumerable<VillaPreviewImages>>(dtos));
            _aPIResponse.PopulateOnSuccess(System.Net.HttpStatusCode.Created, "Images Created Successfully");
            return Ok(_aPIResponse);
        }
    }
}
