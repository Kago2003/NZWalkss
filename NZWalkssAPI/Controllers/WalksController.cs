using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkssAPI.CustomActionFilters;
using NZWalkssAPI.Models.Domain;
using NZWalkssAPI.Models.DTO;
using NZWalkssAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalkssAPI.Controllers
{

    //api/walks
    [Route("api/[controller]")]
    [ApiController]

    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalksRepository walksRepository;

        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            this.mapper = mapper;
            this.walksRepository = walksRepository;
        }

        //get all walks 
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomainModel = await walksRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);


            //create an exeption
            throw new Exception("This is a new exception");

            //Map Domain Model to DTO
            var Walk = mapper.Map<List<WalksDTO>>(walkDomainModel);
            return Ok(walkDomainModel);
        }

        //get walk  by id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRepository.GetByIdAsync(id);
            
            if (walkDomainModel == null) 
            {
                return NotFound();
            }

            //Map Domain to DTO
            var Walks = mapper.Map<WalksDTO>(walkDomainModel);
            return Ok(walkDomainModel);
        }

        //creating a new walk public 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addWalkRequstSDTO)
        {
            
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walks>(addWalkRequstSDTO);
                await walksRepository.CreateAsync(walkDomainModel);


                //Map domain model to DTO
                var Walks = mapper.Map<WalksDTO>(walkDomainModel);
                return Ok(walkDomainModel);
        }

        //Updating a walk by id
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walks>(updateWalkRequestDTO);
                walkDomainModel = await walksRepository.UpdateAsync(id, walkDomainModel);


                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //Map Domain model to DTO
                var Walks = mapper.Map<WalksDTO>(walkDomainModel);
                return Ok(walkDomainModel);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Map DTO to Domain Model
            var deletedWalksDomainModel = await walksRepository.DeleteAsync(id);
            if(deletedWalksDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            var Walks = mapper.Map<WalksDTO>(deletedWalksDomainModel);
            return Ok(deletedWalksDomainModel);
        }
    } 
}
