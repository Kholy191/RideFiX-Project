using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence
{
    public class DataSeeding : IDataSeeding
    {
        readonly ApplicationDbContext _context;
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeding(ApplicationDbContext dbcontext, UserManager<ApplicationUser> _usermanager, RoleManager<IdentityRole> _rolemanager)
        {
            _context = dbcontext;
            _userManager = _usermanager;
            _roleManager = _rolemanager;
        }

        public async Task SeedCategories()
        {
            var Res = await _context.categories.AnyAsync();
            if (!Res)
            {
                var _categories = File.OpenRead(@"..\Presistence\Data\DataSeed\Category.json");
                var categories = await JsonSerializer.DeserializeAsync<List<TCategory>>(_categories);
                foreach (var item in categories)
                {
                    await _context.AddAsync(item);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedDataAsync()
        {

            try
            {
                if ((await _context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _context.Database.MigrateAsync();
                }

                if (!await _context.chatSessions.AnyAsync())
                {
                    var chat = File.OpenRead(@"..\Presistence\Data\DataSeed\ChatSession.json");
                    var Chatsession = await JsonSerializer.DeserializeAsync<List<ChatSession>>(chat);
                    if (Chatsession != null)
                    {
                        await _context.chatSessions.AddRangeAsync(Chatsession);
                    }
                    await _context.SaveChangesAsync();

                }

                if (!await _context.emergencyRequests.AnyAsync())
                {
                    var requestsfile = File.OpenRead(@"..\Presistence\Data\DataSeed\EmergencyRequest.json");
                    var requests = await JsonSerializer.DeserializeAsync<List<EmergencyRequest>>(requestsfile);
                    if (requests != null)
                    {
                        await _context.emergencyRequests.AddRangeAsync(requests);
                    }
                    await _context.SaveChangesAsync();

                }

                if (!await _context.messages.AnyAsync())
                {
                    var Messagesfile = File.OpenRead(@"..\Presistence\Data\DataSeed\Messages.json");
                    var _messages = await JsonSerializer.DeserializeAsync<List<Message>>(Messagesfile);
                    var CarOwner = await _context.carOwners.FirstOrDefaultAsync();
                    var Technician = await _context.technicians.FirstOrDefaultAsync();
                    if (_messages != null && CarOwner != null && Technician != null)
                    {
                        _messages[0].ApplicationId = CarOwner.ApplicationUserId;
                        _messages[1].ApplicationId = Technician.ApplicationUserId;
                        _messages[0].ChatSessionId = _context.chatSessions.FirstOrDefault()!.Id;
                        _messages[1].ChatSessionId = _context.chatSessions.FirstOrDefault()!.Id;
                    }
                    if (_messages != null)
                    {
                        await _context.messages.AddRangeAsync(_messages);
                    }
                    await _context.SaveChangesAsync();

                }

                if (!await _context.reviews.AnyAsync())
                {
                    var reviewsFiles = File.OpenRead(@"..\Presistence\Data\DataSeed\Reviews.json");
                    var Reviews = await JsonSerializer.DeserializeAsync<List<Review>>(reviewsFiles);
                    if (Reviews != null)
                    {
                        await _context.reviews.AddRangeAsync(Reviews);
                    }
                }



                //if (!await _context.ProductTypes.AnyAsync())
                //{
                //    var productTypes = File.OpenRead(@"..\Presistence\Data\DataSeed\types.json");
                //    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                //    if (types != null)
                //    {
                //        await _context.ProductTypes.AddRangeAsync(types);
                //    }
                //}

                //if (!await _context.Products.AnyAsync())
                //{
                //    var productsData = File.OpenRead(@"..\Presistence\Data\DataSeed\products.json");
                //    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                //    if (products != null)
                //    {
                //        await _context.Products.AddRangeAsync(products);
                //    }
                //}

                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                // Notify the error to the user or log it
            }
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!await _context.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("CarOwner"));
                    await _roleManager.CreateAsync(new IdentityRole("Technician"));
                }
                if (!await _context.Users.AnyAsync())
                {
                    var users = File.OpenRead(@"..\Presistence\Data\DataSeed\Users.json");
                    var usersData = await JsonSerializer.DeserializeAsync<List<ApplicationUser>>(users);

                    var stream = File.OpenRead(@"..\Presistence\Data\DataSeed\Tech.json");
                    var options = new JsonSerializerOptions
                    {
                        Converters = { new TimeOnlyJsonConverter() }
                    };
                    var techniciansData = await JsonSerializer.DeserializeAsync<List<Technician>>(stream, options);


                    var Counter = 0;
                    foreach (var user in usersData)
                    {
                        var Result = await _userManager.CreateAsync(user, "Password@123");
                        if (!Result.Succeeded)
                        {
                            throw new Exception("Failed to create users: " + string.Join(", ", Result.Errors.Select(e => e.Description)));
                        }
                        switch (Counter)
                        {
                            case 0:
                                Result = await _userManager.AddToRoleAsync(user, "Admin");
                                break;
                            case 1:
                                Result = await _userManager.AddToRoleAsync(user, "CarOwner");
                                await _context.carOwners.AddAsync(new CarOwner
                                {
                                    ApplicationUserId = user.Id,
                                });
                                break;
                            case 2:
                                Result = await _userManager.AddToRoleAsync(user, "Technician");
                                List<TCategory> categories = new List<TCategory>();
                                categories.Add(_context.categories.FirstOrDefault(c => c.Name == "عفشجي")!);
                                await _context.technicians.AddAsync(new Technician
                                {
                                    ApplicationUserId = user.Id,
                                    StartWorking = techniciansData[0].StartWorking,
                                    EndWorking = techniciansData[0].EndWorking,
                                    Description = techniciansData[0].Description,
                                    TCategories = categories,
                                });
                                break;
                        }
                        Counter++;
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding identity data: " + ex.Message, ex);
            }
        }
    }
}
