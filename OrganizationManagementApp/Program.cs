using System;
using Starcounter;
using OrganizationManagementApp.view_model;

namespace OrganizationManagementApp
{
    class Program
    {
        static void Main()
        {
            //Db.Transact(() => {
            //   var person = Db.SQL<Company>("select c from Company c").First;
            //   if (person == null)
            //    {
            //        new Company
            //        {
            //            Name = "Tauhid Ahmed",
            //            CompanyNo = 1
            //        };
            //    }
            //});

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());


            Handle.GET("/", () => {
                MasterPage master;

                if (Session.Current != null && Session.Current.Data != null)
                {
                    master = (MasterPage)Session.Current.Data;
                }
                else
                {
                    master = new MasterPage();

                    master.Html = "/OrganizationManagementApp/MasterPage.html";
                    master.Session = new Session(SessionOptions.PatchVersioning);
                    

                    master.RecentInvoices = new CompanyList()
                    {
                        Html = "/OrganizationManagementApp/CompanyList.html"
                    };
                }

                ((CompanyList)master.RecentInvoices).RefreshData();
                master.FocusedInvoice = null;

                return master;
            });

            Handle.GET("/new-company", () =>
            {
                MasterPage master = Self.GET<MasterPage>("/");
                master.FocusedInvoice = Db.Scope<AddCompany>(() =>
                {
                    var page = new AddCompany()
                    {
                        Html = "/OrganizationManagementApp/AddCompany.html",
                        Data = new Company
                        {
                            Name = string.Empty
                        }
                    };

                    page.Saved += (s, a) =>
                    {
                        ((CompanyList)master.RecentInvoices).RefreshData();
                    };

                    return page;
                });
                return master;
            });

            Handle.GET("/companies/{?}", (int CompanyNo) =>
            {
                MasterPage master = Self.GET<MasterPage>("/");
                master.FocusedInvoice = Db.Scope<CompanyDetails>(() =>
                {
                    var newEmployee = new AddEmployee()
                    {
                        Html = "/OrganizationManagementApp/AddEmployee.html",
                        Data = new Person
                        {
                            CompanyNo = CompanyNo,
                            PersonName = string.Empty
                        }
                    };

                    newEmployee.Saved += (s, a) =>
                    {
                        ((CompanyDetails)master.FocusedInvoice).RefreshData();
                    };

                    var page = new CompanyDetails()
                    {
                        Html = "/OrganizationManagementApp/CompanyDetails.html",
                        Data = Db.SQL<Company>("SELECT c FROM Company c WHERE c.CompanyNo=?", CompanyNo).First,
                        AddEmployee = newEmployee
                    };

                    return page;
                });

                return master;
            });

            Handle.GET("/employee/{?}/details", (int PersonNo) =>
            {
                MasterPage master = Self.GET<MasterPage>("/");
                master.FocusedInvoice = Db.Scope(() =>
                {
                    var person = Db.SQL<Person>("SELECT p FROM Person p WHERE p.PersonNo=?", PersonNo).First;
                    var page = new EmployeeProfile()
                    {
                        Data = person,
                        AddSale = new RegisterSale()
                        {
                            Html = "/OrganizationManagementApp/RegisterSale.html",
                            Data = new Sales
                            {
                                Saler = person
                            },
                            PersonNo = PersonNo
                        }
                    };

                    return page;
                });
                return master;
            });
        }
    }
}