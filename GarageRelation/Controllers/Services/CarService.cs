﻿using GarageRelation.Controllers.Repositories;
using GarageRelation.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageRelation.Controllers.Services
{
    public interface ICarService
    {
        Task<IQueryable<Car>> GetAllCarsAsync();
        Task<Car?> GetCarByIdAsync(int id);
        Task<Car> SaveCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(Car car);
    }

    public class CarService : ICarService
    {
        private readonly MySqlRepository repository;

        public CarService(MySqlRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IQueryable<Car>> GetAllCarsAsync()
        {
            return await Task.FromResult(repository.Car);
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await repository.Car.SingleOrDefaultAsync(car => car.Id == id);
        }

        public async Task<Car> SaveCarAsync(Car car)
        {
            var createdCar = await repository.Car.AddAsync(car);
            await repository.SaveChangesAsync();
            return createdCar.Entity;
        }

        public async Task UpdateCarAsync(Car car)
        {
            repository.Car.Update(car);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(Car car)
        {
            repository.Car.Remove(car);
            await repository.SaveChangesAsync();
        }
    }
}
