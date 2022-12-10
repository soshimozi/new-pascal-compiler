namespace NewPascalCompiler.Configuration;

/*
var orderText = @"
date: 2012-08-06

billTo: &id001
street: |
  123 Tornado Alley
  Suite 16
city: East Centerville
state: KS
shipTo: *id001
";

var order = deserializer.Deserialize<Order>(orderText);

Console.WriteLine(order.Date);
*/

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}

public class Order
{
    public DateTime Date { get; set; }
    public Address BillTo { get; set; }
    public Address ShipTo { get; set; }

}