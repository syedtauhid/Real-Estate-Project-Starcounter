using Starcounter;

[Database]
public class Organization
{
    public QueryResultRows<Company> CompanyList = Db.SQL<Company>("SELECT c From Company c");
}

[Database]
public class Company
{
    public string Name;
    public QueryResultRows<Person> PersonList => Db.SQL<Person>("SELECT p FROM Person p WHERE p.Company=?", this);
}

[Database]
public class Person
{
    public Company Company;
    public string Street;
    public string City;
    public string Country;
    public string Email;
    public int TotalSale => Db.SQL<int>("SELECT COUNT(s.Saler) FROM Sales s WHERE s.Saler=?", this).First;
    public decimal TotalCommission => Db.SQL<decimal>("SELECT SUM(s.Commission) FROM Sales s WHERE s.Saler=?", this).First;
    public QueryResultRows<Sales> Sales => Db.SQL<Sales>("SELECT s FROM Sales s WHERE s.Saler=?", this);

    public decimal AvgCommission
    {
        get
        {
            return TotalCommission / TotalSale;
        }
    }
}

[Database]
public class Sales
{
    public Person Saler;
    public string Street;
    public string City;
    public string Country;
    public decimal Price;
    public decimal Commission;
    public string Date;
}

//[Database]
//public class Invoice {
//    public int InvoiceNo;
//    public string Name;
//    public decimal Total {
//        get {
//            return Db.SQL<decimal>("SELECT sum(r.Total) FROM InvoiceRow r WHERE r.Invoice=?", this).First;
//        }
//    }
//    public QueryResultRows<InvoiceRow> Items {
//        get {
//            return Db.SQL<InvoiceRow>("SELECT r FROM InvoiceRow r WHERE r.Invoice=?", this);
//        }
//    }

//    public Invoice() {
//        new InvoiceRow() {
//          Invoice = this
//        };
//    }
//}

//[Database]
//public class InvoiceRow {
//    public Invoice Invoice;
//    public string Description;
//    public int Quantity;
//    public decimal Price;
//    public decimal Total {
//        get {
//            return Quantity * Price;
//        }
//    }

//    public InvoiceRow() {
//        Quantity = 1;
//    }
//}
