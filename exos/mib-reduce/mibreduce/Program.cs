

List<Product> products = new List<Product>
{
    new Product { Location = 1, Producer = "Bornand", ProductName = "Pommes", Quantity = 20,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Poires", Quantity = 16,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Pastèques", Quantity = 14,Unit = "pièce", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Melons", Quantity = 5,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Noix", Quantity = 20,Unit = "sac", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Raisin", Quantity = 6,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Pruneaux", Quantity = 13,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Myrtilles", Quantity = 12,Unit = "kg", PricePerUnit = 5.50 },

};


Console.WriteLine(products.Where(x => x.ProductName == "Pommes").Sum(p => p.Quantity));

var chiffreAffaireParProducteur = products
            .GroupBy(p => p.Producer)
            .Select(g => new
            {
                Producteur = g.Key,
                ChiffreAffaire = g.Sum(p => p.Quantity * p.PricePerUnit)
            });

foreach (var item in chiffreAffaireParProducteur)
{
    Console.WriteLine($"{item.Producteur} : {item.ChiffreAffaire}");
}

var revenues = products.Select(p => p.Quantity * p.PricePerUnit);

Console.WriteLine(revenues.Max());
Console.WriteLine(revenues.Min());
Console.WriteLine(revenues.Average());

Console.WriteLine(products.Where(p => p.ProductName == "Noix").Aggregate((a,b) => a.Quantity > b.Quantity ? a:b).Producer);

class Product
{
    public UInt16 Location { get; set; }
    public string? Producer { get; set; }
    public string? ProductName { get; set; }
    public UInt16 Quantity { get; set; }
    public string? Unit { get; set; }
    public double PricePerUnit { get; set; }
}
