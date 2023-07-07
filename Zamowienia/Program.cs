using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string year = "2014";
        string xml = @"<Customers>
  <Customer>
    <CustomerID>KRAHA</CustomerID>
    <CompanyName>Krakowski Handelek</CompanyName>
    <City>Krakow</City>
    <Country>Poland</Country>
    <Orders></Orders>
  </Customer>
  <Customer>
    <CustomerID>ANATR</CustomerID>
    <CompanyName>Ana Trujillo Emparedados y helados</CompanyName>
    <City>Mexico</City>
    <Country>Mexico</Country>
    <Orders>
      <Order>
        <OrderID>10308</OrderID>
        <OrderDate>2014-09-18T00:00:00</OrderDate>
        <Total>88.80</Total>
      </Order>
    </Orders>
  </Customer>
  <Customer>
    <CustomerID>ANTON</CustomerID>
    <CompanyName>Antonio Moreno Taqueria</CompanyName>
    <City>Rio de Janeiro</City>
    <Country>Brazil</Country>
    <Orders>
      <Order>
        <OrderID>10365</OrderID>
        <OrderDate>2014-11-27T00:00:00</OrderDate>
        <Total>403.20</Total>
      </Order>
      <Order>
        <OrderID>10507</OrderID>
        <OrderDate>2015-04-15T00:00:00</OrderDate>
        <Total>749.06</Total>
      </Order>
    </Orders>
  </Customer>
</Customers>";



        XDocument doc = XDocument.Parse(xml);

        var customers = doc.Root.Elements("Customer")
            .Select(c => new
            {
                CompanyName = c.Element("CompanyName")?.Value,
                Orders = c.Element("Orders")
                    .Elements("Order")
                    .Select(o => new
                    {
                        OrderDate = o.Element("OrderDate")?.Value,
                    })
                    .Where(o => o.OrderDate.StartsWith(year))
                    .ToList()
            })
            .Where(c => c.Orders.Any())
            .ToList();

        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.CompanyName);
            }
        }
        else
        {
            Console.WriteLine("empty");
        }
    }
}
