using System.Net.Mime;
using HealMeAppBackend.API.Doctors.Domain.Model.Queries;
using HealMeAppBackend.API.Doctors.Domain.Services;
using HealMeAppBackend.API.Doctors.Interfaces.REST.Resources;
using HealMeAppBackend.API.Doctors.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace HealMeAppBackend.API.Doctors.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class DoctorsController(
    IDoctorCommandService doctorCommandService,
    IDoctorQueryService doctorQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateDoctor([FromBody] CreateDoctorResource resource)
    {
        var createDoctorCommand = CreateDoctorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await doctorCommandService.Handle(createDoctorCommand);

        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetDoctorById), new { id = result.Id }, DoctorResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    public async Task<ActionResult> GetDoctorByName([FromQuery] string name)
    {
        var getDoctorByNameQuery = new GetDoctorByNameQuery(name);
        var result = await doctorQueryService.Handle(getDoctorByNameQuery);
        if (result is null) return BadRequest();
        var resource = DoctorResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDoctorById(int id)
    {
        var getDoctorByIdQuery = new GetDoctorByIdQuery(id);
        var result = await doctorQueryService.Handle(getDoctorByIdQuery);
        if (result is null) return BadRequest();
        var resource = DoctorResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}

