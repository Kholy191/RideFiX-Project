﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServiceAbstraction.CoreServicesAbstractions.Account;
using SharedData.DTOs.Account;

namespace Service.CoreServices.Account
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IFileService _fileService;
        private readonly IFaceRecognitionService _faceRecognitionService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;


        public AuthService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMemoryCache memoryCache , IFileService fileService , IFaceRecognitionService faceRecognitionService , IMapper mapper , IWebHostEnvironment env , IJwtService jwtService)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;
            this._fileService = fileService;
            this._faceRecognitionService = faceRecognitionService;
            this._mapper = mapper;
            _env = env;
            this._jwtService = jwtService;
            _unitOfWork = unitOfWork;

        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password)) 
                return null;
            var roles = await _userManager.GetRolesAsync(user);

            return _jwtService.generateToken(user , roles);
        }

        public async Task<IdentityResult> RegisterStep1Async(RegisterStep1Dto dto)
        {
            var ExistingEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (ExistingEmail is not null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "EmailExists",
                    Description = "Email is already registered."
                });

            }
            if (dto.Role == "Technician")
            {
                var errors = new List<IdentityError>();
                if (dto.StartWorking is null)
                {
                    errors.Add(new IdentityError()
                    {
                        Code = "StartWorkingRequired",
                        Description = "StartWorking is required for Technicain"

                    });
                }

                if (dto.EndWorking is null)
                    errors.Add(new IdentityError { Code = "EndWorkingRequired", Description = "EndWorking is required for technicians." });

                if (string.IsNullOrWhiteSpace(dto.Description))
                    errors.Add(new IdentityError { Code = "DescriptionRequired", Description = "Description is required for technicians." });

                if (errors.Any())
                    return IdentityResult.Failed(errors.ToArray());

            }
            _memoryCache.Set($"register_{dto.Email}", dto, TimeSpan.FromMinutes(10));

            return IdentityResult.Success;


        }

        public async Task<IdentityResult> RegisterStep2Async([FromForm]     RegisterStep2Dto dto)
        {
            if (!_memoryCache.TryGetValue($"register_{dto.Email}", out RegisterStep1Dto step1Dto))
            {
                return  IdentityResult.Failed(new IdentityError
                {
                    Code = "Step1Expired",
                    Description = "Registration Step 1 data not found or expired."
                });
            }
            if (dto.IdentityImage == null || dto.IdentityImage.Length == 0)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "MissingIdentityImage",
                    Description = "Identity image is required."
                });
            }

            if (dto.FaceImage == null || dto.FaceImage.Length == 0)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "MissingFaceImage",
                    Description = "Face image is required."
                });
            }

            var identityImageUrl = await _fileService.SaveFileAsync( dto.IdentityImage , "Identity");
            var faceImageUrl = await _fileService.SaveFileAsync(dto.FaceImage, "Faces");

            var identityFullPath = Path.Combine(_env.WebRootPath, identityImageUrl.TrimStart('/'));
            var faceFullPath = Path.Combine(_env.WebRootPath, faceImageUrl.TrimStart('/'));

            using var identityStream = new FileStream(identityFullPath, FileMode.Open, FileAccess.Read);
            using var faceStream = new FileStream(faceFullPath, FileMode.Open, FileAccess.Read);

            var isMatching = await _faceRecognitionService.AreFacesMatchingAsync(identityStream, faceStream);
            if (!isMatching) {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "FaceMismatch",
                    Description = "Face doesn't match ID image."
                });
            }
            var user = _mapper.Map<ApplicationUser>(step1Dto);
            user.IdentityImageUrl = identityImageUrl;
            user.FaceImageUrl = faceImageUrl;

            var result = await _userManager.CreateAsync(user , step1Dto.Password);
            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, step1Dto.Role);

            if (step1Dto.Role == "CarOwner") {
                var carowner = new CarOwner() {ApplicationUserId = user.Id };
                // add in database
                await _unitOfWork.GetRepository<CarOwner, string>().AddAsync(carowner);

            }
            else if (step1Dto.Role == "Technician")
            {
                var tech = new Technician
                {
                    ApplicationUserId = user.Id,
                    StartWorking = step1Dto.StartWorking.Value,
                    EndWorking = step1Dto.EndWorking.Value,
                    Description = step1Dto.Description
                };
                //add in database
                await _unitOfWork.GetRepository<Technician, string>().AddAsync(tech);


            }

            //save in dataase
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Remove(dto.Email);

            return IdentityResult.Success;


        }
    }
}
