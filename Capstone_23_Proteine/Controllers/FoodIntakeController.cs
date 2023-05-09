﻿using Microsoft.AspNetCore.Mvc;
using Capstone_23_Proteine.Models;
using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Models.Domain;

namespace Capstone_23_Proteine.Controllers
{
    public class FoodIntakeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public FoodIntakeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult FoodIntake()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FoodIntake(FoodIntakeViewModel foodIntakeRequest)
        {
            var foodIntake = new FoodIntake()
            {
                ID = Guid.NewGuid(),
                Protein = foodIntakeRequest.Protein,
                Calories = foodIntakeRequest.Calories,
                Fat = foodIntakeRequest.Fat,
                MealName = foodIntakeRequest.MealName,
                Date = foodIntakeRequest.Date,

            };

            await applicationDbContext.FoodIntake.AddAsync(foodIntake);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("FoodIntake");

        }
    }
}