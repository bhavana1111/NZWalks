using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Repositories;
using AutoMapper;
namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //GET ALL REGIONS
        //GET :https://localhost:1234/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();
            
            //return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);
            //find takes only primary key we can not use find method for other properties 
            //firstOrDefault can be used for any other properties.

            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

           
            //return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        //POST To Create New Region
        //POST :https://localhost:portnumber/api/regions
        [HttpPost]

        public async  Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);
            // Use Domain Model to create region
            regionDomainModel=await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        //Update Region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel =mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel=await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Convert Domain Model to DTO

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();

            }
            //return deleetd region
            //map domain model to DTO
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
