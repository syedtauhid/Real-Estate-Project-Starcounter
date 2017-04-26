﻿using System;
using Starcounter;

namespace OrganizationManagementApp
{
    class Program
    {
        static void Main()
        {
            Db.Transact(() => {
               var person = Db.SQL<Company>("select c from Company c").First;
               if (person == null)
                {
                    new Company
                    {
                        Name = "Tauhid Ahmed",
                        CompanyNo = 1
                    };
                }
            });

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

            Handle.GET("/companies/{?}", (int CompanyNo) =>
            {
                MasterPage master = Self.GET<MasterPage>("/");
                master.FocusedInvoice = Db.Scope<CompanyDetails>(() =>
                {
                    var page = new CompanyDetails()
                    {
                        Html = "/OrganizationManagementApp/CompanyDetails.html",
                        Data = Db.SQL<Company>("SELECT c FROM Company c WHERE c.CompanyNo=?", CompanyNo).First,
                        AddEmployee = new AddEmployee()
                        {
                            Html = "/OrganizationManagementApp/AddEmployee.html",
                            Data = new Person
                            {
                                CompanyNo = CompanyNo,
                                PersonName = ""
                            }
                        }
                    };
                    
                    return page;
                });
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
                            Name= ""
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

            //Handle.GET("/invoicedemo/menu", () => {
            //    MasterPage master = Self.GET<MasterPage>("/invoicedemo");
            //    master.ShowOwnTopBarMenu = false;
            //    return new Page() { Html = "/InvoiceDemo/AppMenuPage.html" };
            //});

            //UriMapping.Map("/invoicedemo/menu", UriMapping.MappingUriPrefix + "/menu");
        }
    }
}