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
            var _categories = File.OpenRead(@"..\Presistence\Data\DataSeed\Users.json");
            var categories = await JsonSerializer.DeserializeAsync<List<TCategory>>(_categories);
            foreach (var item in categories)
            {
                await _context.AddAsync(item);
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


                if (!await _context.ProductBrands.AnyAsync())
                {
                    var productBrands = File.OpenRead(@"..\Presistence\Data\DataSeed\brands.json");
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrands);
                    if (brands != null)
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                    }
                }

                if (!await _context.ProductTypes.AnyAsync())
                {
                    var productTypes = File.OpenRead(@"..\Presistence\Data\DataSeed\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                    if (types != null)
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                    }
                }

                if (!await _context.Products.AnyAsync())
                {
                    var productsData = File.OpenRead(@"..\Presistence\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products != null)
                    {
                        await _context.Products.AddRangeAsync(products);
                    }
                }

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

                    var Counter = 0;
                    foreach (var user in usersData)
                    {
                        var Result = await _userManager.CreateAsync(user, "password@1234");
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
                                Result = await _context.CarOwners.AddAsync(new CarOwner
                                {
                                    UserId = user.Id,
                                    Name = user.Name,
                                    Address = user.Address,
                                    BirthDate = user.BirthDate,
                                });
                                break;
                            case 2:
                                Result = await _userManager.AddToRoleAsync(user, "Technician");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding identity data: " + ex.Message, ex);
            }
        }
    }
}
