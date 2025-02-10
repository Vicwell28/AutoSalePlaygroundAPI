﻿namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    public class SpecificationsDto : IDto
    {
        public string FuelType { get; set; } = string.Empty;
        public int EngineDisplacement { get; set; }
        public int Horsepower { get; set; }
    }
}