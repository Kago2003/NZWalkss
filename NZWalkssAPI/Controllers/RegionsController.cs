using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkssAPI.CustomActionFilters;
using NZWalkssAPI.Data;
using NZWalkssAPI.Models.Domain;
using NZWalkssAPI.Models.DTO;
using NZWalkssAPI.Repositories;
using System.Text.Json;

namespace NZWalkssAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly NZWalkssDbContext dbContext;
        private readonly IRegionRepository regionReporitory;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalkssDbContext dbContext, 
            IRegionRepository regionReporitory, 
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionReporitory = regionReporitory;
            this.mapper = mapper;
            this.logger = logger;
        }

        // Get All Regions.
        [HttpGet]
        //[Authorize (Roles = "Reader, Writer")]
        public async Task <IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom exception");

                //get data from the database
                var regionsDomain = await regionReporitory.GetAllAsync();

                //map domain models to the DTO 
                var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain);


                //Return DTO
                logger.LogInformation($"Finished Get All Regions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(regionsDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

           
        }

        
        //Get Single Region by ID. 
        [HttpGet]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task <IActionResult> GetById([FromRoute] Guid id)
        {
            //Get region domain model from database. 
            var regionDomain = await regionReporitory.GetByIdAsync(id);

            //  var region = dbContext.Reagions.FirstOrDefault(region => region.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //map domain models to the DTO
            var regionsDTO = mapper.Map<RegionDTO>(regionDomain);

            // return the region DTO back to the client.
            return Ok(regionDomain);
        }

        
       // Creating New Region
        //using a POST hhtp verb to create new region 
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult>  Create ([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {

                //convert the dto to domain model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

                //use domain model to asyncronously create a Region 
                regionDomainModel = await regionReporitory.CreateAsync(regionDomainModel);

                //map domain models to the DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);


                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);
        }


        //Updating Region 
        [HttpPost]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task <IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
                //Map DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

                //check if the region exists
                regionDomainModel = await regionReporitory.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Convert domain model to dto
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
                return Ok(regionDTO);
            
        }

        //Deleting Region 
        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionReporitory.DeleteAsync(id);
            
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map the domain Model to the DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);

        }
    }
}
