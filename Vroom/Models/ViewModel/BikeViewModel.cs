﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vroom.Models.ViewModel
{
    public class BikeViewModel
    {
        public Bike Bike { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }

        private List<Currency> CList=new List<Currency>();
        private List<Currency> CreateList()
        {
            CList.Add(new Currency("USD", "USD"));
            CList.Add(new Currency("INR", "INR"));
            CList.Add(new Currency("EUR", "EUR"));
            return CList;
        }
        public BikeViewModel()
        {
            Currencies=CreateList();
        }

    }
    public class Currency
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Currency(String id,String name)
        {
            Id = id;
            Name = name;
        }
    }
}
