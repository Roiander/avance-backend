using HealMeAppBackend.API.Doctors.Domain.Model.Commands;
using HealMeAppBackend.API.Doctors.Interfaces.REST.Resources;

namespace HealMeAppBackend.API.Doctors.Interfaces.REST.Transform
{
    public static class CreateDoctorCommandFromResourceAssembler
    {
        public static CreateDoctorCommand ToCommandFromResource(CreateDoctorResource resource) =>
            new CreateDoctorCommand(resource.Name, resource.Description, resource.Rating);
    }
}
