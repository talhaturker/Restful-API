using MyRestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestApi.Services
{
    public class SparePartService
    {
        private readonly List<SparePart> _spareParts = new();

        public List<SparePart> GetAll() => _spareParts;

        public SparePart GetById(int id) => _spareParts.FirstOrDefault(sp => sp.Id == id);

        public void Add(SparePart sparePart) => _spareParts.Add(sparePart);

        public void Update(SparePart sparePart)
        {
            var existingSparePart = _spareParts.FirstOrDefault(sp => sp.Id == sparePart.Id);
            if (existingSparePart != null)
            {
                existingSparePart.Name = sparePart.Name;
                existingSparePart.Manufacturer = sparePart.Manufacturer;
                existingSparePart.Price = sparePart.Price;
            }
        }

        public void Delete(int id) => _spareParts.RemoveAll(sp => sp.Id == id);
    }
}
