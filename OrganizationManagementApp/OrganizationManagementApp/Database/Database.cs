using Starcounter;

[Database]
public class Organization
{
    public QueryResultRows<Company> CompanyList => Db.SQL<Company>("SELECT c From Company c");
}

[Database]
public class Company
{
    public string Name;
    public int CompanyNo;
    public QueryResultRows<Person> PersonList => 
        Db.SQL<Person>("SELECT p FROM Person p WHERE NOT p.PersonName=? AND p.CompanyNo=?", string.Empty, this.CompanyNo);
}

[Database]
public class Person
{
    public int CompanyNo;
    public int PersonNo;
    public string PersonName;
    public string Street;
    public string City;
    public int Zip;
    public string Country;
    public string Email;
    public decimal TotalSale => Db.SQL<long>("SELECT COUNT(s.Saler) FROM Sales s WHERE s.Saler=?", this).First;
    public decimal TotalCommission => Db.SQL<decimal>("SELECT SUM(s.Commission) FROM Sales s WHERE s.Saler=?", this).First;
    public QueryResultRows<Sales> SalesRecord => Db.SQL<Sales>("SELECT s FROM Sales s WHERE s.Saler=?", this);

    public decimal AvgCommission
    {
        get
        {
            return TotalSale != 0 ? TotalCommission / TotalSale : 0 ;
        }
    }
}

[Database]
public class Sales
{
    public Person Saler;
    public string Street;
    public string City;
    public int Zip;
    public string Country;
    public decimal Price;
    public decimal Commission;
    public string Date;
    public string Address => Street + ", " + Zip +" "+ City + " " + Country;
}
