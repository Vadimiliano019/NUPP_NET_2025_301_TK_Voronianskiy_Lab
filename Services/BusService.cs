using BusStation.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.REST.Services
{
    public class BusService : ICrudServiceAsync<BusModel>
    {
        private readonly List<BusModel> _buses = new List<BusModel>();

        public async Task<bool> CreateAsync(BusModel element)
        {
            _buses.Add(element);
            return await Task.FromResult(true);
        }

        public async Task<BusModel> ReadAsync(Guid id)
        {
            var bus = _buses.FirstOrDefault(b => b.Id == id);
            return await Task.FromResult(bus);
        }

        public async Task<IEnumerable<BusModel>> ReadAllAsync()
        {
            return await Task.FromResult(_buses);
        }

        public async Task<IEnumerable<BusModel>> ReadAllAsync(int page, int amount)
        {
            var paged = _buses.Skip((page - 1) * amount).Take(amount);
            return await Task.FromResult(paged);
        }

        public async Task<bool> UpdateAsync(BusModel element)
        {
            var index = _buses.FindIndex(b => b.Id == element.Id);
            if (index == -1) return await Task.FromResult(false);

            _buses[index] = element;
            return await Task.FromResult(true);
        }

        public async Task<bool> RemoveAsync(BusModel element)
        {
            var removed = _buses.Remove(element);
            return await Task.FromResult(removed);
        }

        public async Task<bool> SaveAsync()
        {
            // Для in-memory немає збереження
            return await Task.FromResult(true);
        }
    }
}
