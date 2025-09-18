
var i18n = new Dictionary<string, string>()
{
    { "Pommes","Apples"},
    { "Poires","Pears"},
    { "Pastèques","Watermelons"},
    { "Melons","Melons"},
    { "Noix","Nuts"},
    { "Raisin","Grapes"},
    { "Pruneaux","Plums"},
    { "Myrtilles","Blueberries"},
    { "Groseilles","Berries"},
    { "Tomates","Tomatoes"},
    { "Courges","Pumpkins"},
    { "Pêches","Peaches"},
    { "Haricots","Beans"}
};

List<Product> products = new List<Product>
{
    new Product { Location = 1, Producer = "Bornand", ProductName = "Pommes", Quantity = 20,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Poires", Quantity = 16,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Pastèques", Quantity = 14,Unit = "pièce", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Melons", Quantity = 5,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Noix", Quantity = 20,Unit = "sac", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Raisin", Quantity = 6,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Pruneaux", Quantity = 13,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Myrtilles", Quantity = 12,Unit = "kg", PricePerUnit = 5.50 }
};
/*
Func<Product, string> getProducer = x => $"{x?.Producer?[0]}{x?.Producer?[1]}{x?.Producer?[2]}...{x?.Producer?[x.Producer.Length - 1]}";
Func<Product, string> getAliment = product => i18n[product.ProductName];
Func<Product, double> chiffreAffaire = product => product.Quantity * product.PricePerUnit;

string producerAliment = string.Join("\n", products.Select
    (item => (
    getProducer(item),
    getAliment(item),
    chiffreAffaire(item)
    )
));

Console.WriteLine(producerAliment);
*/

//Aggretateur
int melon = products.Where(p => p.ProductName == "Melons").Sum(p => p.Quantity);

Console.WriteLine($"Quantité totale de Melons : {melon}");

var caParMarchand = products
           .GroupBy(p => p.Producer)
           .Select(g => new
           {
               Marchand = g.Key,
               ChiffreAffaires = g.Sum(p => p.Quantity * p.PricePerUnit)
           });

Console.WriteLine("\nChiffre d'affaires par marchand :");
foreach (var item in caParMarchand)
    Console.WriteLine($"{item.Marchand} : {item.ChiffreAffaires}");

Console.WriteLine(caParMarchand.Max(p => p.ChiffreAffaires));

Func<Product, Product, Product> comparator = (a, b) => a.Quantity > b.Quantity ? a : b;

Console.WriteLine(products.Where(p => p.ProductName == "Noix").Aggregate(comparator).Producer);