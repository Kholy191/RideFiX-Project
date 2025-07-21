
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    class TechnicianService : ITechnicianService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TechnicianService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<List<FilteredTechniciansDTO>> GetAllTechnicians()
        {
            var AllTechnicians = await unitOfWork.GetRepository<Technician, int>().GetAllAsync();
            var mappedTechnicians = mapper.Map<IEnumerable<Technician>, IEnumerable<FilteredTechniciansDTO>>(AllTechnicians);
            return mappedTechnicians.ToList();

        }

        public async Task<string> GetCity(double latitude, double longitude)
        {
            string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}&zoom=10&addressdetails=1";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName"); // لازم تضيف Header
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

                    string governorate = json.address.state ?? "لم يتم العثور على المحافظة";
                    return governorate;
                }
                return "فشل في الاتصال بالخدمة";
            }
        }

        public async Task<List<FilteredTechniciansDTO>> GetTechniciansByFilterAsync(CreatePreRequestDTO request)
        {
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            string city = await GetCity(request.Latitude, request.Longitude);
            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentException("لم يتم العثور على المدينة بناءً على الإحداثيات المقدمة.");
            }

            var spec = new TechniciansSpecification(currentTime, city, request.categoryId);

            var filteredTechnicians = await unitOfWork.GetRepository<Technician, int>()
                .GetAllAsync(spec);

            var mappedTechnicians = mapper.Map<IEnumerable<Technician>, IEnumerable<FilteredTechniciansDTO>>(filteredTechnicians).ToList();
            return mappedTechnicians;

        }

    }
}
